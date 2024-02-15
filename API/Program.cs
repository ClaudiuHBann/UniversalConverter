using API.Services;
using API.Entities;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddLazyCache();
builder.Services.AddTransient<UCContext>();
builder.Services.AddTransient<RadixService>();
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
                          .WithOrigins("https://localhost:5001/")
                          .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.Run();
