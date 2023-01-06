namespace TPro.Common.NianLog
{
    public sealed class NianLogEntity
    {
        public long Id { get; set; }
        public string LogLevel { get; set; }
        public string EventId { get; set; }
        public string State { get; set; }
        public string Exception { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}