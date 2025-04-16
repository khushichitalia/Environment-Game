using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWateringCan : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    public GameObject waterSplashPrefab;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.localPosition;
        if (canvasGroup != null) canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition
        );

        rectTransform.localPosition = localPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit != null && hit.CompareTag("Tree"))
        {
            TreeGrowth tree = hit.GetComponent<TreeGrowth>();
            if (tree != null)
            {
                tree.WaterTree();
                ScoreManager.Instance?.AddPoints(2);
                Debug.Log("Tree has been watered!");

                if (waterSplashPrefab != null)
                {
                    Instantiate(waterSplashPrefab, hit.transform.position, Quaternion.identity);
                }
            }
        }

        rectTransform.localPosition = originalPosition;
        if (canvasGroup != null) canvasGroup.alpha = 1f;
    }
}