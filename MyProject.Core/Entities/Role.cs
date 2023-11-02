namespace MyProject.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}
