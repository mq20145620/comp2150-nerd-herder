using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordsOnPlay.Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class StudentMove : MonoBehaviour
{
    [SerializeField] private Range wanderImpulse = new Range(4,5);
    [SerializeField] private Range wanderTime = new Range(1,2);
    [SerializeField] private float attractForce = 1;
    [SerializeField] private float repelForce = 1;
    [SerializeField] private float scareForce = 5;
    [SerializeField] private float scaredMultiplier = 2;
    [SerializeField] private int minFriends = 1;
    [SerializeField] private int maxFriends = 4;

    new private Rigidbody2D rigidbody;
    private int nFriends;
    private float wander;
    private RadiusTrigger attractionRadius;
    private RadiusTrigger scareRadius;
    private RadiusTrigger repulsionRadius;
    private bool scared = false;

    private List<Collider2D> attracting = new List<Collider2D>();
    private List<Collider2D> repelling = new List<Collider2D>();
    private List<Collider2D> scaring = new List<Collider2D>();


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        attractionRadius = transform.Find("AttractionRadius").GetComponent<RadiusTrigger>();
        repulsionRadius = transform.Find("RepulsionRadius").GetComponent<RadiusTrigger>();
        scareRadius = transform.Find("ScareRadius").GetComponent<RadiusTrigger>();
        nFriends = Random.Range(minFriends, maxFriends+1);

        wander = wanderTime.Random();
    }

    void Update()
    {
        Wander();
        Scare();
        Attract();
        Repel();
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

    private int CompareDistance(Collider2D x, Collider2D y) 
    {
        if (x == null && y == null) {
            return 0;
        }
        if (x == null) {
            return -1;
        }
        if (y == null) {
            return 1;
        }
        float dx = DistanceTo(x);
        float dy = DistanceTo(y);
        return dx.CompareTo(dy);
    }

    private float DistanceTo(Collider2D other) 
    {
        return Vector2.Distance(other.ClosestPoint(transform.position), transform.position.xy());
    }

    private void Attract()
    {
        attracting.Sort(CompareDistance);

        // if they are scared they want more friends
        int n = (scared ? -1 : 0);
        foreach (Collider2D other in attracting)
        {
            n++;                        
            Vector2 force = other.ClosestPoint(transform.position) - transform.position.xy();

            if (n <= nFriends) 
            {
                force = force.normalized * attractForce * (scared ? scaredMultiplier : 1);
            }
            else 
            {
                force = -force.normalized * repelForce;
            }
            rigidbody.AddForce(force);
        }
    }

    private void Repel()
    {
        foreach (Collider2D other in repelling)
        {
            Vector2 force = other.ClosestPoint(transform.position) - transform.position.xy();
            force = -force.normalized * repelForce;
            rigidbody.AddForce(force);
        }
    }

    private void Scare()
    {
        scared = false;
        foreach (Collider2D other in scaring)
        {
            Vector2 force = other.ClosestPoint(transform.position) - transform.position.xy();
            force = -force.normalized * scareForce;
            rigidbody.AddForce(force);
            scared = true;
        }        
    }

    public void OnRadiusEnter(RadiusTrigger trigger, Collider2D other) 
    {
        if (trigger == attractionRadius) 
        {
            attracting.Add(other);
        }
        else if (trigger == repulsionRadius) 
        {
            repelling.Add(other);
        }
        else if (trigger == scareRadius) 
        {
            scaring.Add(other);
        }
    }

    public void OnRadiusExit(RadiusTrigger trigger, Collider2D other) 
    {
        if (trigger == attractionRadius) 
        {
            attracting.Remove(other);
        }
        else if (trigger == repulsionRadius) 
        {
            repelling.Remove(other);
        }
        else if (trigger == scareRadius) 
        {
            scaring.Remove(other);
        }
    }

    public void OnDrawGizmos() 
    {
        int n = 0;
        foreach (Collider2D other in attracting)
        {
            n++;
            if (n <= nFriends) {
                Gizmos.color = Color.green;
            }
            else {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawLine(transform.position, other.ClosestPoint(transform.position));
        }

        Gizmos.color = Color.red;

        foreach (Collider2D other in repelling)
        {
            Gizmos.DrawLine(transform.position, other.ClosestPoint(transform.position));
        }

        foreach (Collider2D other in scaring)
        {
            Gizmos.DrawLine(transform.position, other.ClosestPoint(transform.position));
        }

    }

}
