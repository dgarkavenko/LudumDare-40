using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    public CatSpawner Spawner;
    public SphereCollider Collider;
    private bool spawned = false;
    public bool Instant = false;
    
    private void Start()
    {
        if (Instant)
        {
            enabled = false;
            Spawner.SpawnCat(transform.position, drowning: true);

        }
        
        Collider = gameObject.GetComponent<SphereCollider>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (Instant)
            return;
        
        if(spawned)
            return;
        
        Spawner.SpawnCat(transform.position, drowning: true);
        spawned = true;
    }

}
