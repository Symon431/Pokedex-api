using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Data;
using Pokedex.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = builder.Configuration.GetConnectionString("PokemonStore");

builder.Services.AddDbContext<PokemonStoreContext>(options =>
    options.UseNpgsql(connString));

var app = builder.Build();

app.MapPokemonsEndpoints();

app.Run();
