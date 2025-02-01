using ClientMeetingHandler.application.settings;
using ClientMeetingHandler.common;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.infrastructure.persistence.configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();