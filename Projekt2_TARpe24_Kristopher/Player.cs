using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt2_TARpe24_Kristopher;

public class Player
{
    public string Name { get; set; } = "Mängija";
    public int HighScore { get; set; } = 0;

    public void CheckHighScore(int currentScore)
    {
        if (currentScore > HighScore)
            HighScore = currentScore;
    }
}