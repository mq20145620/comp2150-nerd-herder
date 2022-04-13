using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordsOnPlay.Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class StudentMove : MonoBehaviour
{
    [SerializeField] private Range wanderImpulse = new Range(4,5);
    [SerializeField] private Range wanderTime = new Range(1,2);

    new private Rigidbody2D rigidbody;
    private float wander;
    private RadiusTrigger attractionRadius;
    private RadiusTrigger repulsionRadius;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.drag = 1;

        attractionRadius = transform.Find("AttractionRadius").GetComponent<RadiusTrigger>();
        repulsionRadius = transform.Find("RepulsionRadius").GetComponent<RadiusTrigger>();

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

    public void OnRadiusEnter(RadiusTrigger trigger, Collider2D other) 
    {
        if (trigger == repulsionRadius) 
        {
            // TODO
        }
        else if (trigger == repulsionRadius) 
        {
            // TODO
        }
    }
}
