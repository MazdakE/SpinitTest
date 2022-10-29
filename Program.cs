using SpinitTest.Context;
using SpinitTest.Repositories;
using SpinitTest.Repositories.Interfaces;
using SpinitTest.Services;
using SpinitTest.Services.Interfaces;
using SpinitTest.Services.PropertyMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spinit", Version = "v1" });
    c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date" });
    c.EnableAnnotations();

    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    c.IncludeXmlComments(xmlFullPath);
});

// Add services to the container.
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IHistoryLogRepository, HistoryLogRepository>();
builder.Services.AddScoped<IHistoryLogService, HistoryLogService>();
builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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
