using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponParameter weaponParameter;
    protected int currentWeaponAmmo;
    protected Animator animator;
    protected bool canShot;

    void Start()
    {
        currentWeaponAmmo = weaponParameter.StartAmmo;
        animator = GetComponent<Animator>();
        canShot = true;
        MainHud.onAmmoSet?.Invoke(currentWeaponAmmo.ToString());
    }

    bool HaveAmmo()
    {
        if (currentWeaponAmmo > weaponParameter.MinimumAmmoInMagazine) return true;
        else return false;
    }

    protected bool CanShot()
    {
        return HaveAmmo() && canShot;
    }
}
