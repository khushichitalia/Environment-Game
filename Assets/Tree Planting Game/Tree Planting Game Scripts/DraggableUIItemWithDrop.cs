using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUIItemWithDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject treePrefab; // Assign in Inspector

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
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
        // Get drop location in world space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0; // Force tree onto visible layer

        // Raycast to find dirt patch
        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if(hit != null && hit.CompareTag("Plantable"))
        {
            DirtPatch patch = hit.GetComponent<DirtPatch>();
            if (patch != null && !patch.hasTree)
            {
                Instantiate(treePrefab, hit.transform.position, Quaternion.identity);
                patch.hasTree = true;

                // ➕ Add planting points
                ScoreManager.Instance?.AddPoints(5);
            }
        }

        // Always reset seed to inventory position (even if drop was valid)
        rectTransform.localPosition = originalPosition;
        if(canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
