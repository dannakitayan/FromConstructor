using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : DefaultWeapon
{
    void OnEnable()
    {
        WeaponManager.onShot += Shot;
    }

    void OnDisable()
    {
        WeaponManager.onShot -= Shot;
    }

    void Shot()
    {
        if (!CanShot()) return;
        canShot = false;
        StartCoroutine(GetShot());
    }

    IEnumerator GetShot()
    {
        animator.SetTrigger(weaponParameter.ShotTriggerName);
        SoundManager.onWeaponPlay?.Invoke(weaponParameter.Shot);
        yield return new WaitForSeconds(weaponParameter.TimeBetweenShots);
        currentWeaponAmmo -= 1;
        MainHud.onAmmoSet?.Invoke(currentWeaponAmmo.ToString());
        canShot = true;
    }
}
