using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameView _gameView;
    [SerializeField] private int _maxMoves = 5;
    private BlockView[,] _blocks;
    private int _moves;
    private int _score;

    void Start()
    {
        _gameView.OnSimulateButtonClicked += HandleSimulateButtonClicked;
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
        _blocks = new BlockView[5, 6];
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                _blocks[x, y] = _gameView.CreateBlock();
            }
        }
    }

    void HandleSimulateButtonClicked()
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
