using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordsOnPlay.Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class StudentMove : MonoBehaviour
{
    static private float TAU = 2 * Mathf.PI;
    [SerializeField] private Range wanderImpulse = new Range(4,5);
    [SerializeField] private Range wanderTime = new Range(1,2);

    private float wander;
    new private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.drag = 1;

        wander = wanderTime.Random();
    }

    void Update()
    {
        Wander();
    }

    private void Wander() 
    {
        wander -= Time.deltaTime;

        if (wander < 0)
        {
            wander += wanderTime.Random();

            // add a push in a random direction
            Vector2 impulse = wanderImpulse.Random() * Vector2.right.Rotate(Random.value * 360);
            rigidbody.AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}
