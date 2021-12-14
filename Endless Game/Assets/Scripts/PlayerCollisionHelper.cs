using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHelper : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.contacts[0].thisCollider;
        if(collider.name == "Box")
        {
            print(collider.name + " hit " + collision.gameObject.name);
        }
        
    }
}
