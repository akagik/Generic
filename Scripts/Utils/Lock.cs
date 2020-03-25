using System;

/// <summary>
/// Lock インスタンスは生成された時点で一意なIDを持つ.
/// </summary>
[Serializable]
public class Lock : IEquatable<Lock>
{
    private static int currentId = 0;

    private int _id;

    public int id => _id;

    public static Lock Create()
    {
        return new Lock();
    }

    private Lock()
    {
        currentId++;

        if (currentId >= int.MaxValue)
        {
            currentId = 0;
        }

        _id = currentId;
    }

    public bool Equals(Lock other)
    {
        if (other == null)
            return false;

        if (this.id == other.id)
            return true;
        else
            return false;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        Lock lck = obj as Lock;

        if (lck == null)
            return false;
        else
            return Equals(lck);
    }

    public override int GetHashCode()
    {
        return this.id.GetHashCode();
    }

    public static bool operator ==(Lock lck1, Lock lck2)
    {
        if (((object) lck1) == null || ((object) lck2) == null)
            return System.Object.Equals(lck1, lck2);

        return lck1.Equals(lck2);
    }

    public static bool operator !=(Lock lck1, Lock lck2)
    {
        if (((object) lck1) == null || ((object) lck2) == null)
            return !System.Object.Equals(lck1, lck2);

        return !(lck1.Equals(lck2));
    }
}