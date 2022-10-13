using UnityEngine;
using UnityEngine.EventSystems;

// Управляет перемещением между сценами через свайпы
public class ChangeSceneManager : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Camera cam;
    public Transform DownsideSceneYPosition;
    private int state = 0;

    // Так надо
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    // Управляет положением камеры относительно свайпов
    public void OnDrag(PointerEventData eventData)
    {
        if (cam.transform.position == new Vector3(0, 0, -100)) { state = 0; }
        if (eventData.delta.y >= 3 && (state == 0 || state == 1))
        {
            cam.transform.position = new Vector3(0, Mathf.Clamp(cam.transform.position.y - eventData.delta.y * Time.deltaTime, DownsideSceneYPosition.position.y, 0f), -100);
            state = 1;
        }

        if (eventData.delta.y <= -3 && state == 1)
        {
            cam.transform.position = new Vector3(0, Mathf.Clamp(cam.transform.position.y - eventData.delta.y * Time.deltaTime, DownsideSceneYPosition.position.y, 0f), -100);
        }

        // ! потом уберём
        //if (eventData.delta.x >= 3 && (state == 0 || state == 2))
        //{
        //    cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x - eventData.delta.x * Time.deltaTime, -17.78f, 0f), 0, -100);
        //    state = 2;
        //}

        //if (eventData.delta.x <= -3 && state == 2)
        //{
        //    cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x - eventData.delta.x * Time.deltaTime, -17.78f, 0f), 0, -100);
        //}
    }
}