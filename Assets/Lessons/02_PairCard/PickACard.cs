using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickACard : MonoBehaviour
{
    [field:SerializeField]InputActionReference LeftClickInputRef { get; set; }
    InputAction LeftClickAcion => LeftClickInputRef.action;

    [field:SerializeField]InputActionReference CurserPositionInputRef { get; set; }
    InputAction CursorPositionAction => CurserPositionInputRef.action;

    [field: SerializeField] LayerMask CardLayer { get; set; }

    Card RevealedCard;

    private void OnEnable()
    {
        LeftClickAcion.Enable();
        CursorPositionAction.Enable();
    }

    private void OnDisable()
    {
        LeftClickAcion.Disable();
        CursorPositionAction.Disable();
    }

    private void Awake()
    {
        LeftClickAcion.performed += _ => {
            Vector2 screenInputPos = CursorPositionAction.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(screenInputPos);
            RaycastHit2D[] result = new RaycastHit2D[100];
            var n_result = Physics2D.RaycastNonAlloc(ray.origin,ray.direction*20f,result,20f,CardLayer);
            Debug.Log(n_result);
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.white, 10f);
            if(n_result > 0)
            {
                if(result[0].transform.TryGetComponent(out Card card)){
                    if (card.IsFaceDown)
                    {
                        StartCoroutine(RevealCard(card));
                    }
                }
            }
        };
    }

    IEnumerator RevealCard(Card card)
    {
        LeftClickAcion.Disable();
        yield return card.FlipCard();

        if (RevealedCard == null)
        {
            RevealedCard = card;
        }
        else
        {
            yield return new WaitForSeconds(1f);

            if (RevealedCard.CardData.name == card.CardData.name)
            {
                // remove both
                var _revealedCard = RevealedCard;
                RevealedCard = null;
                SetupCards.Instance.CardLeft.Remove(_revealedCard);
                Destroy(_revealedCard.gameObject);

                SetupCards.Instance.CardLeft.Remove(card);
                Destroy(card.gameObject);

                SetupCards.Instance.CheckWinCondition();
            }
            else
            {
                // faceDown both
                StartCoroutine(card.FlipCard());
                yield return RevealedCard.FlipCard();
                //yield return RevealedCard.FlipCard();
                RevealedCard = null;

            }
        }
        yield return null;
        LeftClickAcion.Enable();
    }


}