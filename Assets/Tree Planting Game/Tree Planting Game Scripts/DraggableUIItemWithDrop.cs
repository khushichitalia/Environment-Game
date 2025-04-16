using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUIItemWithDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject treePrefab;

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
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if(hit != null && hit.CompareTag("Plantable"))
        {
            DirtPatch patch = hit.GetComponent<DirtPatch>();
            if (patch != null && !patch.hasTree)
            {
                Instantiate(treePrefab, hit.transform.position, Quaternion.identity);
                patch.hasTree = true;

                ScoreManager.Instance?.AddPoints(5);
            }
        }

        rectTransform.localPosition = originalPosition;
        if(canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
