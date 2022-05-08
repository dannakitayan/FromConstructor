using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultWeapon : MonoBehaviour
{
    [SerializeField]
    public WeaponParameter weaponParameter;
    protected Animator animator;
    protected bool canShot;

    void Start()
    {
        animator = GetComponent<Animator>();
        canShot = true;
    }

    bool HaveAmmo()
    {
        if (PlayerSingleton.Get().Player.GetAmmoCount() > weaponParameter.MinimumAmmoInMagazine) return true;
        else return false;
    }

    protected bool CanShot()
    {
        return HaveAmmo() && canShot;
    }
}
