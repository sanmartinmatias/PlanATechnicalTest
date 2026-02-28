using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameView : MonoBehaviour
{
    [SerializeField] private BlockView _blockViewPrefab;
    [SerializeField] private Transform _gameGrid;

    [SerializeField] private TMP_Text _scoreLabel;
    [SerializeField] private TMP_Text _movesLabel;
    [SerializeField] private Button _simulateButton;


    public event Action OnSimulateButtonClicked;

    public void Init()
    {
        _simulateButton.onClick.AddListener(() => OnSimulateButtonClicked?.Invoke());
    }
    public void UpdateScore(int score)
    {
        _scoreLabel.text = $"{score}";
    }
    public void UpdateMoves(int moves)
    {
        _movesLabel.text = $"{moves}";
    }

    public BlockView CreateBlock()
    {
        BlockView block = Instantiate(_blockViewPrefab, _gameGrid);
        return block;
    }
    public void ClearGrid()
    {
        for (int i = _gameGrid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_gameGrid.transform.GetChild(i).gameObject);
        }
    }
}
