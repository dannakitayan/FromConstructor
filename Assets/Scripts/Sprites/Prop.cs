using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] SpriteSetting spriteSetting;
    BoxCollider boxCollider;
    Material propMaterial;

    int health;
    PropState currentState = PropState.Normal;
    bool isHitting;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            if (spriteSetting.Endurance == 0 || isHitting) return;
            health -= Mathf.Abs(value); //To avoid healing the object;
            TakeDamage();
        }
    }


    void Awake()
    {
        propMaterial = meshRenderer.material;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        if(!spriteSetting.IsSolid)
        {
            boxCollider.enabled = false;
            return;
        }

        health = spriteSetting.Endurance;
    }

    void TakeDamage()
    {
        if (currentState == PropState.Destroyed) return;
        if (health <= 0)
        {
            currentState = PropState.Destroyed;
            StartCoroutine(Destroy());
        }
        else
        {
            isHitting = true;
            StartCoroutine(Hit());
        }
    }

    IEnumerator Destroy()
    {
        //Animation;
        for (int i = 1; i < spriteSetting.Sprites.Length; i++)
        {
            propMaterial.SetTexture("_MainTex", spriteSetting.Sprites[i]);
            yield return new WaitForSeconds(0.1f);
        }

        Destroyed();
    }

    IEnumerator Hit()
    {
        propMaterial.SetTexture("_MainTex", spriteSetting.Sprites[1]);
        yield return new WaitForSeconds(0.1f);
        propMaterial.SetTexture("_MainTex", spriteSetting.Sprites[0]);
        isHitting = false;
    }

    void Destroyed()
    {
        propMaterial.SetTexture("_MainTex", spriteSetting.Sprites[spriteSetting.Sprites.Length-1]);
        //sound play;
        boxCollider.enabled = false;
    }    
}
