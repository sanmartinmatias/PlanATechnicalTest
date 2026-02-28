using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameOverView : MonoBehaviour
{
   [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _restartButton;
    public event Action OnRestartButtonClicked;
    public void Init()
    {
        _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
