namespace TPro.EntityFramework.Entity
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
}