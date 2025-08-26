using WinterfloodAssesment.Abstractions;
using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Enums;

namespace WinterfloodAssesment.Services
{
	public class ApartmentBookingService : BookingBase
	{
		public override BookingType Type { get; set; }
		private static readonly Dictionary<int, Booking> _bookings = [];

		public ApartmentBookingService()
		{
			Type = BookingType.Apartment;
		}

		private static int _nextId = 0;

		protected override Task<Booking> AddBookingAsync(Booking booking)
		{
			var newId = Interlocked.Increment(ref _nextId);
			booking.BookingReference = newId;
			_bookings[booking.BookingReference] = booking;
			return Task.FromResult(booking);
		}

		protected override Task<bool> CancelBookingAsync(Booking booking)
		{
			_bookings.Remove(booking.BookingReference);
			return Task.FromResult(true);
		}

		protected override Task<Booking> EditBookingAsync(Booking booking)
		{
			if (!_bookings.ContainsKey(booking.BookingReference))
			{
				throw new ArgumentException("Booking does not exist");
			}
			if(_bookings[booking.BookingReference].CustomerName != booking.CustomerName)
			{
				throw new ArgumentException("CustomerName can not be updated.");
			}
			_bookings[booking.BookingReference] = booking;
			return Task.FromResult(_bookings[booking.BookingReference]);
		}

		protected override Task<List<Booking>> ViewBookingsAsync(string customerName)
		{
			return Task.FromResult(_bookings.Select(x=>x.Value).Where(x=>x.CustomerName == customerName).ToList());
		}
	}
}
