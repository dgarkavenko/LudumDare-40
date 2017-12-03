using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    public CatSpawner Spawner;

    private void Start()
    {
        Spawner.SpawnCat(transform.position, drowning: true);
    }
}
