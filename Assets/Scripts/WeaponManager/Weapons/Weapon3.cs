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

    protected override void Damage(int damageValue)
    {
        int num = 2;
        num = ~num;
        RaycastHit hitInfo;
        Vector3 randomDirection;
        for (float i = -3; i < 3; i+= 0.08f)
        {
            randomDirection = new Vector3(Camera.main.transform.forward.x - i, Camera.main.transform.forward.y, Camera.main.transform.forward.z);
            Debug.DrawLine(Camera.main.transform.position, randomDirection * weaponParameter.Range, Color.yellow);
            if (Physics.Raycast(Camera.main.transform.position, randomDirection, out hitInfo, weaponParameter.Range, num) && hitInfo.collider != null)
            {
                if (hitInfo.collider.tag == "Barrel")
                {
                    hitInfo.collider.GetComponent<ExplosionBarrel>().GetExplosion();
                }

                if (hitInfo.collider.tag == "Head")
                {
                    hitInfo.collider.GetComponent<Head>().Execution();
                }
            }
        }
        
    }

    IEnumerator GetShot()
    {
        animator.SetTrigger(weaponParameter.ShotTriggerName);
        SoundManager.onWeaponPlay?.Invoke(weaponParameter.Shot);
        Damage(8);
        WeaponManager.onWeaponFlashLight?.Invoke();
        MainCameraShake.onShake?.Invoke(weaponParameter.Angle);
        yield return new WaitForSeconds(weaponParameter.TimeBetweenShots);
        PlayerSingleton.Get().Player.ChangeAmmo(-2);
        canShot = true;
        WeaponManager.isShooting = !canShot;
    }
}
