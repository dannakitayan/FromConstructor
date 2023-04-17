using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] DefaultWeapon[] weapons;
    int currentWeaponNumber;

    public static Action onShot;
    public static Action<Weapons> onChange;
    public static Action<int> onAmmoAdd;
    //Save\Load;
    public static Action onSaveWeaponsState;
    public static Action onLoadWeaponsState;

    public static bool isShooting = false;

    private void Start()
    {
        SetWeaponOnStart(PlayerParameters.CurrentWeapon);
    }

    void Awake()
    {
        onChange += SetWeapon;
        onAmmoAdd += AddAmmo;
        onSaveWeaponsState += SaveWeaponsState;

        InputController.onUse += Interaction;
        InputController.onShoot += Shoot;
    }

    void OnDestroy()
    {
        onChange -= SetWeapon;
        onAmmoAdd -= AddAmmo;
        onSaveWeaponsState -= SaveWeaponsState;

        InputController.onUse += Interaction;
        InputController.onShoot -= Shoot;
    }

    void DisableAll()
    {
        foreach(var element in weapons)
        {
            if(element is not null)
            {
                element.gameObject.SetActive(false);
            }
        }
    }

    void SetWeaponOnStart(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.Weapon1:
                weapons[0].gameObject.SetActive(true);
                currentWeaponNumber = 0;
                break;
            case Weapons.Weapon2:
                weapons[1].gameObject.SetActive(true);
                currentWeaponNumber = 1;
                break;
            case Weapons.Weapon3:
                weapons[2].gameObject.SetActive(true);
                currentWeaponNumber = 2;
                break;
            case Weapons.Weapon4:
                weapons[3].gameObject.SetActive(true);
                currentWeaponNumber = 3;
                break;
            case Weapons.Weapon5:
                weapons[4].gameObject.SetActive(true);
                currentWeaponNumber = 4;
                break;
            case Weapons.Weapon6:
                weapons[5].gameObject.SetActive(true);
                currentWeaponNumber = 5;
                break;
        }

        PlayerParameters.CurrentWeapon = weapon;
    }

    void SetWeapon(Weapons weapon)
    {
        if (isShooting 
            || !PlayerParameters.HaveWeapon(weapon) 
            || PlayerParameters.CurrentWeapon == weapon) return;

        DisableAll();
        switch (weapon)
        {
            case Weapons.Weapon1:
                weapons[0].gameObject.SetActive(true);
                currentWeaponNumber = 0;
                break;
            case Weapons.Weapon2:
                weapons[1].gameObject.SetActive(true);
                currentWeaponNumber = 1;
                break;
            case Weapons.Weapon3:
                weapons[2].gameObject.SetActive(true);
                currentWeaponNumber = 2;
                break;
            case Weapons.Weapon4:
                weapons[3].gameObject.SetActive(true);
                currentWeaponNumber = 3;
                break;
            case Weapons.Weapon5:
                weapons[4].gameObject.SetActive(true);
                currentWeaponNumber = 4;
                break;
            case Weapons.Weapon6:
                weapons[5].gameObject.SetActive(true);
                currentWeaponNumber = 5;
                break;
        }

        PlayerParameters.CurrentWeapon = weapon;
    }

    void AddAmmo(int value)
    {
        if(PlayerParameters.CurrentWeapon == Weapons.NULL) return;
        switch (PlayerParameters.CurrentWeapon)
        {
            case Weapons.Weapon1:
                weapons[0].CurrentAmmo += value;
                break;
            case Weapons.Weapon2:
                weapons[1].CurrentAmmo += value;
                break;
            case Weapons.Weapon3:
                weapons[2].CurrentAmmo += value;
                break;
            case Weapons.Weapon4:
                weapons[3].CurrentAmmo += value;
                break;
            case Weapons.Weapon5:
                weapons[4].CurrentAmmo += value;
                break;
            case Weapons.Weapon6:
                weapons[5].CurrentAmmo += value;
                break;
        }
    }

    void SaveWeaponsState()
    {
        foreach (var element in weapons)
        {
            PlayerParameters.UpdateWeaponCluster(element.WeaponType, element.CurrentAmmo);
        }
    }

    //Remove in the other script;
    void Interaction()
    {
        int num = 2;
        num = ~num;
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, /*weaponParameter.Range*/ 2, num) && hitInfo.collider != null)
        {
            //Debug.Log(hitInfo.collider.tag);
            if (hitInfo.collider.tag == "NextLevel")
            {
                SaveWeaponsState();
                LevelLoader.onLevelLoad?.Invoke();
            }

            if (hitInfo.collider.tag == "Door")
            {
                var door = hitInfo.collider.GetComponent<DoorAction>();
                if (door.State == DoorState.Open) return;
                if (Vector3.Distance(Player.onPositionGet.Invoke(), hitInfo.collider.transform.position) > 5f) return;
                Debug.Log("Action");
                door.OpenDoor();
            }
        }
    }

    void Shoot()
    {
        weapons[currentWeaponNumber].Shoot();
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    onShot?.Invoke();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Interaction();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SetWeapon(Weapons.Weapon1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SetWeapon(Weapons.Weapon2);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SetWeapon(Weapons.Weapon3);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SetWeapon(Weapons.Weapon4);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    SetWeapon(Weapons.Weapon5);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    SetWeapon(Weapons.Weapon6);
        //}
    }
}
