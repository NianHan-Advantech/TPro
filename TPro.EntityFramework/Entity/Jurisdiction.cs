namespace TPro.EntityFramework.Entity
{
    public class Jurisdiction
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RouteId { get; set; }
        public bool IsAllowed { get; set; }
        public virtual TPRole Role { get; set; }
        public virtual RoutePath Route { get; set; }
    }
}