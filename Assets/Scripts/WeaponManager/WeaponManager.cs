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
    public static bool isShooting = false;

    void Start()
    {

    }

    void OnEnable()
    {
        onChange += SetWeapon;
    }

    void OnDisable()
    {
        onChange -= SetWeapon;
    }

    void SetWeaponInHand(Weapons weapon, int ammo, int maxAmmo)
    {
        if(!PlayerSingleton.Get().Player.HaveWeapon(weapon))
        {
            PlayerSingleton.Get().Player.AddWeapon(weapon, ammo, maxAmmo);
        }
        if(PlayerSingleton.Get().Player.CurrentWeapon != weapon) PlayerSingleton.Get().Player.CurrentWeapon = weapon;
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
        //DisableAll();
        //switch (weapon)
        //{
        //    case Weapons.Weapon1:
        //        weapon1.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon1, weapon1.weaponParameter.StartAmmo, weapon1.weaponParameter.MaxAmmo);
        //        break;
        //    case Weapons.Weapon2:
        //        weapon2.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon2, weapon2.weaponParameter.StartAmmo, weapon2.weaponParameter.MaxAmmo);
        //        break;
        //    case Weapons.Weapon3:
        //        weapon3.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon3, weapon3.weaponParameter.StartAmmo, weapon3.weaponParameter.MaxAmmo);
        //        break;
        //    case Weapons.Weapon4:
        //        weapon4.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon4, weapon4.weaponParameter.StartAmmo, weapon4.weaponParameter.MaxAmmo);
        //        break;
        //    case Weapons.Weapon5:
        //        weapon5.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon5, weapon5.weaponParameter.StartAmmo, weapon5.weaponParameter.MaxAmmo);
        //        break;
        //    case Weapons.Weapon6:
        //        weapon6.gameObject.SetActive(true);
        //        SetWeaponInHand(Weapons.Weapon6, weapon6.weaponParameter.StartAmmo, weapon6.weaponParameter.MaxAmmo);
        //        break;
        //}
    }

    void GetWeaponFromTheBag(int weaponNumber)
    {
        if (isShooting) return;
        switch(weaponNumber)
        {
            case 1:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon1) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon1) SetWeapon(Weapons.Weapon1);
                break;
            case 2:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon2) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon2) SetWeapon(Weapons.Weapon2);
                break;
            case 3:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon3) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon3) SetWeapon(Weapons.Weapon3);
                break;
            case 4:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon4) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon4) SetWeapon(Weapons.Weapon4);
                break;
            case 5:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon5) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon5) SetWeapon(Weapons.Weapon5);
                break;
            case 6:
                if (PlayerSingleton.Get().Player.HaveWeapon(Weapons.Weapon6) && PlayerSingleton.Get().Player.CurrentWeapon != Weapons.Weapon6) SetWeapon(Weapons.Weapon6);
                break;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            onShot?.Invoke();
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GetWeaponFromTheBag(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    GetWeaponFromTheBag(2);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    GetWeaponFromTheBag(3);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    GetWeaponFromTheBag(4);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    GetWeaponFromTheBag(5);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    GetWeaponFromTheBag(6);
        //}
    }
}
