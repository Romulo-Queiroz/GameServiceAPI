using GameServiceAPI.Configuration;
using GameServiceAPI.Clients;
using Microsoft.Extensions.Options;
using GameServiceAPI.Interfaces;
using GameServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
              .WithOrigins("http://localhost:5173")   
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
});

// 1) Registra as op��es:
builder.Services.Configure<FreeToGameOptions>(
    builder.Configuration.GetSection("FreeToGameApi"));

// 2) Registra o client tipado e depois *configura* o HttpClient com sp:
builder.Services
    .AddHttpClient<IFreeToGameClient, FreeToGameClient>()
    .ConfigureHttpClient((sp, client) =>
    {
        // Aqui sim 'sp' � IServiceProvider e 'client' � HttpClient
        var opts = sp.GetRequiredService<IOptions<FreeToGameOptions>>().Value;
        client.BaseAddress = new Uri(opts.BaseUrl);
    });

// 3) Resto do pipeline
builder.Services.AddControllers();
builder.Services.AddScoped<GamesService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
