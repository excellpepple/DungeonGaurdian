using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private TopDownController _controller;
    public Transform target;
    public float minimumDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _controller = gameObject.GetComponent<TopDownController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > minimumDistance)
        {
            //_controller.Move(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, _controller.speed * Time.deltaTime);
        }
        else
        {
            //attack
        }
        
    }
}
