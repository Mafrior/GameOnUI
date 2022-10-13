using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
///  Класс, который хранит и управляет всей информацией об ячейке
/// </summary>
public class CellScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public InventoryItem itemInfo; // Вся информация об хранящемся объекте
    public Text countOfResources; // Количество ресурсов или крафтов в инвентаре
    [SerializeField]

    private Camera cam;
    private Canvas canvas;

    private Transform draggingObject;
    private bool isDragging;

    private void Awake()
    {
        GetComponentInChildren<Text>().text = itemInfo.Name;
        cam = Camera.main;

        draggingObject = cam.transform.GetChild(0);

        canvas = transform.parent.GetComponent<Canvas>();

        canvas.worldCamera = cam;
        canvas.overrideSorting = true;
        if (canvas.transform.parent.CompareTag("ForgingItems"))
        {
            canvas.sortingLayerName = "ForgingCell"; return;
        }
        canvas.sortingLayerName = "Cells";
    }

    /// <summary>
    /// Передвигает ресурс
    /// </summary>
    private void MoveCard()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        draggingObject.position = pos;
    }

    // Срабатывает, когда игрок свайпает ячейку (возникает раз в тот кадр, перед которым было передвижение пальца)
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            MoveCard();
        }
    }

    // Срабатывает в начале свапа
    public void OnBeginDrag(PointerEventData eventData)
    {
        int count = Convert.ToInt32(countOfResources.text);
        DragItem(true);
        if (count == 1)
        {
            SetCellVisual(false);
        }
        countOfResources.text = $"{--count}";
    }

    // Срабатывает в конце свапа
    public void OnEndDrag(PointerEventData eventData)
    {
        int count = Convert.ToInt32(countOfResources.text);
        if (!draggingObject.gameObject.activeSelf)
        {
            if (count == 0)
            {
                InventoryManager.Instance.RemoveItem(this);
            }
            return;
        }
        if (draggingObject.GetComponent<DraggingObjectScript>().slot != null)
        {
            if (count == 0)
            {
                InventoryManager.Instance.RemoveItem(this);
            }
            draggingObject.GetComponent<DraggingObjectScript>().slot.SetResource((ResourceObject)itemInfo);
            draggingObject.gameObject.SetActive(false);
            return;
        }
        countOfResources.text = $"{++count}";
        SetCellVisual(true);

        DragItem(false);

    }

    private void SetCellVisual(bool isVisible)
    {
        GetComponent<Image>().enabled = isVisible;
        for (int i = 0; i < transform.childCount; i++)
        {
            Image image = transform.GetChild(i).GetComponent<Image>();
            if (image != null)
            {
                image.enabled = isVisible;
                continue;
            }
            transform.GetChild(i).GetComponent<Text>().enabled = isVisible;
        }
    }

    /// <summary>
    /// Метод устанавливаеет состояние перетаскиваемого объекта (убирает или включает)
    /// </summary>
    void DragItem(bool dragging)
    {
        if (dragging)
        {
            draggingObject.GetComponent<DraggingObjectScript>().SetProperties(gameObject);
        }
        draggingObject.gameObject.SetActive(dragging);
        isDragging = dragging;
    }
}
