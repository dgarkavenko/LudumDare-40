using System;
using UnityEngine;

public class Links : MonoBehaviour
{
    public static Links Instance;
    public Sprite CatWalkingSprite;
    public Sprite CatHangingSprite;
    public Sprite CatDraggedSprite;
    public Cat Cat;

    private void Awake()
    {
        if (Instance != null)
            throw new Exception("duplicate links object");

        Instance = this;
    }

    public FightView FightView;

}
