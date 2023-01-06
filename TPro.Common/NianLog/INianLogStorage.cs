namespace TPro.Common.NianLog
{
    public interface INianLogStorage
    {
        void WriteToStorage(NianLogEntity entity);
    }
}