using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCards : MonoBehaviour
{
    public static SetupCards Instance { get; set; }
    [field: SerializeField] public GameObject CardPrefab { get; set; }
    Vector2Int size = new(4, 4);
    [field: SerializeField] public List<CardData> CardDataset { get; set; }
    public List<CardData> cardList;
    [field:SerializeField] public List<Card> CardLeft { get; private set; }

    [field:SerializeField] public GameObject WinGameUi { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        WinGameUi.SetActive(false);
        foreach(var CardData in CardDataset)
        {
            cardList.Add(CardData);
            cardList.Add(CardData);
        }

        SuffleCard(cardList);

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                var clone = Instantiate(CardPrefab,new Vector2(x - (size.x / 2), y - (size.y / 2)), Quaternion.identity,transform);
                if(clone.TryGetComponent(out Card card))
                {
                    card.CardData = cardList[(x * size.y) + y];
                    CardLeft.Add(card);
                }
            }
        }
    }

    void SuffleCard(List<CardData> cards)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            int rng = Random.Range(0, cards.Count);
            var temp = cards[i];
            cards[i] = cards[rng];
            cards[rng] = temp;
         
        }
    }

    public void CheckWinCondition()
    {
        if(CardLeft.Count == 0){
            WinGameUi.SetActive(true);
        }
    }
}
