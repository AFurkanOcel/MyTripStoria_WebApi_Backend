using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Trip
    {
        [Key]
        public int TripID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public bool IsCompleted { get; set; }
        public string TripType { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan TripDuration => EndDate - StartDate;
        [StringLength(1000)]
        public string? Notes { get; set; }
    }
}