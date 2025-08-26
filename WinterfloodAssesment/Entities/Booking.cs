using WinterfloodAssesment.Enums;

namespace WinterfloodAssesment.Entities
{
	public class Booking
	{
		public required string CustomerName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public required int PartySize { get; set; }
		public required string ProductName { get; set; }
		public int BookingReference { get; set; }
		public BookingType BookingType { get; set; }
	}
}
