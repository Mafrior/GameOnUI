using UnityEngine;

// Адаптивно устанавливает размеры колайдерам
public class DynamicColiderSize : MonoBehaviour
{
    void Awake()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
    }
}
