using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooters : MonoBehaviour
{
    [SerializeField] private TopDownController _controller;
    public Transform target;
    public float minimumDistance;

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _controller = gameObject.GetComponent<TopDownController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }
        
        if (Vector3.Distance(transform.position, target.position) < minimumDistance)
        {
            //_controller.Move(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, -_controller.speed * Time.deltaTime);
        }
        else
        {
            //attack
        }
        
    }
}
