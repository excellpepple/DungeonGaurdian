using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroAIBase : MonoBehaviour
{
    [Header("Components")]
    NavMeshAgent navMeshAgent;
    Transform target;
    [SerializeField] GameObject retreatBackToo;

    [Header("Values")]
    [SerializeField] float health;
    [SerializeField] float heroSpeed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float startChaseDistance;
    [SerializeField] float retreatHealth;
    [SerializeField] float retreatDistance;

    //[Header("Animator")]
    //Animator animator;

    [Header("Triggers")]
    Vector3 retreat;
    float distanceFromTarget;
    bool haveseen = false;
    bool attack;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //idleanimationtrue
        navMeshAgent.acceleration = heroSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
        retreat = target.position * retreatDistance;
        AI();
    }

    private void AI()
    {
        if (health <= retreatHealth && distanceFromTarget != retreatDistance)
        {
            haveseen = false;
            retreatTarget();
        }
        if (startChaseDistance >= distanceFromTarget && health > retreatHealth && !attack || haveseen)
        {
            ChaseTarget();
            haveseen = true;
        }
        if (stoppingDistance == distanceFromTarget)
        {
            attack = true;
            Attack();
            haveseen = false;
        }
        else
        {
            attack = false;
        }
        if (health <= 0)
        {
            //deathAnimation
            Destroy(gameObject);
        }

    }

    private void Attack()
    {
        //Animation
    }

    void ChaseTarget()
    {
        //StartAnimationtomove
        //idleanimationtoflase
        navMeshAgent.SetDestination(target.position);
        Vector3 direction = (target.position - transform.position).normalized;
        faceTarget(direction);
    }

    void retreatTarget()
    {
        //StartAnimationtomove
        //idleanimationtoflase
        navMeshAgent.SetDestination(retreatBackToo.transform.position);
        Vector3 direction = -(target.position - transform.position).normalized;
        faceTarget(direction);
    }

    void faceTarget(Vector3 direction)
    {
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, transform.position.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, 5f * Time.deltaTime);
    }
    
    public void tookDamage(float damagetaken)
    {
        health -= damagetaken;
    }

}
