using UnityEngine;

public class CatMovement : SixWayMovement
{
    [SerializeField] private Sprite _hangingSprite;
    [SerializeField] private Sprite _drowningSprite;

    public Sprite GetSprite(bool faceUp, float x, Cat.CatState state)
    {
        if (state is Cat.Walking)
            return base.GetSprite(faceUp, x);
        if (state is Cat.Hanging)
            return _hangingSprite;
        if (state is Cat.Fighting)
            return Links.Instance.CatDraggedSprite;
        if (state is Cat.Drowning)
            return _drowningSprite;

        return null;
    }
}