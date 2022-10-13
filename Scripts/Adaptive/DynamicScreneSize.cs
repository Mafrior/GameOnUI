using UnityEngine;

public class DynamicScreneSize : MonoBehaviour
{
    public Vector2 DefaultResolution = new Vector2(720, 1280);
    public RectTransform[] Canvases;

    private Camera componentCamera;

    private float initialSize; // Зум камеры
    private float targetAspect; // Изначальное соотношение сторон экрана

    [ExecuteInEditMode]
    private void Start()
    {
        componentCamera = GetComponent<Camera>();
        initialSize = componentCamera.orthographicSize;

        targetAspect = DefaultResolution.x / DefaultResolution.y;

        componentCamera.orthographicSize = initialSize * (targetAspect / componentCamera.aspect); // Устанавливает камере новый зум

        Vector2 currentResolution = new Vector2(DefaultResolution.x, DefaultResolution.x / componentCamera.aspect);

        for (int i = 0; i < Canvases.Length; i++)
        {
            Canvases[i].SetSize(currentResolution); // Изменяет размеры объектам сцены (UI) в соответствии с соотношением сторон
            if (Canvases[i].rect.position.y != 0)
            { // И меняем позиции объектам сцены для грамотной работы перехода между сценами
                Canvases[i].position = new Vector2(Canvases[i].position.x, Canvases[i].position.y / (DefaultResolution.y / Canvases[i].rect.height));
            }
        }
    }
}
