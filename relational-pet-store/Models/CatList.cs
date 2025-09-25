namespace relational_pet_store.Models;

public class CatList
{
    public int CatId { get; set; }
    public Cat Cat { get; set; } = null!;

    public int NamedListId { get; set; }
    public NamedList NamedList { get; set; } = null!;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}