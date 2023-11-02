namespace MyProject.Core.DTOs.UserDtos
{
    public class UserRegisterDto
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string Phone { get; set; }
        public int CityId { get; set; }
    }
}
