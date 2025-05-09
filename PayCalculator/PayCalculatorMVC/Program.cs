using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEmployeeRepository<PermanentEmployee>, PermanentEmployeeRepository>();
builder.Services.AddSingleton<IEmployeeRepository<TemporaryEmployee>, TemporaryEmployeeRepository>();
builder.Services.AddSingleton<IPermanentEmployeeMapper, PermanentEmployeeMapper>();
builder.Services.AddSingleton<IPermanentPayCalculator, PermanentPayCalculator>();
builder.Services.AddSingleton<ITemporaryEmployeeMapper, TemporaryEmployeeMapper>();
builder.Services.AddSingleton<ITemporaryPayCalculator, TemporaryPayCalculator>();
builder.Services.AddSingleton<ITimeCalculator, TimeCalculator>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
