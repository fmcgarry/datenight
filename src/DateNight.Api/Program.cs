using DateNight.Api;
using DateNight.Infrastructure;
using DateNight.Infrastructure.Auth;

const string DateNightDatabase = "DateNightDatabase";
const string JwtSettings = "JwtSettings";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddRequiredInfrastructureConfiguration();

builder.Services.AddAppLogger();
builder.Services.AddIdeaService(builder.Configuration.GetSection(DateNightDatabase));
builder.Services.AddUserService(builder.Configuration.GetSection(DateNightDatabase));

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration.GetSection(JwtSettings));
builder.Services.AddApplicationInsightsTelemetry();

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