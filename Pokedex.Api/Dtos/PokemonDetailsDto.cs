namespace Pokedex.Api.Dtos
{
    public record PokemonDetailsDto(

        int Id,
        string Name,
        int TypeId,
        int HP, //Health Points
        int Attack, //Atack Strength
        int Defense, //Defense Strength
        string ImageUrl
    );
}