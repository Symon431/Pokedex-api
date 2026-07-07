using Pokedex.Api.Dtos;

namespace Pokedex.Api.Endpoints
{
    public static class PokemonsEndpoints
    {
        const string GETpokemonEndpointName = "GETpokemon";
        private static readonly List<PokemonDto> pokemons = [
            new(1, "Pikachu", "Electric", 35, 55, 40, "ksjlkljak"),
            new(2, "Charizard", "Fire/Flying", 78, 84, 78, "kljksl"),
            new(3, "Bulbasur", "Grass/Poison", 45, 49, 49, "ioijwl"),
            new(4, "Squirtle", "Water", 44, 48, 65, "klkjlaj"),
            new(5, "Gengar", "Ghost/Poison", 60, 65, 60, "lkjlja")
        ];

        public static void MapPokemonsEndpoints(this WebApplication app)
        {            

            var group = app.MapGroup("/pokemons");

            //GET/pokemon
            group.MapGet("/", () => pokemons);

            //Get/pokemon/id
            group.MapGet("/{id}", (int id)=> {
                var game = pokemons.Find(pokemon => pokemon.Id == id);

                return game is null ? Results.NotFound() : Results.Ok(game);

                }).WithName(GETpokemonEndpointName);

            //POST/pokemon
            group.MapPost("/", (CreatePokemonDto newPokemon) =>
            {
                PokemonDto pokemon = new(
                    pokemons.Count + 1,
                    newPokemon.Name,
                    newPokemon.Type,
                    newPokemon.HP,
                    newPokemon.Attack,
                    newPokemon.Defense,
                    newPokemon.ImageUrl
                );

                pokemons.Add(pokemon);

                return Results.CreatedAtRoute(GETpokemonEndpointName, new {id = pokemon.Id}, pokemon);
            });

            // PUT/pokemon/{id}
            group.MapPut("/{id}", (int id, UpdatePokemonDto updatePokemon) =>
            {
                var index = pokemons.FindIndex(pokemon => pokemon.Id == id);

                if(index == -1)
                {
                    return Results.NotFound();
                }

                pokemons[index] = new PokemonDto(
                    id,
                    updatePokemon.Name,
                    updatePokemon.Type,
                    updatePokemon.HP,
                    updatePokemon.Attack,
                    updatePokemon.Defense,
                    updatePokemon.ImageUrl
                );

                return Results.NoContent();

            });

            // DELETE/pokemon/{id}
            group.MapDelete("/{id}", (int id) => {
                
                pokemons.RemoveAll(pokemon => pokemon.Id == id);

                return Results.NoContent();
            });
        }
    }
}