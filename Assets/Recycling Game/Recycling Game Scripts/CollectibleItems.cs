using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private SpawnerScript spawner;
    private bool triggeredRespawn = false;
    private Renderer itemRenderer;

    void Start()
    {
        spawner = FindObjectOfType<SpawnerScript>();
        itemRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!triggeredRespawn && itemRenderer != null && itemRenderer.material.color == Color.red)
        {
            TriggerRespawn();
        }
    }

    public void TriggerRespawn()
    {
        if (triggeredRespawn) return;
        triggeredRespawn = true;

        if (spawner != null)
        {
            spawner.RespawnCollectibles();
        }
    }
}
