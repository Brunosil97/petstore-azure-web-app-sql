using System.ComponentModel.DataAnnotations;

namespace relational_pet_store.Models;

public class Dog
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Breed { get; set; } = string.Empty;

    public int Age { get; set; }

    [StringLength(20)]
    public string Size { get; set; } = string.Empty; // Small, Medium, Large

    [StringLength(20)]
    public string Color { get; set; } = string.Empty;

    public bool IsGoodWithKids { get; set; }

    public bool IsGoodWithOtherPets { get; set; }

    public int EnergyLevel { get; set; } // 1-10 scale

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<DogList> DogLists { get; set; } = new List<DogList>();
}