using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touching Start Pad");
                break;
            case "Finish":
                Debug.Log("Level Finished!");
                break;
            case "Fuel":
                Debug.Log("Refueling Rocket");
                break;
            default:
                Debug.Log("CRASH!");
                break;
        }
    }
}
