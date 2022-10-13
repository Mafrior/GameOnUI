using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Мехи, которые управляет правильной температурой
/// </summary>
public class MechsScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float maxHornTemperature;
    public Slider slider; // Слайдер температуры
    public RectTransform temperaturePoint; // Объект, который устанавливает (визуально) необходимую температуру нагрева

    private bool setTimer;
    private float timer;

    // При нажатии устанавливает нагрев
    public void OnPointerDown(PointerEventData eventData)
    {
        setTimer = true;
    }

    // Если игрок слишком быстро отжал клик/тап, то температура спадает
    public void OnPointerUp(PointerEventData eventData)
    {
        setTimer = false;
        if (timer <= 0.2f) { slider.value -= 0.03f; }
        timer = 0;
    }

    void Update()
    {
        slider.value -= 0.02f * Time.deltaTime; // со временем температура падает
        if (setTimer)
        {
            timer += Time.deltaTime;
            if (timer > 0.8)
            { // Догла держим
                return;
            }
            if (timer > 0.2f)
            { // Чутка держим
                slider.value += 0.1f * Time.deltaTime;
            }
            if (timer > 0.5f)
            { // Средне держим
                slider.value += 0.3f * Time.deltaTime;
            }
        }

    }

    /// <summary>
    /// Адаптивно устанавливает необходимую температуру
    /// </summary>
    /// <param name="temperature"></param>
    public void SetPoint(float temperature)
    {
        float height = slider.gameObject.GetComponent<RectTransform>().rect.height;
        temperaturePoint.localPosition = new Vector3(0, (height / 100 * (temperature / maxHornTemperature * 100)) - slider.transform.position.y - height / 2, 0);
        temperaturePoint.gameObject.SetActive(true);
    }

    public void HidePoint()
    {
        temperaturePoint.gameObject.SetActive(false);
    }
}
