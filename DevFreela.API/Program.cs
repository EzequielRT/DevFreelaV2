using DevFreela.API.Middlewares;
using DevFreela.Application;
using DevFreela.Application.Options;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .Configure<FreelanceTotalCostOptions>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));

//builder.Services.AddDbContext<DevFreelaDbContext>(options =>
//    options.UseInMemoryDatabase("DevFreelaDb"));
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DafaultConnection")));

builder.Services.AddApplicationModule();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
