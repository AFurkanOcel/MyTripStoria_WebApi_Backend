namespace Contracts.TripDtos
{
    public class TripPutDto
    {
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TripType { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}
