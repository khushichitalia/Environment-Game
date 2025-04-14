using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public Sprite[] healthyStages;
    public Sprite[] dryStages;
    public float timePerStage = 5f;

    private int currentStage = 0;
    private float timer = 0f;
    private bool isWatered = false;
    private int waterCount = 0; // how many times the tree was watered
    private bool bonusGiven = false;
    private SpriteRenderer spriteRenderer;

    private float plantedYPosition; // the dirt patch center Y

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = healthyStages[0];

        // Store the spawn Y position once
        plantedYPosition = transform.position.y;

        AlignBottomToPlantedY();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerStage)
        {
            timer = 0f;

            if (isWatered && currentStage < healthyStages.Length - 1)
            {
                currentStage++;
                spriteRenderer.sprite = healthyStages[currentStage];
                isWatered = false;
                AlignBottomToPlantedY();

                // ➕ Check for fully grown healthy bonus
                if (IsFullyGrown() && !bonusGiven)
                {
                    ScoreManager.Instance?.AddPoints(10);
                    bonusGiven = true;
                }
            }
            else if (!isWatered && currentStage < dryStages.Length - 1)
            {
                currentStage++;
                spriteRenderer.sprite = dryStages[currentStage];
                AlignBottomToPlantedY();
            }

            // Reset watering flag regardless
            isWatered = false;
        }
    }


    public void WaterTree()
    {
        isWatered = true;
        waterCount++;
    }


    private void AlignBottomToPlantedY()
    {
        float spriteHeight = spriteRenderer.bounds.size.y;
        Vector3 newPosition = transform.position;
        newPosition.y = plantedYPosition + (spriteHeight / 2f);
        transform.position = newPosition;
    }

    public int GetWaterCount()
    {
        return waterCount;
    }

    public bool IsHealthy()
    {
        return currentStage < healthyStages.Length && isWatered;
    }

    public bool IsFullyGrown()
    {
        return currentStage == healthyStages.Length - 1;
    }
}
