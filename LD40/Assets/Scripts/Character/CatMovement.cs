using UnityEngine;

public class CatMovement : SixWayMovement
{
    [SerializeField] private Sprite _hangingUpSprite;
    [SerializeField] private Sprite _hangingDownSprite;
    [SerializeField] private Sprite _drowningSprite;

    public Sprite GetSprite(bool faceUp, float x, Cat.CatState state)
    {
        if (state is Cat.Walking)
            return base.GetSprite(faceUp, x);
        if (state is Cat.Hanging)
            return faceUp ? _hangingUpSprite : _hangingDownSprite;
        if (state is Cat.Fighting)
            return Links.Instance.CatDraggedSprite;
        if (state is Cat.Drowning)
            return _drowningSprite;
        if (state is Cat.Flying)
            return base.GetSprite(faceUp: false, x: -1f);

        return null;
    }
}