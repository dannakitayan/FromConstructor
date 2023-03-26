using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGround : MonoBehaviour
{
    [SerializeField] MeshRenderer floor, ceiling;

    public void SetMaterials(Material materialFloor, Material materialCeiling)
    {
        floor.material = materialFloor;
        ceiling.material = materialCeiling;
    }
}
