using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        Vector3 rotation = Camera.main.transform.position - transform.position;
        rotation.z = rotation.x = 0.0f;
        transform.LookAt(Camera.main.transform.position - rotation);
    }
}
