using UnityEngine;

public class SixWayMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _upLeftSprite;
    [SerializeField] private Sprite _upRightSprite;

    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _downLeftSprite;
    [SerializeField] private Sprite _downRightSprite;

    public void SetSprite(bool faceUp, float x)
    {
        if (faceUp)
        {
            if (x > 0)
            {
                _spriteRenderer.sprite = _upRightSprite;
            }
            else if (x == 0)
            {
                _spriteRenderer.sprite = _upSprite;
            }
            else if (x < 0)
            {
                _spriteRenderer.sprite = _upLeftSprite;
            }
        }
        else
        {
            if (x > 0)
            {
                _spriteRenderer.sprite = _downRightSprite;
            }
            else if (x == 0)
            {
                _spriteRenderer.sprite = _downSprite;
            }
            else if (x < 0)
            {
                _spriteRenderer.sprite = _downLeftSprite;
            }
        }

//        if (z > 0 && x == 0)
//        {
//            _spriteRenderer.sprite = _upSprite;
//        }
//        else if (z > 0 && x > 0)
//        {
//            _spriteRenderer.sprite = _upRightSprite;
//        }
//        else if (z == 0 && x > 0)
//        {
//            _spriteRenderer.sprite = _rightSprite;
//        }
//        else if (z < 0 && x > 0)
//        {
//            _spriteRenderer.sprite = _downRightSprite;
//        }
//        else if (z < 0 && x == 0)
//        {
//            _spriteRenderer.sprite = _downSprite;
//        }
//        else if (z < 0 && x < 0)
//        {
//            _spriteRenderer.sprite = _downLeftSprite;
//        }
//        else if (z == 0 && x < 0)
//        {
//            _spriteRenderer.sprite = _leftSprite;
//        }
//        else if (z > 0 && x < 0)
//        {
//            _spriteRenderer.sprite = _upLeftSprite;
//        }
    }
}