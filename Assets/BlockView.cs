using UnityEngine;
using UnityEngine.UI;
using System;


public class BlockView : MonoBehaviour
{
    public event Action OnClicked;
    public Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _blockSprites;
    void Start()
    {
        // Initialize the block view with a random sprite
        int randomIndex = UnityEngine.Random.Range(0, _blockSprites.Length - 1);
        _image.sprite = _blockSprites[randomIndex];
        _button.onClick.AddListener(() => OnClicked?.Invoke());
    }
    public void SetHidden(bool hidden)
    {
        _image.enabled = !hidden;
    }

    public void SetSprite(int spriteIndex)
    {
        _image.sprite = _blockSprites[spriteIndex];
    }

    public bool IsHidden()
    {
        return !_image.enabled;
    }

    public void SetRandomSprite()
    {
        int randomIndex = UnityEngine.Random.Range(0, _blockSprites.Length - 1);
        _image.sprite = _blockSprites[randomIndex];
        SetHidden(false);
    }

    public bool IsSameType(BlockView other)
    {
        return _image.sprite == other._image.sprite;
    }

    public int GetSpriteIndex()
    {
        for (int i = 0; i < _blockSprites.Length; i++)
        {
            if (_image.sprite == _blockSprites[i])
            {
                return i;
            }
        }
        return -1; // Not found
    }
    public void SetSpriteIndexx(int spriteIndex)
    {
        if (spriteIndex >= 0 && spriteIndex < _blockSprites.Length)
        {
            _image.sprite = _blockSprites[spriteIndex];
        }
    }
}
