using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }
        public bool IsPremium { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}