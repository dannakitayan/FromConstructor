using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public enum ItemType
    {
        Medkit,
        Ammo
    }

    public ItemType Type;
    public AudioClip TakeSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Type == ItemType.Medkit && PlayerSingleton.Get().Player.Health < 100)
            {
                PlayerSingleton.Get().Player.Health += 25;
                SoundManager.onPlayerPlay?.Invoke(TakeSound);
                gameObject.SetActive(false);
            }

            if (Type == ItemType.Ammo && PlayerSingleton.Get().Player.GetAmmoCount() < PlayerSingleton.Get().Player.GetMaxAmmoCount())
            {
                PlayerSingleton.Get().Player.ChangeAmmo(10);
                SoundManager.onPlayerPlay?.Invoke(TakeSound);
                gameObject.SetActive(false);
            }
        }
    }
}
