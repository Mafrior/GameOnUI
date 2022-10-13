using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

/// <summary>
/// Класс отвечает за создание предметов из заготовок
/// </summary>
public class CraftManager : MonoBehaviour, IPointerDownHandler
{
    private List<SlotScript> Slots = new List<SlotScript>();
    public CraftObject[] craftsKnowledge; // Массив тех крафтов, которые игрок уже сделал хотя бы раз (в дальнейшем будет нужно для библиотеки)

    public Transform MadenCraftPanel;

    private Dictionary<string, List<ResourceObject>> resources = new Dictionary<string, List<ResourceObject>>()
    {
        ["metal"] = new List<ResourceObject>(),
        ["wood"] = new List<ResourceObject>()
    }; // Созданные ресурсы (заготовки)

    private void Awake()
    {
        Slots = transform.GetComponentsInChildren<SlotScript>().ToList();
    }

    /// <summary>
    /// Добавляет метал к заготовкам, для дальнейшего создания предмета
    /// </summary>
    /// <param name="metal"></param>
    public void AddMetal(MetalObject metal)
    {
        resources["metal"].Add(metal);
        InventoryManager.Instance.AddResource(metal, true);
    }

    /// <summary>
    /// Добавляет дерево к заготовкам, для дальнейшего создания предмета
    /// </summary>
    /// <param name="wood"></param>
    public void AddWood(WoodObject wood)
    {
        resources["wood"].Add(wood);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void ReloadMatches()
    {
        List<CraftObject> DBCrafts = DataBase.Instance.GetCrafts();
        for (int i = 0; i < DBCrafts.Count; i++)
        {
            for (int k = 0; k < DBCrafts[i].cases.Count; k++)
            {
                for (int j = 0; j < Slots.Count; j++)
                {
                    string matchingString = Slots[j].currentResource is MetalObject ? "Металл" : Slots[j].currentResource is WoodObject ? "Дерево" : "";
                    if (matchingString == DBCrafts[i].cases[k].variant[j])
                    {
                        if (j == Slots.Count - 1)
                        {
                            MadenCraftPanel.GetChild(0).GetComponent<Text>().text = DBCrafts[i].Name;
                        }
                        continue;
                    }
                    break;
                }
            }
        }
    }
}
