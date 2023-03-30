using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] DefaultWeapon weapon1;
    [SerializeField] DefaultWeapon weapon2;
    [SerializeField] DefaultWeapon weapon3;
    [SerializeField] DefaultWeapon weapon4;
    [SerializeField] DefaultWeapon weapon5;
    [SerializeField] DefaultWeapon weapon6;
    [Header("Start level with a weapon")]
    public Weapons StartWith;

    public static Action onShot;
    public static Action<Weapons> onChange;
    public static Action<int> onAmmoAdd;
    public static bool isShooting = false;

    private void Start()
    {
        if (StartWith == Weapons.NULL) return;
        PlayerParameters.AddWeapon(StartWith);
        SetWeapon(StartWith);
    }

    void Awake()
    {
        onChange += SetWeapon;
        onAmmoAdd += AddAmmo;
    }

    void OnDestroy()
    {
        onChange -= SetWeapon;
        onAmmoAdd -= AddAmmo;
    }

    void DisableAll()
    {
        var weapons = new DefaultWeapon[] { weapon1, weapon2, weapon3, weapon4, weapon5, weapon6};
        foreach(var element in weapons)
        {
            if(element is not null)
            {
                element.gameObject.SetActive(false);
            }
        }
    }

    void SetWeapon(Weapons weapon)
    {
        if (isShooting || !PlayerParameters.HaveWeapon(weapon) || PlayerParameters.CurrentWeapon == weapon) return;

        DisableAll();
        switch (weapon)
        {
            case Weapons.Weapon1:
                weapon1.gameObject.SetActive(true);
                break;
            case Weapons.Weapon2:
                weapon2.gameObject.SetActive(true);
                break;
            case Weapons.Weapon3:
                weapon3.gameObject.SetActive(true);
                break;
            case Weapons.Weapon4:
                weapon4.gameObject.SetActive(true);
                break;
            case Weapons.Weapon5:
                weapon5.gameObject.SetActive(true);
                break;
            case Weapons.Weapon6:
                weapon6.gameObject.SetActive(true);
                break;
        }

        PlayerParameters.CurrentWeapon = weapon;
    }

    void AddAmmo(int value)
    {
        switch (PlayerParameters.CurrentWeapon)
        {
            case Weapons.Weapon1:
                weapon1.CurrentAmmo += value;
                break;
            case Weapons.Weapon2:
                weapon2.CurrentAmmo += value;
                break;
            case Weapons.Weapon3:
                weapon3.CurrentAmmo += value;
                break;
            case Weapons.Weapon4:
                weapon4.CurrentAmmo += value;
                break;
            case Weapons.Weapon5:
                weapon5.CurrentAmmo += value;
                break;
            case Weapons.Weapon6:
                weapon6.CurrentAmmo += value;
                break;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            onShot?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(Weapons.Weapon1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(Weapons.Weapon2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(Weapons.Weapon3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeapon(Weapons.Weapon4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetWeapon(Weapons.Weapon5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetWeapon(Weapons.Weapon6);
        }
    }
}
