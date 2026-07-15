using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Data;
using Pokedex.Api.Dtos;
using Pokedex.Api.Models;

namespace Pokedex.Api.Endpoints
{
    public static class PokemonsTypeEndpoints
    {
        public static void MapPokemonsTypeEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("/types");

            // Get/types
            group.MapGet("/", async(PokemonStoreContext dbContext) =>
            await dbContext.PokemonTypes
                            .Select(Type => new PokemonTypeDto(Type.Id, Type.Name))
                            .AsNoTracking()
                            .ToListAsync()
                        
            );
        }


    }
}