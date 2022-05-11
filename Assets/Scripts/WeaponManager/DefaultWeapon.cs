using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultWeapon : MonoBehaviour
{
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

	protected virtual void Damage(int damageValue)
	{

		int num = 2;
		num = ~num;
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, weaponParameter.Range, num) && hitInfo.collider != null)
		{
            //if (hitInfo.collider.tag == "Robot")
            //{
            //	Object.Instantiate(RobotParticle, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.LookRotation(hitInfo.normal));
            //	hitInfo.collider.GetComponent<EnemyBase>().Hitting(damageValue + Parameters.AddDamage);
            //}
            if (hitInfo.collider.tag == "Barrel")
            {
                hitInfo.collider.GetComponent<ExplosionBarrel>().GetExplosion();
            }
        }
	}
}
