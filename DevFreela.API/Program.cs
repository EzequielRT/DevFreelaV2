using DevFreela.API.Extensions;
using DevFreela.API.Middlewares;
using DevFreela.Application;
using DevFreela.Application.Settings;
using DevFreela.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentJsonFiles(builder.Environment);
builder.AddLogging();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddRouting(opt => { opt.LowercaseUrls = true; });

builder.Services
    .Configure<FreelanceTotalCostSettings>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));

builder.Services.AddSwaggerConfiguration();

builder.Services
    .AddApplicationModule()
    .AddInfraModule(builder.Configuration);

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
