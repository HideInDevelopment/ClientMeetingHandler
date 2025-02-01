using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.infrastructure.persistence.configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));

//Dependency Injection
builder.Services.AddScoped<IEntityTypeConfiguration<Contact>, ContactConfiguration>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();