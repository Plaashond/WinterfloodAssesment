using System.Threading.Tasks;
using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Services;

namespace TestProject1.ApartmentBookings
{
	public class AddApartmentBookingTests: BaseTest
	{

		[Fact]
		public async Task AddBookingTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow;
			booking.EndDate = DateTime.UtcNow.AddDays(1);
			Booking bookingResult = await apartmentBookingService.AddBooking(booking);
			Assert.NotNull(bookingResult);
			List<Booking> viewResults = await apartmentBookingService.ViewBookings(booking.CustomerName);
			Assert.NotEmpty(viewResults);
			var onlyRow = viewResults.First();
			Assert.True(onlyRow.BookingReference > 0);
		}
		[Fact]
		public async Task AddBookingWithWrongDatesTes()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow.AddDays(-1);
			booking.EndDate = DateTime.UtcNow.AddDays(1);
			try
			{
				await apartmentBookingService.AddBooking(booking);
				Assert.Fail("False Positive A");
			}
			catch (Exception ex) 
			{
				Assert.Contains("cant be in the past", ex.Message);
			}
			booking.StartDate = DateTime.UtcNow;
			booking.EndDate = DateTime.UtcNow.AddDays(-1);
			try
			{
				await apartmentBookingService.AddBooking(booking);
				Assert.Fail("False Positive B");
			}
			catch (Exception ex)
			{
				Assert.Contains("must be after start date", ex.Message);
			}
		}
		[Fact]
		public async Task AddBookingNullObjectTes()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking = null;
			try
			{
				await apartmentBookingService.AddBooking(booking);
				Assert.Fail("False Positive A");
			}
			catch (Exception ex)
			{
				Assert.Contains("Value cannot be null", ex.Message);
			}
		}

	}
}