using UnityEngine;

public class DeckScript : MonoBehaviour
{
    // Массив созданных карт на данной раздаче
    private Transform[] deck;
    public int maxCount;

    public Transform cardPrefab;

    void Start()
    {
        FillTheDeck();
    }

    /// <summary>
    /// Создаёт карты на данную раздачу
    /// </summary>
    public void FillTheDeck()
    {
        if (deck != null)
        {
            transform.parent.gameObject.SetActive(true);
            for (int i = 0; i < maxCount; i++)
            {
                if (deck[i] != null)
                {
                    Destroy(deck[i].gameObject);
                }
            }
        }

        deck = new Transform[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            float rand = Random.value;
            cardPrefab.GetComponent<CardInfo>().Initialize(DataBase.Instance.GetRandomResource()); // ��� ��������� ���

            Vector2 newPos = new Vector2(transform.position.x - maxCount * 0.0008f * (maxCount - i), transform.position.y + maxCount * 0.0008f * (maxCount - i)); // ���� ��������� �� ������ ����

            deck[i] = Instantiate(cardPrefab, newPos, Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// Удаляет карту, перед этим отключает панель, если карта была последняя
    /// </summary>
    /// <param name="obj"></param>
    public void DestroyCard(GameObject obj)
    {
        if (deck[0] == obj.transform)
        {
            transform.parent.gameObject.SetActive(false);
        }
        Destroy(obj);
    }
}