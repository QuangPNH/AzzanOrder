using Net.payOS;
using System.Reflection;

/*IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
*/


IConfiguration configuration = new ConfigurationBuilder()
	.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
	.AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS("c3ab25b3-a36b-44bc-8a15-0a2406de4642",
					"5e6bd626-9e42-4e4b-8b8a-6858d1f7615a",
					"bf84f6e8550cecf8ef0cf8c0b3eca70a37dd2ceb610efb6c3cc01d25148d637b");

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddSingleton(payOS);

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});


var app = builder.Build();

// app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
