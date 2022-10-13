using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HornScript : MonoBehaviour, IPointerDownHandler
{
    public CraftManager craftManager;
    public MechsScript mechs;
    public AnvilScript anvil;
    public Sprite[] fireSprites; // Спрайты огня

    [HideInInspector]
    public Image fire;
    [HideInInspector]
    public bool temperatureIsOptimal; // Температура установлена

    private MetalObject forgingMetal;
    private bool isMelting; // Заготовка греется (ей холодно)
    private float timer;
    private float randomTime; // Случайное время готовки предмета ( // TODO: передать металлам )

    private bool isReadyToTakeOff; // Заготовку можно вытаскивать

    private void Awake()
    {
        fire = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (temperatureIsOptimal)
        {
            fire.sprite = fireSprites[0];
            fire.enabled = true;
            mechs.HidePoint();

            temperatureIsOptimal = false;

            isMelting = true;
            randomTime = Random.Range(1, 2);
        }
        if (isMelting)
        {
            timer += Time.deltaTime;
            if (timer >= randomTime) // Если время доставать подошло игрок должен успеть
            {
                fire.sprite = fireSprites[1];
                isMelting = false;
                isReadyToTakeOff = true;
                timer = 0;
            }
        }
    }

    public void AddCraftingResource(ResourceObject resource) // Добавляет в горн ресурс 
    {
        if (resource is MetalObject)
        {
            MetalObject metal = resource as MetalObject;
            forgingMetal = metal;
            mechs.SetPoint(metal.temperaturePoint);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isReadyToTakeOff) // Если игрок вытаскивает заготовку - добавляем ко всей остальным заготовкам
        {
            fire.enabled = false;
            anvil.isCanForging = true;
            craftManager.AddMetal(forgingMetal);
        }
    }
}
