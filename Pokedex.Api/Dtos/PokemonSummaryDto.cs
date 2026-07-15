namespace Pokedex.Api.Dtos
{
    public record PokemonSummaryDto(

        int Id,
        string Name,
        string Type,
        int HP, //Health Points
        int Attack, //Atack Strength
        int Defense, //Defense Strength
        string ImageUrl
    );
}