using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float patrolRadius = 3f;  
    public float moveSpeed = 1f;      
    public float changeTargetDistance = 0.5f;  
    public bool hit = false;
 
    public GameObject prefab;
    public int hp;
    public bool canBeHit = true; 
    IngameUI ingame;

    private Vector3 startPosition;    
    private Vector3 targetPosition;   

    public AudioSource audioSource;

    enum BearState
    {
        B_gethit, B_death, B_walk
    }
    BearState state = BearState.B_walk;

    Animator bear_animator;

    void Start()
    {
        ingame = GameObject.Find("UIManager").GetComponent<IngameUI>();
        
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position; 
        SetRandomTargetPosition();          
        bear_animator = GetComponent<Animator>();
        hp = 10;
    }

    void Update()
    {
        if (hp <= 0)
            return;

        if (hit == true)
        {
            IsGetHit();
        }
        else
        {
            MoveToTarget();
            this.transform.LookAt(targetPosition);
            if (Vector3.Distance(transform.position, targetPosition) < changeTargetDistance)
            {
                SetRandomTargetPosition();
            }
        }
    }

    private void LateUpdate()
    {
        tmp();
        if (hp <= 0)
        {
            EnemyDie();
        }
    }

    void SetRandomTargetPosition()
    {
      
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += startPosition;
        randomDirection.y = transform.position.y;
        targetPosition = randomDirection;
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("beartree") || collision.gameObject.CompareTag("terrain"))
        {
            SetRandomTargetPosition();
        }
    }

    void SetBearAni()
    {
        switch (state)
        {
            case BearState.B_walk:
                bear_animator.SetBool("bear_walk", true);
                bear_animator.SetBool("bear_gethit", false);
                break;
            case BearState.B_gethit:
                bear_animator.SetBool("bear_walk", false);
                bear_animator.SetBool("bear_gethit", true);
                break;
            case BearState.B_death:
                bear_animator.SetBool("bear_walk", false);
                bear_animator.SetBool("bear_gethit", false);
                bear_animator.SetBool("bear_death", true);
                break;
        }
    }

    void ChangeState(BearState value)
    {
        state = value;
        SetBearAni();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBeHit && other.CompareTag("sword"))
        {
            canBeHit = false;
            StartCoroutine(EnableHitAfterDelay(0.7f)); 

            audioSource.Play();
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            hit = true;
            ChangeState(BearState.B_gethit);
            hp = hp - 1;
            Debug.Log(hp);
        }
    }

    IEnumerator EnableHitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canBeHit = true;
    }

    void tmp()
    {
        if (bear_animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_GetHitFromFront"))
        {
            if (bear_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                ChangeState(BearState.B_walk);
            }
        }
    }

    void IsGetHit()
    {
        moveSpeed = 3.0f;
        patrolRadius = 10.0f;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= patrolRadius)
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            hit = false;
            patrolRadius = 5.0f;
            moveSpeed = 1.0f;
        }
    }

    void EnemyDie()
    {
        ChangeState(BearState.B_death);

        if (bear_animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Death"))
        {
            if (bear_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(this.gameObject);
                ingame.IncreaseLeather();
            }
        }
    }
}