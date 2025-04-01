using ClientMeetingHandler.common;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();