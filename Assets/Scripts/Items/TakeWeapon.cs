using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    public Weapons Weapon;
    public WeaponParameter Parameter;
    public AudioClip TakeSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !WeaponManager.isShooting)
        {
            WeaponManager.onChange?.Invoke(Weapon);
            SoundManager.onPlayerPlay?.Invoke(TakeSound);
            gameObject.SetActive(false);
        }
    }
}
