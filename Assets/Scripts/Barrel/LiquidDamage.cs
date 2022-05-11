using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidDamage : MonoBehaviour
{
    public int Damage;
    bool playerCanGetDamage = true;

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (playerCanGetDamage) StartCoroutine(PlayerDamage(Damage));
        }
    }

    IEnumerator PlayerDamage(int value)
    {
        PlayerSingleton.Get().Player.Health = -Damage;
        Debug.Log(PlayerSingleton.Get().Player.Health);
        HitIndicator.onIndicatorChange?.Invoke(0.25f);
        playerCanGetDamage = false;
        yield return new WaitForSeconds(1f);
        playerCanGetDamage = true;
    }
}
