using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent navMeshAgent;

    [Header("Values")]
    [SerializeField] private float health;

    [SerializeField] private float ArcherSpeed;
    [SerializeField] private float shootDistance;
    [SerializeField] private float stopDistance;
    [SerializeField] private float arrowMaxDistance;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float Distance = Vector3.Distance(target.position, transform.position);
        //attack();
        Invoke("attack", 0.25f);
        if (Distance < stopDistance - 1)
        {
            navMeshAgent.SetDestination(target.position * shootDistance);
        }
        else if (Distance != stopDistance)
        {
            navMeshAgent.SetDestination(target.position);
            navMeshAgent.speed = ArcherSpeed;
        }
        //if u want it to shoot from certain distance
        //other wise just use attack so it can attack all the time while he is running back too like a expert archer.
        //if (Distance == shootDistance)
        //{
        //    Invoke("attack", 0.25f);
        //}
        navMeshAgent.stoppingDistance = stopDistance;
        facetarget();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void facetarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, transform.position.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, 5f * Time.deltaTime);
    }

    public void tookDamage(float damagetaken)
    {
        health -= damagetaken;
    }

    private void attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, arrowMaxDistance))
        {
            //make sure to change the script name to the player script name other wise it will cause error
            PlayerInput target = hit.transform.GetComponent<PlayerInput>();
            if (target == null) return;
            //target.takeDamage(damage);
            //Debug.Log("hit");
        }
        else
        {
            return;
        }
    }
}