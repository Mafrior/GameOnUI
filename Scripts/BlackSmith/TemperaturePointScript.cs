using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, который устанавливает сохраняется ли температура
/// </summary>
public class TemperaturePointScript : MonoBehaviour
{
    public HornScript horn;
    public Slider mySlider;

    private bool isFollowTemperature;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TemperaturePoint"))
        {
            isFollowTemperature = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // TODO: возможно уберём
        if (collision.gameObject.CompareTag("TemperaturePoint"))
        {
            isFollowTemperature = false;
        }
    }

    private void Update()
    {
        if (isFollowTemperature)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                horn.temperatureIsOptimal = true;
                mySlider.value = 0;
            }
            return;
        }
        timer = 0;
    }
}
