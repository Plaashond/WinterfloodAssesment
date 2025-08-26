using WinterfloodAssesment.Factory;
using WinterfloodAssesment.Interfaces;
using WinterfloodAssesment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApartmentBookingService>();

builder.Services.AddScoped<IBookingService>(s => s.GetRequiredService<ApartmentBookingService>());

builder.Services.AddScoped<BookingServiceFactory>();

builder.Services.AddScoped<BookinServiceRewsolver>(provider =>
{
    var factory = provider.GetService<BookingServiceFactory>();
    if (factory != null)
    {
		return type => factory.GetService(type);
	}
    throw new Exception("Can nott resolve factory. Type not set in service.");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
