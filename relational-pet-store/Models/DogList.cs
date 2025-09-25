namespace relational_pet_store.Models;

public class DogList
{
    public int DogId { get; set; }
    public Dog Dog { get; set; } = null!;

    public int NamedListId { get; set; }
    public NamedList NamedList { get; set; } = null!;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}