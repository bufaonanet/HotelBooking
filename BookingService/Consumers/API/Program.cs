using Application.Guest;
using Application.Guest.Ports;
using Application.Room.Ports;
using Application.Room;
using Data;
using Data.Guest;
using Data.Room;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;
using Application.Booking.Ports;
using Application.Booking;
using Data.Booking;
using Domain.Booking.Ports;
using Application.Payment.Ports;
using System.Text.Json.Serialization;
using Payment.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

#region Db Config
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region Ioc
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
