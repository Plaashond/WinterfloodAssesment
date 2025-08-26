using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Services;

namespace TestProject1
{
	public class ViewApartmentBookingTests:BaseTest
	{
		[Fact]
		public async Task CancelBookingTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow;
			booking.EndDate = DateTime.UtcNow.AddDays(1);
			Booking bookingResult = await apartmentBookingService.AddBooking(booking);
			List<Booking> viewResultsBeforeUpdate = await apartmentBookingService.ViewBookings(bookingResult.CustomerName);
			Assert.NotEmpty(viewResultsBeforeUpdate);
		}
	}
}