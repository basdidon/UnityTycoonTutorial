using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [field: SerializeField] Sprite FaceDownSpite { get; set; }
    [SerializeField] CardData cardData;
    public CardData CardData {
        get => cardData;
        set
        {
            cardData = value;
            SpriteRenderer.sprite = CardData.Sprite;
        }
    }

    [SerializeField] bool isFaceDown;
    public bool IsFaceDown {
        get => isFaceDown;
        set
        {
            isFaceDown = value;

            SpriteRenderer.sprite = IsFaceDown ? FaceDownSpite : CardData.Sprite;
        }
    }

    public IEnumerator FlipCard()
    {
        float duration = 1f;
        float timeElapsed = 0f;
        while(timeElapsed < duration/2)
        {
            transform.localScale = new Vector3 (Mathf.Lerp(1, 0, timeElapsed / (duration/2)),1,1);
            yield return null;
            timeElapsed += Time.deltaTime;
            //  Debug.Log('a');
        }

        IsFaceDown = !IsFaceDown;

        while(timeElapsed < duration)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0, 1, timeElapsed / duration), 1, 1);
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        yield return null;
    }

    SpriteRenderer SpriteRenderer { get; set; }

    private void Awake()
    {
        if(TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            SpriteRenderer = spriteRenderer;
        }
    }

    private void Start()
    {
        IsFaceDown = true;
    }
}
