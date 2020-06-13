namespace Generic
{
    using System;

    public class ActionPartialUpdater
    {
        public Action<int> onUpdate { get; private set; }
        public int total { get; private set; }
        public int delta { get; private set; }

        public int firstIndex { get; private set; }
        public int lastIndex { get; private set; }

        public ActionPartialUpdater(Action<int> onUpdate, int total, int delta = 1)
        {
            this.onUpdate = onUpdate;
            this.total = total;
            this.delta = delta;

            if (delta <= 0)
            {
                throw new ArgumentException("delta must be greater than 0. Actual: " + delta);
            }
        }

        public void OnUpdate()
        {
            firstIndex = lastIndex;
            lastIndex = (firstIndex + delta) % total;

            // 一周したとき
            if (firstIndex > lastIndex)
            {
                for (int i = firstIndex; i < total; i++)
                {
                    onUpdate(i);
                }

                for (int i = 0; i < lastIndex; i++)
                {
                    onUpdate(i);
                }
            }
            else
            {
                for (int i = firstIndex; i < lastIndex; i++)
                {
                    onUpdate(i);
                }
            }
        }
    }
}