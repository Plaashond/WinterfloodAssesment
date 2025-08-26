using WinterfloodAssesment.Enums;
using WinterfloodAssesment.Interfaces;
using System.Linq;


namespace WinterfloodAssesment.Factory
{
	public delegate IBookingService BookinServiceRewsolver(BookingType type);
	public class BookingServiceFactory(IServiceProvider serviceProvider)
	{
		private readonly IServiceProvider _serviceProvider = serviceProvider;

		public IBookingService GetService(BookingType bookingType) 
		{
			var services = _serviceProvider.GetServices<IBookingService>();

			var service = services.SingleOrDefault(x=>x.Type == bookingType);

			if (service == null) 
			{
				throw new InvalidOperationException($"Booking service for type '{bookingType}' not registered");
			}

			return service;
		}
	}
}
