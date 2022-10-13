using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataBase : MonoBehaviour
{
    public static DataBase Instance; // Синглтон

    // массивы с данными
    [SerializeField]
    private MetalObject[] metals;
    [SerializeField]
    private WoodObject[] woods;
    [SerializeField]
    private CraftObject[] crafts;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = gameObject.GetComponent<DataBase>();
    }

    /// <summary>
    /// Выдаёт случайный ресурс
    /// </summary>
    /// <returns></returns>
    public ResourceObject GetRandomResource()
    {
        if (Random.value >= 0.7)
        {
            return woods[Random.Range(0, woods.Length)];
        }
        return metals[Random.Range(0, metals.Length)];
    }

    /// <summary>
    /// Выдаёт случайный ресурс
    /// </summary>
    /// <returns></returns>
    public CraftObject GetRandomCraft()
    {
        return crafts[Random.Range(0, crafts.Length)];
    }

    public List<CraftObject> GetCrafts()
    {
        return crafts.ToList();
    }
}
