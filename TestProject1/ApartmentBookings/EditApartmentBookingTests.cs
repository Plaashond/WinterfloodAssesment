using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Services;

namespace TestProject1.ApartmentBookings
{
	public class EditApartmentBookingTests:BaseTest
	{
		[Fact]
		public async Task EditBookingTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow;
			booking.EndDate = DateTime.UtcNow.AddDays(1);
			Booking bookingResult = await apartmentBookingService.AddBooking(booking);
			List<Booking> viewResultsBeforeUpdate = await apartmentBookingService.ViewBookings(bookingResult.CustomerName);
			var onlyRowBeforeUpdating = viewResultsBeforeUpdate.Single(x=>x.BookingReference == bookingResult.BookingReference);
			Assert.Equal(bookingResult.CustomerName, onlyRowBeforeUpdating.CustomerName);
			Assert.Equal(bookingResult.ProductName, onlyRowBeforeUpdating.ProductName);
			Assert.Equal(bookingResult.PartySize, onlyRowBeforeUpdating.PartySize);
			var updatedBooking = new Booking
			{
				BookingReference = bookingResult.BookingReference,
				CustomerName = bookingResult.CustomerName,
				StartDate = DateTime.UtcNow.AddDays(1),
				EndDate = DateTime.UtcNow.AddDays(2),
				PartySize = 2,
				ProductName = "UpdatedProductName"
			};

			var editedBooking = await apartmentBookingService.EditBooking(updatedBooking);
			List<Booking> viewResultsAfterUpdate = await apartmentBookingService.ViewBookings(bookingResult.CustomerName);
			Assert.NotEmpty(viewResultsAfterUpdate);
			var onlyRowAfterUpdate = viewResultsAfterUpdate.Single(x => x.BookingReference == bookingResult.BookingReference);
			Assert.Equal(editedBooking.ProductName, onlyRowAfterUpdate.ProductName);
			Assert.Equal(editedBooking.PartySize, onlyRowAfterUpdate.PartySize);
			Assert.Equal(updatedBooking.ProductName, onlyRowAfterUpdate.ProductName);
			Assert.Equal(updatedBooking.PartySize, onlyRowAfterUpdate.PartySize);
		}
		[Fact]
		public async Task EditBookingCustomerNameTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			try
			{
				Booking booking = new() { CustomerName = "juan", PartySize = 1, ProductName = "Century City" };
				booking.StartDate = DateTime.UtcNow.AddDays(1);
				booking.EndDate = DateTime.UtcNow.AddDays(2);
				var bookingResult = await apartmentBookingService.AddBooking(booking);
				var updatedBooking = new Booking
				{
					BookingReference = booking.BookingReference,
					CustomerName = "test",
					StartDate = DateTime.UtcNow.AddDays(1),
					EndDate = DateTime.UtcNow.AddDays(2),
					PartySize = 2,
					ProductName = "UpdatedProductName"
				};
				var editedBooking = await apartmentBookingService.EditBooking(updatedBooking);
				Assert.Fail();
			}
			catch (Exception ex) 
			{
				Assert.Contains("CustomerName can not", ex.Message);
			}
		}
		[Fact]
		public async Task EditBookingNullObjectTest()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking = null;
			try
			{
				await apartmentBookingService.EditBooking(booking);
				Assert.Fail("False Positive A");
			}
			catch (Exception ex)
			{
				Assert.Contains("Value cannot be null", ex.Message);
			}
		}
		[Fact]
		public async Task EditNonExistingBooking()
		{
			var apartmentBookingService = new ApartmentBookingService();
			booking.StartDate = DateTime.UtcNow.AddDays(1);
			booking.EndDate = DateTime.UtcNow.AddDays(2);
			try
			{
				await apartmentBookingService.EditBooking(booking);
				Assert.Fail("False Positive A");
			}
			catch (Exception ex)
			{
				Assert.Contains("does not exist", ex.Message);
			}
		}
	}
}