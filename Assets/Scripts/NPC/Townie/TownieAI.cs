using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TownieAI : MonoBehaviour
{
    [SerializeField] private TopDownController _controller;
    public GameObject target;
    public float minimumDistance;
    [SerializeField] private CombatController _combatController;
    // private Animator _animator;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        _controller = gameObject.GetComponent<TopDownController>();
        _combatController = gameObject.GetComponent<CombatController>();
        // agent = gameObject.GetComponent<NavMeshAgent>();
        // _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, target.transform.position) > minimumDistance)
        {
            //_controller.Move(target.position);
            // agent.destination = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _controller.speed * Time.deltaTime);

            //_animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        
        else
        {
            //attack
            _combatController.MeleeAttack();
        }
        
        
    }

    
}
