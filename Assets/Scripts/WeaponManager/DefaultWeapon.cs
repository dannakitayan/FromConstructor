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
        }
    }
    public bool CanSeeTheWeapon
    {
        get
        {
            return currentAmmo > 0;
        }
    }

    void Awake()
    {
        weaponMaterial = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        currentAmmo = weaponSetting.StartAmmo;
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

    public void Damage()
	{
		int num = 2;
		num = ~num;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, /*weaponParameter.Range*/ 1, num) && hitInfo.collider != null)
		{
            if (hitInfo.collider.tag == "Barrel")
            {
                hitInfo.collider.GetComponent<ExplosionBarrel>().GetExplosion();
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
