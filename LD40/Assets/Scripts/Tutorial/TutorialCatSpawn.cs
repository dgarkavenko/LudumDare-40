using System;
using UnityEngine;

public class TutorialCatSpawn : CatSpawn
{
    [SerializeField] private float _hangTime;

    public Action OnCatPicked;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        Spawner.SpawnCat(transform.position, true, OnCatPicked, _hangTime);
    }
}