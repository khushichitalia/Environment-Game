using UnityEngine;

public class RecyclingTrashBinScript : MonoBehaviour
{
    private Color wrongColor = Color.red;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        var itemScript = other.GetComponent<CollectibleItem>();

        if (other.CompareTag("Recycling Item"))
        {
            itemScript?.TriggerRespawn();
            Destroy(other.gameObject);
            ScoreManager.Instance?.AddPoints(10);
            Debug.Log("Destroyed: " + other.name);
        }
        else if (other.CompareTag("Organic Item")) 
        {
            Renderer itemRenderer = other.GetComponent<Renderer>();
            if (itemRenderer != null)
            {
                itemRenderer.material.color = wrongColor;
                Debug.Log("Wrong Item: " + other.name);
            }
        }
    }
}
