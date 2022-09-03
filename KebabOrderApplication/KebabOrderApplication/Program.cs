using KebabOrderApplication.Extensions;
using KebabOrderApplication.Options;
using KebabOrderApplication.Repositories;
using KebabOrderApplication.Services;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddConnections();

builder.Services.Configure<RabbitMqConnection>(
    builder.Configuration.GetSection("RabbitMq"));

var martenOptions = builder.Configuration.GetSection("ConnectionStrings")
    .GetChildren();

builder.Services.AddHealthChecks();
builder.Services.AddMarten(options =>
{
    options.CreateDatabasesForTenants(dat =>
    {
        dat.MaintenanceDatabase((martenOptions.First(o => o.Key == "Master").Value))
            .ForTenant()
            .CheckAgainstPgDatabase()
            .WithOwner("postgres");
    });
    options.Connection(martenOptions.First(o => o.Key == "OrderDb").Value);
});

// builder.Services.AddSingleton<IBusReceiver, BusReceiver>();
var app = builder.Build();
app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception e)
{
    app?.Logger.LogCritical(e, "Problem during startup");
}
