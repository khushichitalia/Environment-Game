using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
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

        if(canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent.GetComponent<RectTransform>(), 
            eventData.position, 
            canvas.worldCamera, 
            out localPointerPosition
        );

        rectTransform.localPosition = localPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.localPosition = originalPosition;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}