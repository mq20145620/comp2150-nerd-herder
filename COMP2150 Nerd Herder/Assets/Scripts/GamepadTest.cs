using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadTest : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5;

    void Update()
    {
        Vector2 dir = Gamepad.current.leftStick.ReadValue();
        transform.Translate(speed * dir * Time.deltaTime);
    }
}
