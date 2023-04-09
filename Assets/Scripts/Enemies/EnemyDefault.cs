using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDefault : MonoBehaviour
{
    [SerializeField] EnemySetting enemySetting;
    [SerializeField] MeshRenderer meshRenderer;

    CapsuleCollider characterController;
    NavMeshAgent agent;
    EnemyState state = EnemyState.Wait;

    int health;
    bool isMoving;
    bool isAttacking;

    public int Health
    {
        set
        {
            if (state == EnemyState.Damage) return;
            health -= Mathf.Abs(value); //To avoid healing the enemy;
            if(health <= 0)
            {
                state = EnemyState.Died;
                isMoving = false;
                isAttacking = false;
                characterController.enabled = false;
                agent.enabled = false;
                StartCoroutine(FinishedAnimation(enemySetting.Die));
            }
            else
            {
                Debug.Log("Damage");
                state = EnemyState.Damage;
                isMoving = false;
                isAttacking = false;
                StartCoroutine(FinishedAnimation(EnemyState.Wait, enemySetting.Die, 1));
            }
        }
    }


    void Awake()
    {
        characterController = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDestroy()
    {
        
    }

    void Start()
    {
        health = enemySetting.Endurance;
        agent.speed = enemySetting.Speed;
    }

    public virtual void Attack()
    {
        if (isAttacking) return;
        state = EnemyState.Attack;
        isMoving = false;
        isAttacking = true;
        StartCoroutine(Attacking(EnemyState.Attack, enemySetting.Attack));
    }

    public virtual void Walk()
    {
        if (isMoving) return;
        isMoving = true;
        isAttacking = false;
        state = EnemyState.Move;
        StartCoroutine(Animation(EnemyState.Move, enemySetting.Walk));
    }

    public virtual void Wait()
    {
        state = EnemyState.Wait;
        isMoving = false;
        isAttacking = false;
        meshRenderer.material.SetTexture("_MainTex", enemySetting.Walk[0]);
    }

    IEnumerator Attacking(EnemyState newState, Texture2D[] textures)
    {
        int Frame = 0;
        int endFrameNumber = textures.Length;
        while (state == newState)
        {
            if (state != newState) yield break;
            meshRenderer.material.SetTexture("_MainTex", textures[Frame]);
            Frame++;
            if (Frame > endFrameNumber - 1)
            {
                Frame = 0;
                yield return new WaitForSeconds(enemySetting.Time / 10f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Animation(EnemyState newState, Texture2D[] textures)
    {
        int Frame = 0;
        int endFrameNumber = textures.Length;
        while (state == newState)
        {
            if (state != newState) yield break;
            meshRenderer.material.SetTexture("_MainTex", textures[Frame]);
            Frame++;
            if (Frame > endFrameNumber - 1) Frame = 0;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator FinishedAnimation(Texture2D[] textures)
    {
        for (int i = 0; i < textures.Length; i++)
        {
            meshRenderer.material.SetTexture("_MainTex", textures[i]);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator FinishedAnimation(EnemyState newState, Texture2D[] textures, int finalFrame)
    {
        for (int i = 0; i < finalFrame; i++)
        {
            meshRenderer.material.SetTexture("_MainTex", textures[i]);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.6f);
        state = newState;
    }

    void AlreadyDied()
    {
        characterController.enabled = false;
        agent.enabled = false;
        meshRenderer.material.SetTexture("_MainTex", enemySetting.Die[enemySetting.Die.Length-1]);
    }

    void SaveState()
    {

    }

    void LoadState()
    {

    }

    void Update()
    {
        if (state == EnemyState.Died || state == EnemyState.Damage) return;

        Vector3 offset = Player.onPositionGet.Invoke() - transform.position;
        float distance = offset.sqrMagnitude;
        if (distance <= enemySetting.AttractionDistance && distance > enemySetting.AttackDistance / 2)
        {
            agent.isStopped = false;
            Walk();
            agent.destination = Player.onPositionGet.Invoke();
        }
        else if (distance <= enemySetting.AttackDistance)
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            agent.isStopped = true;
            Wait();
        }
    }
}
