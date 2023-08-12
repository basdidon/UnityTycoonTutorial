using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class ClickToMove : MonoBehaviour
{
    public InputActionReference LeftClickReference;
    public InputAction LeftClickAction => LeftClickReference.action;

    public InputActionReference CursorReference;
    public InputAction CursorAction => CursorReference.action;

    Coroutine MoveRoutine;

    Player Player { get; set; }
    float Speed => Player.Speed;

    private void OnEnable()
    {
        LeftClickAction.Enable();
        CursorAction.Enable();
    }
    private void OnDisable()
    {
        LeftClickAction.Disable();
        CursorAction.Disable();
    }
    private void Awake()
    {
        if (TryGetComponent(out Player player))
        {
            Player = player;
        }
        else
        {
            Debug.Log("Player Script not found.");
        }

        LeftClickAction.performed += _ =>
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(CursorAction.ReadValue<Vector2>());

            if(MoveRoutine != null)
                StopCoroutine(MoveRoutine);
            
            MoveRoutine = StartCoroutine(MoveToPositionRoutine(worldPoint));
        };
    }

    IEnumerator MoveToPositionRoutine(Vector2 targetPos)
    {
        var startPos = transform.position;
        var distance = Vector2.Distance(startPos, targetPos);
        var duration = distance / Speed;
        float timeElapsed = 0f;

        while(timeElapsed <= duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, timeElapsed / duration);
            Debug.DrawLine(transform.position, targetPos,Color.white);
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }
}
