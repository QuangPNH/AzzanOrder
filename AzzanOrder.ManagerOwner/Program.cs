using AzzanOrder.ManagerOwner.Filters;
using AzzanOrder.ManagerOwner.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// Register HttpClient
builder.Services.AddHttpClient();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<EmployeeActionFilter>();
});
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(15));
var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
