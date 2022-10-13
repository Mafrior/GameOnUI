using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Хранит все вкладки инвентаря (ресурсы и крафты)
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventoryScript resources;
    public InventoryScript forgingResources;
    public InventoryScript equip;

    public DeckScript deck;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Добавляет ресурс в инвентарь и вызывает удаление карты
    /// </summary>
    /// <param name="item"></param>
    public void AddResource(InventoryItem item, bool isForgingInvenoty = false)
    {
        if (isForgingInvenoty)
        {
            forgingResources.AddResource(item); return;
        }
        if (item is CraftObject)
        {
            equip.AddResource(item); return;
        }
        resources.AddResource(item);
    }

    public void RemoveItem(CellScript cell)
    {
        if (resources.items.Contains(cell))
        {
            resources.items.Remove(cell);
            Destroy(cell.transform.parent.gameObject);
        }
        if (forgingResources.items.Contains(cell))
        {
            forgingResources.items.Remove(cell);
            Destroy(cell.transform.parent.gameObject);
        }
    }

    /// <summary>
    /// Находит среди всех крафтов в инвентаре такой крафт, который нужен покупателю. true - такой есть, false - такого нет
    /// </summary>
    /// <param name="craft"></param>
    /// <returns></returns>
    public bool FindCraft(CraftObject craft)
    {
        if (equip.items.Any(x => (CraftObject)x.itemInfo == craft))
        {
            return true;
        }
        return false;
    }
}
