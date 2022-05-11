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
        WeaponManager.isShooting = !canShot;
        StartCoroutine(GetShot());
    }

    IEnumerator GetShot()
    {
        animator.SetTrigger(weaponParameter.ShotTriggerName);
        SoundManager.onWeaponPlay?.Invoke(weaponParameter.Shot);
        WeaponManager.onWeaponFlashLight?.Invoke();
        Damage(4);
        MainCameraShake.onShake?.Invoke(weaponParameter.Angle);
        yield return new WaitForSeconds(weaponParameter.TimeBetweenShots);
        PlayerSingleton.Get().Player.ChangeAmmo(-1);
        MainHud.onAmmoSet?.Invoke(PlayerSingleton.Get().Player.GetAmmoCount().ToString());
        canShot = true;
        WeaponManager.isShooting = !canShot;
    }
}
