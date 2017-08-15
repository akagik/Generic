using System.Collections.Generic;

public static class StringExtensions
{
    /// <summary>
    /// 文字が全角なら true を、半角なら false を返します
    /// </summary>
    public static bool IsChar2Byte(char c)
    {
        return !((c >= 0x0 && c < 0x81) || (c == 0xf8f0) || (c >= 0xff61 && c < 0xffa0) || (c >= 0xf8f1 && c < 0xf8f4));
    }

    /// <summary>
    /// バイト数を計算して返します
    /// </summary>
    public static int GetByteCount(this string self)
    {
        int count = 0;
        for (int i = 0; i < self.Length; i++)
        {
            if (IsChar2Byte(self[i]))
            {
                count++;
            }
            count++;
        }
        return count;
    }

    /// <summary>
    /// インスタンスからバイト単位で部分文字列を取得します
    /// </summary>
    public static string SubstringInByte(this string self, int byteCount)
    {
        string tmp = "";
        int count = 0;
        for (int i = 0; i < self.Length; i++)
        {
            char c = self[i];
            if (IsChar2Byte(c))
            {
                count++;
            }
            count++;
            if (count > byteCount)
            {
                break;
            }
            tmp += c;
        }
        return tmp;
    }

    /// <summary>
    /// <para>インスタンスからバイト単位で部分文字列を取得します</para>
    /// <para>文字数が指定されたバイト数以内の場合はインスタンスをそのまま返します</para>
    /// </summary>
    public static string SafeSubstringInByte(this string self, int byteCount)
    {
        return byteCount < self.GetByteCount() ? self.SubstringInByte(byteCount) : self;
    }
}
