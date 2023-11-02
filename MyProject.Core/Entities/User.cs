namespace MyProject.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int CityId { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public City City { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }

    }
}
