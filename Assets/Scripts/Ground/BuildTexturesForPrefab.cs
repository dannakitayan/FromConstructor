using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTexturesForPrefab : MonoBehaviour
{
    [SerializeField] MeshRenderer first, second, third, fourth;

    public void SetMaterials(Material materialFirst, Material materialSecond)
    {
        first.material = materialFirst;
        second.material = materialSecond;
        if (third == null || fourth == null) return;
        third.material = materialFirst;
        fourth.material = materialSecond;
    }
}
