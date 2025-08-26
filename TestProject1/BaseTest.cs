using WinterfloodAssesment.Entities;
using WinterfloodAssesment.Services;

namespace TestProject1
{
	public class BaseTest
	{
		public Booking booking;
		public BaseTest()
		{
			booking = new() { CustomerName = "juan",PartySize = 1, ProductName= "Century City" };
		}
	}
}