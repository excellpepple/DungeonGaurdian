using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private UnityEngine.CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<UnityEngine.CharacterController>();
    }

    public void Move(Vector3 direction)
    {
        characterController.SimpleMove(direction * moveSpeed);
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
