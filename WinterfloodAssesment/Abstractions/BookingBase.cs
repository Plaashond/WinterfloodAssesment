using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Enums;
using WinterfloodAssesment.Interfaces;

namespace WinterfloodAssesment.Abstractions
{
	public abstract class BookingBase : IBookingService
	{
		public abstract BookingType Type { get; set; }

		public async Task<Booking> AddBooking(Booking booking)
		{
			ArgumentNullException.ThrowIfNull(booking);
			ValidateBookingDates(booking.StartDate,booking.EndDate);
			ValidatePartySize(booking.PartySize);
			return await AddBookingAsync(booking);
		}

		public async Task<bool> CencelBooking(Booking booking)
		{
			ArgumentNullException.ThrowIfNull(booking);
			ValidateCancelationDate(booking.EndDate);
			return await CancelBookingAsync(booking);
		}

		public async Task<Booking> EditBooking(Booking booking)
		{
			ArgumentNullException.ThrowIfNull(booking);
			ValidateBookingDates(booking.StartDate, booking.EndDate);
			ValidatePartySize(booking.PartySize);
			return await EditBookingAsync(booking);
		}

		public async Task<List<Booking>> ViewBookings(string customerName)
		{
			return await ViewBookingsAsync(customerName);
		}

		private static void ValidateBookingDates(DateTime start, DateTime end)
		{
			if(start < DateTime.UtcNow && start.Date != DateTime.UtcNow.Date)
			{
				throw new ArgumentException("Booking start date cant be in the past.");
			}
			if (end <= start)
			{
				throw new ArgumentException("Booking ebnd date must be after start date");
			}
		}

		private static void ValidateCancelationDate(DateTime end)
		{
			if (end < DateTime.UtcNow)
			{
				throw new ArgumentException("Cant cancel a booking from the past.");
			}
		}
		private static void ValidatePartySize(int PartySize)
		{
			if (PartySize < 1)
			{
				throw new ArgumentException("Party size must atleast be 1 or more.");
			}
		}

		protected abstract Task<Booking> AddBookingAsync(Booking booking);
		protected abstract Task<bool> CancelBookingAsync(Booking booking);
		protected abstract Task<Booking> EditBookingAsync(Booking booking);
		protected abstract Task<List<Booking>> ViewBookingsAsync(string customerName);
	}
}
