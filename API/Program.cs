using API.Services;
using API.Entities;
using API.Middlewares;

using Shared.Validators;

#if DEBUG
var urls = new string[] { "http://localhost:32406/" };
var origins = new string[] { "http://localhost:5173/" };
#else
var urls = new string[] { "http://162.55.32.18:32406/" };
var origins = new string[] { "http://162.55.32.18:80/" };
#endif

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(urls);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddLazyCache();

builder.Services.AddTransient<UCContext>();

builder.Services.AddTransient<LinkValidator>();

builder.Services.AddSingleton<RankService>();
builder.Services.AddTransient<RadixService>();
builder.Services.AddSingleton<CommonService>();
builder.Services.AddTransient<LinkZipService>();
builder.Services.AddTransient<CurrencyService>();
builder.Services.AddTransient<TemperatureService>();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(config => config.AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(origin => true)
                          .WithOrigins(origins)
                          .AllowCredentials());

app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.Run();
