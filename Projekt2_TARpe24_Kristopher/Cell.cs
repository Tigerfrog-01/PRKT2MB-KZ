namespace Projekt2_TARpe24_Kristopher;

public class Cell
{
    private int value;
    public int GetValue() => value;
    public void SetValue(int val) => value = val;
    public bool IsZeroValue() => value == 0;
    public void SetZeroValue() => value = 0;
}
