using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardInfo : MonoBehaviour
{
    public Text text; // Инвормация об ресурсе (или абилке в дальнейшем)
    public ResourceObject resource;

    /// <summary>
    /// Устанавливает данные об карте при создании
    /// </summary>
    public void Initialize(ResourceObject res)
    {
        text.text = res.Name;
        resource = res;
    }
}
