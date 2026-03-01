using System.Numerics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameView _gameView;
    [SerializeField] private int _maxMoves = 5;
    [SerializeField] private UnityEngine.Vector2 gridSize = new(5, 6);

    private BlockView[,] _blocks;
    private int _moves;
    private int _score;

    void Start()
    {
        _gameView.OnSimulateButtonClicked += HandleTurn;
        _gameOverView.OnRestartButtonClicked += StartNewGame;
        _gameView.Init();
        _gameOverView.Init();
        StartNewGame();
    }

    private void StartNewGame()
    {
        _moves = _maxMoves;
        _score = 0;
        _gameView.UpdateMoves(_moves);
        _gameView.UpdateScore(_score);
        _gameOverView.Hide();
        InitializeGameGrid();
    }

    private void InitializeGameGrid() // creating the grid, and instantiating the block views in the grid
    {
        _gameView.ClearGrid();
        _blocks = new BlockView[(int)gridSize.x, (int)gridSize.y];
        for (int y = 0; y < (int)gridSize.y; y++)
        {
            for (int x = 0; x < (int)gridSize.x; x++)
            {
                var block = _gameView.CreateBlock();    
                _blocks[x, y] = block;
                var capturedX = x;
                var capturedY = y;
                block.OnClicked += () => StartCoroutine(HandleBlockClicked(capturedX,capturedY));
            }
        }
    }

    IEnumerator HandleBlockClicked(int x, int y)
    {

        var visited = new List<BlockView>();
        var clickedBlocks = FindAdjacentBlocks(x,y,ref visited );
        if (clickedBlocks != null)
        {        
            foreach (var clickedBlock in clickedBlocks)
            {
                clickedBlock.SetHidden(true);
            }
            _score += clickedBlocks.Count;
            _gameView.UpdateScore(_score);
        }
        yield return HandleBlocksFalling(clickedBlocks);
        yield return new WaitForSeconds(0.1f);
        HandleTurn();
    }
        IEnumerator HandleBlocksFalling(List<BlockView> clickedBlocks)
    {
        yield return new WaitForSeconds(0.1f); 
        for (int x = 0; x < (int)gridSize.x; x++)
        {
            int emptySpaces = 0;
            for (int y = 0; y < (int)gridSize.y; y++)
            {
                if (_blocks[x, y].IsHidden())
                {
                    emptySpaces++;
                }
                else if (emptySpaces > 0)
                {
                    _blocks[x, y - emptySpaces].SetSprite(_blocks[x, y].GetSpriteIndex());
                    _blocks[x, y - emptySpaces].SetHidden(false);
                    _blocks[x, y].SetHidden(true);
                    yield return new WaitForSeconds(0.1f); 

                }
            }
            for (int i = 0; i < emptySpaces; i++)
            {
                _blocks[x, (int)gridSize.y - 1 - i].SetRandomSprite();
            }
        }

        yield return null;
    }

    List<BlockView> FindAdjacentBlocks(int x, int y, ref List<BlockView> visited )
    {
        UnityEngine.Debug.Assert(x >= 0 && x < (int)gridSize.x && y >= 0 && y < (int)gridSize.y, $"Position ({x},{y}) is out of bounds");
        var block = _blocks[x, y];
        
        if (visited == null)
        {
            visited = new();
        }
        else if (visited.Contains(block))
        {
            return new();
        }

        visited.Add(block);
        var list = new List<BlockView>{block};
        var adjacentPositions = new (int x, int y)[] { (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) };
        foreach (var pos in adjacentPositions)
        {
            if (pos.x >= 0 && pos.x < (int)gridSize.x && pos.y >= 0 && pos.y < (int)gridSize.y)
            {
                var adjacentBlock = _blocks[pos.x, pos.y];
                if (adjacentBlock.IsSameType(block))
                {
                    list.AddRange(FindAdjacentBlocks(pos.x, pos.y, ref visited));
                }
            }
        }
        return list;
    }
    void HandleTurn()
    {
        _moves--;
        _score += 10;
        _gameView.UpdateMoves(_moves);
        _gameView.UpdateScore(_score);
        if (_moves <= 0) // lost the game, out of moves
        {
            GameOver();
        }
    }       
    public void GameOver()
    {
        _gameOverView.Show();
    }
}
