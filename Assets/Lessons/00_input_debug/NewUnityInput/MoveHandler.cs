using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class MoveHandler : MonoBehaviour
{
    [field:SerializeField] public InputActionReference InputActionReference { get; set; }
    InputAction InputAction => InputActionReference.action;
    Player Player { get; set; }
    float Speed => Player.Speed;

    private void OnEnable() => InputActionReference.action.Enable();
    private void OnDisable() => InputActionReference.action.Disable();

    private void Awake()
    {
        InputAction.performed += ctx => StartCoroutine(MoveRoutine());
        if(TryGetComponent(out Player player))
        {
            Player = player;
        }
        else
        {
            Debug.Log("Player Script not found.");
        }
    }
    /*
    private void Update()
    {
        if (InputAction.IsPressed()) {
            Move(InputAction.ReadValue<Vector2>());
        }
    }
    */
    IEnumerator MoveRoutine()
    {        
        while (InputAction.phase == InputActionPhase.Performed)
        {
            Move(InputAction.ReadValue<Vector2>());
            yield return null;      // put everything after this to execute in next frame
        }
    }

    void Move(Vector2 moveInput)
    {
        transform.position += Speed * Time.deltaTime * new Vector3(moveInput.x,moveInput.y);
    }
}
