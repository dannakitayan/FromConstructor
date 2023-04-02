using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : MonoBehaviour
{
    [SerializeField] WeaponSetting weaponSetting;
    Material weaponMaterial;

    int currentAmmo;

    public int CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }

        set
        {
            int expectedValue = Mathf.Abs(currentAmmo + value); //If the user makes a negative value when collecting ammunition,
                                                                //it will always be a positive value.
            if (expectedValue >= weaponSetting.MaxAmmo)
            {
                currentAmmo = weaponSetting.MaxAmmo;
            }
            else
            {
                currentAmmo = expectedValue;
            }

            MainHud.onAmmoSet?.Invoke(currentAmmo.ToString());
        }
    }

    public Weapons WeaponType
    {
        get
        {
            return weaponSetting.WeaponType;
        }
    }

    void Awake()
    {
        weaponMaterial = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        currentAmmo = PlayerParameters.LoadAmmoCluster(weaponSetting.WeaponType);
        Debug.Log(currentAmmo);
        if (currentAmmo == -1) currentAmmo = weaponSetting.StartAmmo;
        Debug.Log(currentAmmo);
        MainHud.onAmmoSet(currentAmmo.ToString());
    }

    void OnEnable()
    {
        weaponMaterial.SetTexture("_MainTex", weaponSetting.Sprites[0]);
        MainHud.onAmmoSet(currentAmmo.ToString());
        WeaponManager.onShot += Shoot;
    }

    void OnDisable()
    {
        WeaponManager.onShot -= Shoot;
    }

    public void SetDamage()
	{
		int num = 2;
		num = ~num;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, /*weaponParameter.Range*/ 10, num) && hitInfo.collider != null)
		{
            Prop prop = hitInfo.collider.GetComponent<Prop>();
            if (prop != null)
            {
                prop.Health = weaponSetting.Power;
            }
        }
	}

    public void Shoot()
    {
        if (WeaponManager.isShooting || currentAmmo < weaponSetting.BulletsPerShot) return;
        WeaponManager.isShooting = true;
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        //Shoot;
        currentAmmo -= weaponSetting.BulletsPerShot;

        MainHud.onAmmoSet?.Invoke(currentAmmo.ToString());
        SoundManager.onWeaponPlay?.Invoke(weaponSetting.Shot);
        SetDamage();

        //Animation;
        for (int i = 1; i < weaponSetting.Sprites.Length; i++)
        {
            weaponMaterial.SetTexture("_MainTex", weaponSetting.Sprites[i]);
            yield return new WaitForSeconds(weaponSetting.RepetitionSpeed / 10f);
        }
        weaponMaterial.SetTexture("_MainTex", weaponSetting.Sprites[0]);
        yield return new WaitForSeconds(weaponSetting.RepetitionSpeed / 10f);
        WeaponManager.isShooting = false;
    }
}
