using SQLite;

namespace Projekt2_TARpe24_Kristopher.Models;

public class Place
{
    [PrimaryKey]
    public string Key { get; set; } = string.Empty;
    public string CategoryKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string FullDescription { get; set; } = string.Empty;
    public bool IsFavorite { get; set; }

    public Place Copy()
    {
        return new Place
        {
            Key = Key,
            CategoryKey = CategoryKey,
            Name = Name,
            ImageName = ImageName,
            ShortDescription = ShortDescription,
            FullDescription = FullDescription,
            IsFavorite = IsFavorite
        };
    }
}
