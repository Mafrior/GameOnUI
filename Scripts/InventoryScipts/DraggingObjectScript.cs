using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Перетаскиваемый объект, ему передаём все необходимые данные, чтобы игрок понимал, что перетаскивает
/// </summary>
public class DraggingObjectScript : MonoBehaviour
{
    private InventoryItem resource;
    private GameObject cellObject;
    public SlotScript slot = null;

    // Обрабатывает информацию, когда колайдер перетаскиваемого объекта сталкивается с другим каким-то объектом
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HornScript horn = collision.GetComponent<HornScript>();
        if (horn != null)
        {
            horn.AddCraftingResource(resource as ResourceObject);
            gameObject.SetActive(false);
            return;
        }
        slot = collision.GetComponent<SlotScript>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("slot"))
        {
            if (slot == other.GetComponent<SlotScript>())
            {
                slot = null;
            }
        }
    }

    public void SetProperties(GameObject gameobj)
    {
        cellObject = gameobj;
        resource = gameobj.GetComponent<CellScript>().itemInfo;
    }
}
