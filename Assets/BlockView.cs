using UnityEngine;
using UnityEngine.UI;
using System;


public class BlockView : MonoBehaviour
{
    public event Action OnClicked;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _blockSprites;
    void Start()
    {
        // Initialize the block view with a random sprite
        int randomIndex = UnityEngine.Random.Range(0, _blockSprites.Length - 1);
        _image.sprite = _blockSprites[randomIndex];
    }
    public void SetHidden(bool hidden)
    {
        _image.enabled = !hidden;
    }

    public void SetSprite(int spriteIndex)
    {
        _image.sprite = _blockSprites[spriteIndex];
    }

    private void OnMouseDown()
    {
        OnClicked?.Invoke();
    }
    
}
