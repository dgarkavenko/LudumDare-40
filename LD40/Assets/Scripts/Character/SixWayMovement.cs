using System;
using UnityEngine;

public class SixWayMovement : MonoBehaviour
{
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _upLeftSprite;
    [SerializeField] private Sprite _upRightSprite;

    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _downLeftSprite;
    [SerializeField] private Sprite _downRightSprite;

    public Sprite GetSprite(bool faceUp, float x)
    {
        if (faceUp)
        {
            if (x > 0)
            {
                return _upRightSprite;
            }
            if (x == 0)
            {
                return _upSprite;
            }
            if (x < 0)
            {
                return _upLeftSprite;
            }
        }
        else
        {
            if (x > 0)
            {
                return _downRightSprite;
            }
            if (x == 0)
            {
                return _downSprite;
            }
            if (x < 0)
            {
                return _downLeftSprite;
            }
        }

        throw new InvalidOperationException();
    }
}