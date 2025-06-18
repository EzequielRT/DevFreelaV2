using DevFreela.API.Extensions;
using DevFreela.API.Middlewares;
using DevFreela.Application;
using DevFreela.Application.Settings;
using DevFreela.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentJsonFiles(builder.Environment);
builder.AddLogging();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddRouting(opt => { opt.LowercaseUrls = true; });

builder.Services
    .Configure<FreelanceTotalCostSettings>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));

//builder.Services.AddDbContext<DevFreelaDbContext>(options =>
//    options.UseInMemoryDatabase("DevFreelaDb"));
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerConfiguration();
builder.Services.AddApplicationModule();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
