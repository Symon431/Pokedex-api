using System.ComponentModel.DataAnnotations;

namespace Pokedex.Api.Dtos
{
    public record UpdatePokemonDto(
        [Required][StringLength(50)]string Name,
        [Range(1,50)]int TypeId,
        [Required][Range(1,100)]int HP, //Health Points
        [Required][Range(1,100)]int Attack, //Atack Strength
        [Required][Range(1,100)]int Defense, //Defense Strength
        string? ImageUrl

    );
}