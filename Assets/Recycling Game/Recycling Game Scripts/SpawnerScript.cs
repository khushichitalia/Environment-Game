using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles item spawning
public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> recyclingPrefabs;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private int organicItemPercentage;

    [SerializeField]
    private int recylingItemPercentage;

    void Start()
    {
        SpawnCollectibles();
    }

    public void SpawnCollectibles()
    {

        Dictionary<GameObject, bool> hasItemSpawned = new Dictionary<GameObject, bool>();

        foreach (GameObject item in recyclingPrefabs)
        {
            hasItemSpawned[item] = false;
        }

        // shuffle the spawn points
        for (int i = 0; i < spawnPoints.Count - 3; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count - 3);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }

        // spawn items at each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {

            // get a list of available items that can still be spawned
            List<GameObject> availableItems = new List<GameObject>();

            GameObject selectedPrefab;

            foreach (var item in recyclingPrefabs)
            {
                if (!hasItemSpawned[item])
                {
                    availableItems.Add(item);
                }
            }

            if (availableItems.Count > 0)
            {

                // randomly select an available item
                if(availableItems.Count > 3) {
                    int randomIndex = Random.Range(0, availableItems.Count - 3);
                    selectedPrefab = availableItems[randomIndex];
                }

                else
                {
                    int randomIndex = Random.Range(0, availableItems.Count);
                    selectedPrefab = availableItems[randomIndex];
                }
                    
                if(selectedPrefab!=null) {
                    // only spawn powerups with a 20% chance
                    if(selectedPrefab.tag == "Powerup")
                    {
                        if((Random.Range(0, 100)) > organicItemPercentage) {
                            hasItemSpawned[selectedPrefab] = true;
                            continue;
                        }
                    }

                    // only spawn powerups with a 20% chance
                    if(selectedPrefab.tag == "Penalty")
                    {
                        if((Random.Range(0, 100)) > recylingItemPercentage) {
                            hasItemSpawned[selectedPrefab] = true;
                            continue;
                        }
                    }

                    // instantiate the selected collectible at the spawn point position
                    GameObject newItem = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);

                    Debug.Log($"Spawned {selectedPrefab.name} at {spawnPoint.position}");

                    // mark item as selected
                    hasItemSpawned[selectedPrefab] = true;
                }
            }
            else
            {
                Debug.Log("No more items available to spawn.");
            }
        }
    }

    // destroys old items and spawns in new ones
    public void RespawnCollectibles()
    {
        
        List<GameObject> itemsToBeDestroyed = new List<GameObject>();

        foreach(GameObject item in GameObject.FindGameObjectsWithTag("Organic Item"))
        {
            itemsToBeDestroyed.Add(item);
        }

        foreach(GameObject item in GameObject.FindGameObjectsWithTag("Recycling Item"))
        {
            itemsToBeDestroyed.Add(item);
        }   

        foreach(GameObject item in itemsToBeDestroyed)
        {
            if(item.name.Contains("(Clone)"))
            {
                Destroy(item);
            }
        }

       SpawnCollectibles();
    }
}