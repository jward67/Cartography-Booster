using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float xDegree = 0f, yDegree = 0f, zDegree = 0f;

    void Update()
    {
        // Update object's rotations every frame relative to itself in the coordinate space.
        transform.Rotate(xDegree, yDegree, zDegree);
    }
}
