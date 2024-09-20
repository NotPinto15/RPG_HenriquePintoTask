using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandleUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _rectTransform;

    private RectTransform _canvasTransform;
    private Vector2 _beginDragOffset;

    private void Awake()
    {
        _canvasTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out _beginDragOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPointerPosition))
        {
            _rectTransform.localPosition = localPointerPosition - _beginDragOffset;
        }
    }
}
