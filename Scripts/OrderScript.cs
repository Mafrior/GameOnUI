using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrderScript : MonoBehaviour
{
    public Text orderMessage; // сообщение которое выводится на  UI
    private CraftObject orderItem; // id объекта, который мы хотим получить

    void Start()
    {
        SetOrder();
    }

    /// <summary>
    /// Устанавливает крафт который хочет покупатель
    /// </summary>
    void SetOrder()
    {
        orderItem = DataBase.Instance.GetRandomCraft();
        orderMessage.text = $"Я хочу {orderItem.Name}";
    }

    /// <summary>
    /// Отдаёт предмет из инвентаря (Если такой имеется)
    /// </summary>
    public void GiveAnOrder()
    {
        if (InventoryManager.Instance.FindCraft(orderItem))
        {
            orderMessage.text = "";
        }
    }
}
