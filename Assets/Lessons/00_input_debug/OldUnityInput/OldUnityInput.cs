using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldUnityInput : MonoBehaviour
{
    public Transform targetTarform;
    readonly float inputScaler = 5f;

    private void Update()
    {
        DebugGetKeyUpAndDown(KeyCode.Y);
        // DebugGetKey(KeyCode.Y);
        // DebugAnyKeyDown(KeyCode.Y);
        DebugGetAxis();
    }

    #region KeyCode
    void DebugGetKeyUpAndDown(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log($"Input.KeyDown : {keyCode}");
        }

        if (Input.GetKeyUp(keyCode))
        {
            Debug.Log($"Input.KeyUp : {keyCode}");
        }
    }

    void DebugGetKey(KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
        {
            Debug.Log($"Input.Key : {keyCode}");
        }
    }

    void DebugAnyKeyDown()
    {
        if (Input.anyKeyDown)   // when any key down, included ,mouse button
        {
            Debug.Log("Input.AnyKeyDown");
        }
    }
    #endregion

    #region Axis
    //  Edit > ProjectSetting > Input Manager
    void DebugGetAxis()
    {
        float inputX = Input.GetAxis("Horizontal");     // A ,D ,left-arrow ,right-arrow , right-joystick(horizontal-axis)
        float inputY = Input.GetAxis("Vertical");       // W ,S ,up-arrow ,down-arrow ,reght-joystick(vertical-axis)

        if (inputX != 0 || inputY != 0) // don't update position in every Update just do it when you need to.
        {
            targetTarform.position = new Vector2(inputX, inputY) * inputScaler;
        }

        float fire1 = Input.GetAxis("Fire1");           // left-ctrl, left-mouse-button, joystick ?? (not sure >_<)
        if (fire1 > 0)  // fire1 dosen't have negative button at default
        {
            Debug.Log($"fire1 : {fire1}");
        }
    }
    #endregion

    /// let move your own character
    /// Hints :
    /// 1. getInput from user
    /// 2. update character position with the input
    /// 


    #region DeltaTime
    /// https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
    /// Time.DeltaTime = The interval in seconds from the last frame to the current one (Read Only).
    void UpdatePositionCharater()
    {
        float speed = 20f;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputX != 0 || inputY != 0)
        {
            targetTarform.position += speed * Time.deltaTime * new Vector3(inputX, inputY);
        }
    }
    #endregion
}
