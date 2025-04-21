using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private SpawnerScript spawner;

    void Start()
    {
        spawner = FindObjectOfType<SpawnerScript>();
    }

    public void TriggerRespawn()
    {
        if (spawner != null)
        {
            spawner.RespawnCollectibles();
        }
    }
}
