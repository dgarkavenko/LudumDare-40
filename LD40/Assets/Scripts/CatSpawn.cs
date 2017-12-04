using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    public CatSpawner Spawner;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        Spawner.SpawnCat(transform.position, drowning: true);
    }
}
