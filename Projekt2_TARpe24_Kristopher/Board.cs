using System;
using System.Collections.Generic;

namespace Projekt2_TARpe24_Kristopher;

public class Board
{
    private const int BOARD_SIZE = 4;
    public Cell[,] gameBoard;
    private Random random = new Random();
    private int cellAddValue = 0;

    public Board()
    {
        gameBoard = new Cell[BOARD_SIZE, BOARD_SIZE];
        ResetBoard();
    }

    public int GetScore() => cellAddValue;

    public void ResetBoard()
    {
        cellAddValue = 0;
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                gameBoard[i, j] = new Cell();
            }
        }
        AddNewField();
        AddNewField();
    }

    public void AddNewField()
    {
        if (IsGridFull()) return;
        bool notValid = true;
        while (notValid)
        {
            int row = random.Next(0, BOARD_SIZE);
            int column = random.Next(0, BOARD_SIZE);
            if (gameBoard[row, column].IsZeroValue())
            {
                gameBoard[row, column].SetValue(random.Next(10) < 9 ? 2 : 4);
                notValid = false;
            }
        }
    }

    public bool IsGridFull()
    {
        foreach (var cell in gameBoard) if (cell.IsZeroValue()) return false;
        return true;
    }

    public bool Move(string direction)
    {
        bool changed = direction switch
        {
            "Up" => MoveCellsLeft(),
            "Down" => MoveCellsRight(),
            "Left" => MoveCellsUp(),
            "Right" => MoveCellsDown(),
            _ => false
        };

        if (changed) AddNewField();
        return changed;
    }

    private bool MoveCellsUp()
    {
        bool occupied = false;
        if (MoveCellsUpLoop()) occupied = true;
        for (int r = 0; r < BOARD_SIZE; r++)
            for (int c = 0; c < (BOARD_SIZE - 1); c++)
                occupied = CombineCells(r, c + 1, r, c, occupied);
        if (MoveCellsUpLoop()) occupied = true;
        return occupied;
    }

    private bool MoveCellsUpLoop()
    {
        bool occupied = false;
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            bool moved;
            do
            {
                moved = false;
                for (int c = 0; c < (BOARD_SIZE - 1); c++)
                    if (MoveCell(r, c + 1, r, c)) { moved = true; occupied = true; }
            } while (moved);
        }
        return occupied;
    }

    private bool MoveCellsDown()
    {
        bool occupied = false;
        if (MoveCellsDownLoop()) occupied = true;
        for (int r = 0; r < BOARD_SIZE; r++)
            for (int c = BOARD_SIZE - 1; c > 0; c--)
                occupied = CombineCells(r, c - 1, r, c, occupied);
        if (MoveCellsDownLoop()) occupied = true;
        return occupied;
    }

    private bool MoveCellsDownLoop()
    {
        bool occupied = false;
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            bool moved;
            do
            {
                moved = false;
                for (int c = BOARD_SIZE - 1; c > 0; c--)
                    if (MoveCell(r, c - 1, r, c)) { moved = true; occupied = true; }
            } while (moved);
        }
        return occupied;
    }

    private bool MoveCellsLeft()
    {
        bool occupied = false;
        if (MoveCellsLeftLoop()) occupied = true;
        for (int c = 0; c < BOARD_SIZE; c++)
            for (int r = 0; r < (BOARD_SIZE - 1); r++)
                occupied = CombineCells(r + 1, c, r, c, occupied);
        if (MoveCellsLeftLoop()) occupied = true;
        return occupied;
    }

    private bool MoveCellsLeftLoop()
    {
        bool occupied = false;
        for (int c = 0; c < BOARD_SIZE; c++)
        {
            bool moved;
            do
            {
                moved = false;
                for (int r = 0; r < (BOARD_SIZE - 1); r++)
                    if (MoveCell(r + 1, c, r, c)) { moved = true; occupied = true; }
            } while (moved);
        }
        return occupied;
    }

    private bool MoveCellsRight()
    {
        bool occupied = false;
        if (MoveCellsRightLoop()) occupied = true;
        for (int c = 0; c < BOARD_SIZE; c++)
            for (int r = BOARD_SIZE - 1; r > 0; r--)
                occupied = CombineCells(r - 1, c, r, c, occupied);
        if (MoveCellsRightLoop()) occupied = true;
        return occupied;
    }

    private bool MoveCellsRightLoop()
    {
        bool occupied = false;
        for (int c = 0; c < BOARD_SIZE; c++)
        {
            bool moved;
            do
            {
                moved = false;
                for (int r = BOARD_SIZE - 1; r > 0; r--)
                    if (MoveCell(r - 1, c, r, c)) { moved = true; occupied = true; }
            } while (moved);
        }
        return occupied;
    }

    private bool MoveCell(int x1, int y1, int x2, int y2)
    {
        if (!gameBoard[x1, y1].IsZeroValue() && gameBoard[x2, y2].IsZeroValue())
        {
            gameBoard[x2, y2].SetValue(gameBoard[x1, y1].GetValue());
            gameBoard[x1, y1].SetZeroValue();
            return true;
        }
        return false;
    }

    private bool CombineCells(int x1, int y1, int x2, int y2, bool occupied)
    {
        if (!gameBoard[x1, y1].IsZeroValue())
        {
            int val1 = gameBoard[x1, y1].GetValue();
            int val2 = gameBoard[x2, y2].GetValue();
            if (val1 == val2)
            {
                gameBoard[x2, y2].SetValue(val1 + val2);
                gameBoard[x1, y1].SetZeroValue();
                cellAddValue += (val1 + val2);
                return true;
            }
        }
        return occupied;
    }

    public bool IsGameOver()
    {
      
        for (int r = 0; r < BOARD_SIZE; r++)
            for (int c = 0; c < BOARD_SIZE; c++)
                if (gameBoard[r, c].GetValue() == 0) return false;

       
        for (int r = 0; r < BOARD_SIZE; r++)
            for (int c = 0; c < BOARD_SIZE - 1; c++)
                if (gameBoard[r, c].GetValue() == gameBoard[r, c + 1].GetValue()) return false;

    
        for (int r = 0; r < BOARD_SIZE - 1; r++)
            for (int c = 0; c < BOARD_SIZE; c++)
                if (gameBoard[r, c].GetValue() == gameBoard[r + 1, c].GetValue()) return false;

        return true;
    }

    public void ResetGame()
    {
   
        cellAddValue = 0;
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                gameBoard[r, c].SetValue(0);
            }
        }
 
        AddNewField();
        AddNewField();
    }
}