using RabbitMQ.Client;
using Refit;
using Transfer.Api.CrossCutting;
using Transfer.Api.Domain.Options;
using Transfer.Api.Event;
using Transfer.Api.Infra;
using Transfer.Api.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRefitClient<ITransferAccountApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("TransferAccountApi").Value));

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(nameof(DatabaseOptions)));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddTransient<IEventBus, EventBus>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
