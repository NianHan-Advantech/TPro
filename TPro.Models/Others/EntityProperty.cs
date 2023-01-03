namespace TPro.Models.Others
{
    public class EntityProperty
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string Type { get; set; }
        public bool IsKey { get; set; } = false;
        public bool IsForiginKey { get; set; } = false;
        public string ForiginEntityName { get; set; }
    }
}