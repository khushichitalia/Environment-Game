using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingTrashBinScript : MonoBehaviour
{
    private Color wrongColor = Color.red;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("🔥 Trigger Entered! Object: " + other.gameObject.name);
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Recycling Item")) 
        {
            Destroy(other.gameObject); 
            Debug.Log("Destroyed: " + other.gameObject.name);
        }

        else {
            Renderer itemRenderer = other.gameObject.GetComponent<Renderer>();
            if (itemRenderer != null)
            {
                itemRenderer.material.color = wrongColor;
                Debug.Log("Wrong Item: " + other.gameObject.name);
            }
        }
    }
}
