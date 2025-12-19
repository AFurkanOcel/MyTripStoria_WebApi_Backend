namespace Contracts.UserDtos
{
    public class UserPostDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public decimal Budget { get; set; }
        public bool IsPremium { get; set; }
    }
}
