using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    public GameObject Explosion;

    public void GetExplosion()
    {
        gameObject.SetActive(false);
        SoundManager.onExplosionPlay?.Invoke();
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
    }
}
