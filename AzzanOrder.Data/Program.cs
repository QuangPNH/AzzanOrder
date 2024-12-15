using AzzanOrder.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using AzzanOrder.Data.DTO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var a = builder.Configuration.GetConnectionString("MyCnn");
builder.Services.AddDbContext<OrderingAssistSystemContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
.AddOData(options => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
.AddRouteComponents("odata", EdmModel.GetEdmModel()))
.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(); // Add this line to enable CORS

app.MapControllers();

app.Run();
