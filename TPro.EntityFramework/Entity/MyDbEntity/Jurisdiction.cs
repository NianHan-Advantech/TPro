namespace TPro.EntityFramework.Entity.MyDbEntity
{
    public class Jurisdiction
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long RouteId { get; set; }
        public bool IsAllowed { get; set; }
        public virtual TPRole Role { get; set; }
        public virtual RoutePath Route { get; set; }
    }
    public class CustomText
    {
        public long Id { get; set; }
        public string FontStyle { get; set; }
        public int FontSize { get; set; }
        public string SizeUnit { get; set; }

    }
    public class ItemPosition
    {
        public long Id { get; set; }
        public decimal Left { get; set; }
        public decimal Rigth { get; set; }
        public decimal Top { get; set; }
        public decimal Bottom { get; set; }
        public decimal Rotate { get; set; }
        public decimal Scale { get; set; }
    }
}