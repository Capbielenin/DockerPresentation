using KebabMenuApplication.Extensions;
using KebabMenuApplication.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<ConnectionOptions>(
    options => builder.Configuration.GetSection("ConnectionStrings").Bind(options));
builder.Services.Configure<RabbitMqConnection>(
    options => builder.Configuration.GetSection("RabbitMq").Bind(options));

builder.Services.AddDataServices();
builder.Services.AddMessageBus();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.RunSetUpScript();
app.MapHealthChecks("/healthz");
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();