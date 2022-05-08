using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : DefaultWeapon
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
        WeaponManager.isShooting = !canShot;
        StartCoroutine(GetShot());
    }

    IEnumerator GetShot()
    {
        animator.SetTrigger(weaponParameter.ShotTriggerName);
        SoundManager.onWeaponPlay?.Invoke(weaponParameter.Shot);
        MainCameraShake.onShake?.Invoke(130);
        yield return new WaitForSeconds(weaponParameter.TimeBetweenShots);
        PlayerSingleton.Get().Player.ChangeAmmo(-2);
        canShot = true;
        WeaponManager.isShooting = !canShot;
    }
}
