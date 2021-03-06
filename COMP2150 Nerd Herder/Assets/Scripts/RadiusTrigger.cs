using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTrigger : MonoBehaviour
{
    private StudentMove student;

    void Start()
    {
        student = transform.parent.GetComponent<StudentMove>();
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        // pass any enter events to the student class with the identity of this
        if (collider.gameObject != student.gameObject) {
            student.OnRadiusEnter(this, collider);        
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        // pass any exit events to the student class with the identity of this
        if (collider.gameObject != student.gameObject) {
            student.OnRadiusExit(this, collider);
        }
    }

}
