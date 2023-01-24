using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEmployeeRepository<PermanentEmployee>, PermanentEmployeeRepository>();
builder.Services.AddSingleton<IEmployeeRepository<TemporaryEmployee>, TemporaryEmployeeRepository>();
builder.Services.AddSingleton<IPermanentPayCalculator, PermanentPayCalculator>();
builder.Services.AddSingleton<ITemporaryPayCalculator, TemporaryPayCalculator>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
