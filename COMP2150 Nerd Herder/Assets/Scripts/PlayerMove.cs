using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5;

    new private Rigidbody2D rigidbody;

    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 dir = Gamepad.current.leftStick.ReadValue();
        rigidbody.velocity = dir * speed;
    }
}
