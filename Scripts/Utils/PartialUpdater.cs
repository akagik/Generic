namespace Generic
{
    using System;
    using UnityEngine;

    public class PartialUpdater
    {
        public int total { get; private set; }
        public int delta { get; private set; }

        // lastIndex < firstIndex となるような更新を許すか?
        // false の場合、必ず一度は lastIndex == total となるような更新を行う.
        public bool cyclicLastIndex = false;

        public int firstIndex { get; private set; }
        public int lastIndex { get; private set; }

        public PartialUpdater(int total, int delta = 1)
        {
            this.total = total;
            this.delta = delta;

            if (delta <= 0)
            {
                throw new ArgumentException("delta must be greater than 0. Actual: " + delta);
            }
        }

        public void OnUpdate()
        {
            firstIndex = lastIndex % total;

            if (cyclicLastIndex)
            {
                lastIndex = (firstIndex + delta) % total;
            }
            else
            {
                lastIndex = Mathf.Min(firstIndex + delta, total);
            }
        }
    }
}