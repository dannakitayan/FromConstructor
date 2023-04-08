using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Animator animator;
    DoorState state = DoorState.Close;

    public void OpenDoor()
    {
        if (state == DoorState.Open) return;
        state = DoorState.Open;
        boxCollider.enabled = false;
        animator.Play("Open");
    }
}
