using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Enums;

namespace WinterfloodAssesment.Interfaces
{
	public interface IBookingService
	{
		BookingType Type { get; }
		public Task<Booking> AddBooking(Booking booking);
		public Task<Booking> EditBooking(Booking booking);
		public Task<bool> CencelBooking(Booking booking);
		public Task<List<Booking>> ViewBookings(string customerName);
	}
}
