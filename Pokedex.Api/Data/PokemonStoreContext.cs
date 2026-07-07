using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Models;
using PokemonType = Pokedex.Api.Models.PokemonType;

namespace Pokedex.Api.Data
{
    public class PokemonStoreContext(DbContextOptions<PokemonStoreContext> options)
    : DbContext(options)
    {
        public DbSet<Pokemon> Pokemons => Set<Pokemon>();

        public DbSet<PokemonType> PokemonTypes => Set<PokemonType>();

    }
}