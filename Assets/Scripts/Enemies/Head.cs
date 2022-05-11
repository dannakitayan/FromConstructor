using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject NeckBleed;

    public void Execution()
    {
        gameObject.SetActive(false);
        NeckBleed.SetActive(true);
    }
}
