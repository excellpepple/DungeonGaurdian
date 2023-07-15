using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private TopDownController _controller;
    public Transform[] patrolPoints;
    public float waitTime;

    private int currentPointIndex;

    private bool once;
    //public NavMeshAgent agent;
    //public LayerMask whatIsGround, whatIsPlayer;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<TopDownController>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position,
                _controller.speed * Time.deltaTime);
        }
        if(once == false)
        {
            once = true;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}
