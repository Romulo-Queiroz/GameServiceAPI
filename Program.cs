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

builder.Services.Configure<FreeToGameOptions>(
    builder.Configuration.GetSection("FreeToGameApi"));

builder.Services
    .AddHttpClient<IFreeToGameClient, FreeToGameClient>()
    .ConfigureHttpClient((sp, client) =>
    {
        var opts = sp.GetRequiredService<IOptions<FreeToGameOptions>>().Value;
        client.BaseAddress = new Uri(opts.BaseUrl);
    });

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
