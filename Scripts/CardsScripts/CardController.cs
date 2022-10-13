using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 offset;
    private Camera cam;

    private Vector2 startPosition;

    private void Awake()
    {
        cam = Camera.main;
        startPosition = transform.position;
    }

    /// <summary>
    /// Передвигает ресурс
    /// </summary>
    private void MoveCard()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition) + offset;
        pos.z = 0;
        transform.position = pos;
    }

    // Срабатывает, когда игрок свайпает ячейку (возникает раз в тот кадр, перед которым было передвижение пальца)
    public void OnDrag(PointerEventData eventData)
    {
        MoveCard();
    }

    // Срабатывает в начале свапа
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Срабатывает в конце свапа
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x - startPosition.x < -1)
        {
            InventoryManager.Instance.AddResource(GetComponent<CardInfo>().resource);
            transform.parent.GetComponent<DeckScript>().DestroyCard(gameObject);
            return;
        }
        transform.position = startPosition;
    }
}
