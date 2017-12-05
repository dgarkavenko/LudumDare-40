using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    public CatSpawner Spawner;
    public SphereCollider Collider;
    private bool spawned = false;
    
    private void Start()
    {
        Collider = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(spawned)
            return;
        
        Spawner.SpawnCat(transform.position, drowning: true);
        spawned = true;
    }

}
