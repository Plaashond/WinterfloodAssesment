using Microsoft.AspNetCore.Mvc;
using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Enums;
using WinterfloodAssesment.Factory;

namespace WinterfloodAssesment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookinServiceRewsolver _bookinServiceRewsolver;

        public BookingController(BookinServiceRewsolver bookinServiceRewsolver)
        {
			_bookinServiceRewsolver = bookinServiceRewsolver;
        }

        [HttpGet(Name = "GetBooking")]
        public async Task<ICollection<Booking>> Get([FromQuery] BookingType bookingType, [FromQuery] string customerName)
        {
            var bookingservice = _bookinServiceRewsolver(bookingType);

            return await bookingservice.ViewBookings(customerName);
        }
		[HttpPost(Name = "AddBooking")]
		public async Task<Booking> Add([FromBody] Booking booking)
		{
			var bookingservice = _bookinServiceRewsolver(booking.BookingType);

			return await bookingservice.AddBooking(booking);
		}
		[HttpPut(Name = "EditBooking")]
		public async Task<Booking> Edit([FromBody] Booking booking)
		{
			var bookingservice = _bookinServiceRewsolver(booking.BookingType);

			return await bookingservice.EditBooking(booking);
		}
		[HttpDelete(Name = "CancelBooking")]
		public async Task<bool> Delete([FromBody] Booking booking)
		{
			var bookingservice = _bookinServiceRewsolver(booking.BookingType);

			return await bookingservice.CencelBooking(booking);
		}
	}
}
