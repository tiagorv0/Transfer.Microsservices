using Transfer.Account.Domain.Options;
using Transfer.Account.Events;
using Transfer.Account.Infra;
using Transfer.Account.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHostedService<TransferConsumer>();

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
