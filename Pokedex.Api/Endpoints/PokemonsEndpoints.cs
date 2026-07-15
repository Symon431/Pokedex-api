using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Data;
using Pokedex.Api.Dtos;
using Pokedex.Api.Models;

namespace Pokedex.Api.Endpoints
{
    public static class PokemonsEndpoints
    {
        const string GETpokemonEndpointName = "GETpokemon";

        public static async Task MapPokemonsEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("/pokemons");

            //GET/pokemon
            group.MapGet("/", async (PokemonStoreContext dbContext)
                => await dbContext.Pokemons
                .Include(pokemon => pokemon.Type)
                .Select(pokemon => new PokemonSummaryDto(
                    pokemon.Id,
                    pokemon.Name,
                    pokemon.Type.Name,
                    pokemon.Hp,
                    pokemon.Attack,
                    pokemon.Defense,
                    pokemon.ImageUrl
                ))
                .AsNoTracking()
                .ToListAsync());

            //Get/pokemon/id
            group.MapGet("/{id}", async (int id, PokemonStoreContext dbContext) =>
            {
                var pokemon = await dbContext.Pokemons.FindAsync(id);

                return pokemon is null ? Results.NotFound() : Results.Ok(
                    new PokemonDetailsDto(
                        pokemon.Id,
                        pokemon.Name,
                        pokemon.TypeId,
                        pokemon.Hp,
                        pokemon.Attack,
                        pokemon.Defense,
                        pokemon.ImageUrl
                    )
                );

            }).WithName(GETpokemonEndpointName);

            //POST/pokemon
            group.MapPost("/", async (CreatePokemonDto newPokemon, PokemonStoreContext dbContext) =>
            {
                Pokemon pokemon = new()
                {
                    Name = newPokemon.Name,
                    TypeId = newPokemon.TypeId,
                    Hp = newPokemon.HP,
                    Attack = newPokemon.Attack,
                    Defense = newPokemon.Defense,
                    ImageUrl = newPokemon.ImageUrl
                };

                dbContext.Pokemons.Add(pokemon);
                await dbContext.SaveChangesAsync();

                PokemonDetailsDto pokemonDto = new(
                    pokemon.Id,
                    pokemon.Name,
                    pokemon.TypeId,
                    pokemon.Hp,
                    pokemon.Attack,
                    pokemon.Defense,
                    pokemon.ImageUrl
                );

                return Results.CreatedAtRoute(GETpokemonEndpointName, new { id = pokemonDto.Id }, pokemonDto);
            });

            // PUT/pokemon/{id}
            group.MapPut("/{id}", async (PokemonStoreContext dbContext, int id, UpdatePokemonDto updatePokemon) =>
            {
                var existingPokemon = await dbContext.Pokemons.FindAsync(id);

                if (existingPokemon is null)
                {
                    return Results.NotFound();
                }

                existingPokemon.Name = updatePokemon.Name;
                existingPokemon.TypeId = updatePokemon.TypeId;
                existingPokemon.Hp = updatePokemon.HP;
                existingPokemon.Attack = updatePokemon.Attack;
                existingPokemon.Defense = updatePokemon.Defense;
                existingPokemon.ImageUrl = updatePokemon.ImageUrl;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();

            });

            // DELETE/pokemon/{id}
            group.MapDelete("/{id}", async (int id, PokemonStoreContext dbContext) => {
                
               await dbContext.Pokemons
                                    .Where(pokemon => pokemon.Id == id)
                                    .ExecuteDeleteAsync();

                return Results.NoContent();
            });

            group.MapGet("/name/{name}", async (string name, PokemonStoreContext dbContext) =>
            {

                var matchingPokemon = await (dbContext.Pokemons
                .Include(pokemon => pokemon.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(pokemon => EF.Functions.ILike(pokemon.Name, name))
                );

                if (matchingPokemon is null)
                {
                    return Results.NotFound();
                }



                return Results.Ok(
                    new PokemonSummaryDto
                (
                    matchingPokemon.Id,
                    matchingPokemon.Name,
                    matchingPokemon.Type.Name,
                    matchingPokemon.Hp,
                    matchingPokemon.Attack,
                    matchingPokemon.Defense,
                    matchingPokemon.ImageUrl
                ));
            });

        }
    }
}