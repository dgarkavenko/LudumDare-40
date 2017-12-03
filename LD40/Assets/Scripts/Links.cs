using System;
using UnityEngine;

public class Links : MonoBehaviour
{
    public static Links Instance;
    public Sprite CatDraggedSprite;
    public Cat[] Cats;

    private void Awake()
    {
        if (Instance != null)
            throw new Exception("duplicate links object");

        Instance = this;
    }

    public FightView FightView;

}
