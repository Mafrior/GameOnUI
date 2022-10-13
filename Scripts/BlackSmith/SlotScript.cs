using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SlotScript : MonoBehaviour
{
    private Text currentText;
    [HideInInspector]
    public ResourceObject currentResource;

    private void Awake()
    {
        currentText = GetComponentInChildren<Text>();
    }

    public void SetResource(ResourceObject resource)
    {
        currentResource = resource;
        currentText.text = resource.Name;
        transform.parent.parent.GetComponent<CraftManager>().ReloadMatches();
    }
}
