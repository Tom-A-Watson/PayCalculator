using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using log4net;
using log4net.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var _log4net = LogManager.GetLogger(typeof(Program));
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
var fileInfo = new FileInfo("C:\\dev\\PayCalculatorRoot\\PayCalculator\\log4net.config");
XmlConfigurator.Configure(logRepository, fileInfo);

// Add services to the container.
builder.Services.AddSingleton<IEmployeeRepository<PermanentEmployee>, PermanentEmployeeRepository>();
builder.Services.AddSingleton<IEmployeeRepository<TemporaryEmployee>, TemporaryEmployeeRepository>();
builder.Services.AddSingleton<IPermanentPayCalculator, PermanentPayCalculator>();
builder.Services.AddSingleton<ITemporaryPayCalculator, TemporaryPayCalculator>();
builder.Services.AddSingleton<IPermanentEmployeeMapper, PermanentEmployeeMapper>();
builder.Services.AddSingleton<ITemporaryEmployeeMapper, TemporaryEmployeeMapper>();
builder.Services.AddSingleton<ITimeCalculator, TimeCalculator>();
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
