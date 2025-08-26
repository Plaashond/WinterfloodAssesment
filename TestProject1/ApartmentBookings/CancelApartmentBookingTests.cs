using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Services;

namespace TestProject1
{
	public class CancelApartmentBookingTests:BaseTest
	{
		[Fact]
		public async Task CancelBookingTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow;
			booking.EndDate = DateTime.UtcNow.AddDays(1);
			Booking bookingResult = await apartmentBookingService.AddBooking(booking);
			List<Booking> viewResultsBeforeUpdate = await apartmentBookingService.ViewBookings(bookingResult.CustomerName);
			
			var canceled = await apartmentBookingService.CencelBooking(bookingResult);
			Assert.True(canceled);
			List<Booking> viewResultsAfterUpdate = await apartmentBookingService.ViewBookings(bookingResult.CustomerName);
			Assert.True(!viewResultsAfterUpdate.Any(x=>x.BookingReference == bookingResult.BookingReference));
		}
		[Fact]
		public async Task CancelBookingWrongDateTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow.AddDays(-2);
			booking.EndDate = DateTime.UtcNow.AddDays(-1);
			try
			{
				var canceled = await apartmentBookingService.CencelBooking(booking);
				Assert.Fail();
			}catch(Exception ex)
			{
				Assert.Contains("from the past",ex.Message);
			}
		}
	}
}