namespace Projekt2_TARpe24_Kristopher;

public class Theme
{
    public string Name { get; set; }
    public Color BackgroundColor { get; set; }
    public Color EmptyTileColor { get; set; }
    public Color TextColor { get; set; }

    public void Apply(ContentPage page)
    {
        page.BackgroundColor = this.BackgroundColor;
    }

    public Color GetTileColor(int val)
    {
      
        if (val == 0) return EmptyTileColor;

        if (val == 4) return Colors.Yellow;

        if (val == 8) return Colors.Orange;

        if (val == 16) return Colors.Red;

        if (val == 32) return Colors.Violet;

        if (val == 64) return Colors.Purple;

        if (val == 128) return Colors.Green;

        if (val == 256) return Colors.Gold;

        return Colors.White; 
    }
}