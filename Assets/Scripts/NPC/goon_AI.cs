using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class goonAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent navMeshAgent;

    [Header("Values")]
    [SerializeField] float health;
    [SerializeField] float goonSpeed;
    [SerializeField] float startChaseDistance;
    [SerializeField] float stopDistance;

    bool provoked = false;
    float maxhp;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
        //tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float Distance = Vector3.Distance(target.position, transform.position);
        if (Distance <= startChaseDistance || provoked)
        {
            navMeshAgent.SetDestination(target.position);
            navMeshAgent.speed = goonSpeed;
            navMeshAgent.stoppingDistance = stopDistance;
            facetarget();
            provoked = true;
        }
        if(health == maxhp && health != 0)
        {
            provoked = true;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void facetarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, transform.position.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, 5f * Time.deltaTime);
    }

    public void tookDamage(float damagetaken)
    {
        health -= damagetaken;
    }
}
