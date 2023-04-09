using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] Animator animator;
    DoorState state = DoorState.Close;
    Coroutine doorCoroutine;
    bool isChangingState = false;

    public DoorState State => state;

    public void OpenDoor()
    {
        if (isChangingState) return;
        if (state == DoorState.Open) return;

        state = DoorState.Open;
        boxCollider.enabled = false;
        animator.Play("Open");
        doorCoroutine = StartCoroutine(DoorStuck());
    }

    IEnumerator DoorStuck()
    {
        while (state == DoorState.Open)
        {
            float distance = Vector3.Distance(transform.position, Player.onPositionGet.Invoke());
            Debug.Log($"Distance {distance}");

            if (distance > 5f)
            {
                boxCollider.enabled = true;
                animator.Play("Close");
                state = DoorState.Close;
                doorCoroutine = null;
                isChangingState = false;
                yield break;
            }

            yield return null;
        }
    }
}
