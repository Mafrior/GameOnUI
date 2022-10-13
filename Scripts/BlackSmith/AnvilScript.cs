using UnityEngine;
using UnityEngine.EventSystems;

public class AnvilScript : MonoBehaviour, IPointerDownHandler
{
    public Transform ForgingPanel;

    [HideInInspector]
    public bool isCanForging;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCanForging)
        {
            ForgingPanel.transform.parent.gameObject.SetActive(true);
        }
    }
}
