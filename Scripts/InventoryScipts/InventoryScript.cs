using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InventoryScript : MonoBehaviour
{
    [HideInInspector]
    public List<CellScript> items = new List<CellScript>(); // Список всех ячеек в инвентаре ресурсов
    [SerializeField]
    private Transform cellPrefab; // Префаб ячейки в инвентаре

    public void AddResource(InventoryItem item)
    {
        CellScript cell = FindEqualsCell(item);
        if (cell != null)
        {
            int count = Convert.ToInt32(cell.countOfResources.text);
            cell.countOfResources.text = $"{++count}";
            return;
        }
        cellPrefab.gameObject.GetComponentInChildren<CellScript>().itemInfo = item;
        items.Add(Instantiate(cellPrefab, transform).GetChild(0).GetComponent<CellScript>());
    }

    private CellScript FindEqualsCell(InventoryItem item)
    {
        if (item is CraftObject)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if ((CraftObject)item == (CraftObject)items[i].itemInfo)
                {
                    return items[i];
                }
            }
            return null;
        }
        if (items.Find(x => x.itemInfo == item))
        {
            return items.Find(x => x.itemInfo == item);
        }
        return null;
    }
}
