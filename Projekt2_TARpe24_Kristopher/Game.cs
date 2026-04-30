using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt2_TARpe24_Kristopher;

public class Game
{
    public Board Board { get; private set; }
    public int CurrentScore => Board.GetScore();

    public Game()
    {
        Board = new Board();
    }

    public bool ProcessMove(string direction)
    {
        return Board.Move(direction);
    }

    public void Restart()
    {
        Board.ResetBoard();
    }
}
