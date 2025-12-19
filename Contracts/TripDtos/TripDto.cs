namespace Contracts.TripDtos
{
    public class TripDto
    {
        public int TripID { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string TripType { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        // Id's for Backend
        public int CountryId { get; set; }
        public int CityId { get; set; }
        // Names for Frontend
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan TripDuration => EndDate - StartDate;
        public string Notes { get; set; }
    }
}
