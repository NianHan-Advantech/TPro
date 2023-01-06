using System;

namespace TPro.Common.NianLog
{
    public sealed class NianLogStorage : INianLogStorage
    {
        public void WriteToStorage(NianLogEntity entity)
        {
            Console.WriteLine(entity.ToString());
        }
    }
}