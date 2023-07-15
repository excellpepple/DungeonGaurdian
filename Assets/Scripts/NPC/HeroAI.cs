using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;
using UnityEngine.AI;

public class HeroAI : MonoBehaviour
{
    public Transform target;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float fleeHealthThreshold = 30f;
    public float fleeRadius = 5f;
    public float meleeAttackCooldown = 2f;
    public float backupDistance = 1f;

    private Animator animator;
    private Health enemyHealth;
    private float originalMoveSpeed;
    private bool isAttacking;
    private float attackCooldownTimer;

    public HeroController characterController;
    public CombatController combatController;

    private Vector3 backupStartPosition;
    private Vector3 backupTargetPosition;
    private float backupStartTime;
    private float backupDuration = 0.5f;

    public EnemyType EnemyType;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();
        originalMoveSpeed = characterController.moveSpeed;

        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRadius)
        {
            RotateTowards(target.position);

            if (distanceToTarget <= attackRadius)
            {
                if (!isAttacking && attackCooldownTimer <= 0f)
                    MeleeAttack();
            }
            else if (enemyHealth.CurrentHealthPercentage() <= fleeHealthThreshold)
            {
                Flee();
            }
            else
            {
                Chase();
            }
        }

        attackCooldownTimer -= Time.deltaTime;
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, characterController.rotationSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        Vector3 moveDirection = (target.position - transform.position).normalized;
        moveDirection.y = 0f;
        transform.position += moveDirection * characterController.moveSpeed * Time.deltaTime;
    }

    private void MeleeAttack()
    {
        if (EnemyType == EnemyType.Warrior)
            combatController.MeleeAttack();
        else
            combatController.RangedAttack();

        Vector3 backupDirection = (transform.position - target.position).normalized;
        backupDirection.y = 0f;
        backupStartPosition = transform.position;
        backupTargetPosition = transform.position + backupDirection * backupDistance;
        backupStartTime = Time.time;

        attackCooldownTimer = meleeAttackCooldown;
        isAttacking = true;
        Invoke(nameof(ResetAttackState), meleeAttackCooldown);
    }

    private void ResetAttackState()
    {
        isAttacking = false;
    }

    private void Flee()
    {
        Vector3 fleeDirection = (transform.position - target.position).normalized;
        fleeDirection.y = 0f;
        Vector3 fleePosition = transform.position + fleeDirection * fleeRadius;

        Vector3 moveDirection = (fleePosition - transform.position).normalized;
        moveDirection.y = 0f;
        Vector3 fleeVelocity = moveDirection * characterController.moveSpeed * Time.deltaTime;

        // Reduce move speed to create a slower movement during fleeing
        fleeVelocity *= 0.25f; // Adjust the factor as needed for desired speed

        characterController.Move(fleeVelocity);
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            float progress = (Time.time - backupStartTime) / backupDuration;
            progress = Mathf.Clamp01(progress);

            Vector3 newPosition = Vector3.Lerp(backupStartPosition, backupTargetPosition, progress);
            characterController.Move(newPosition - transform.position);

            if (progress >= 1f)
            {
                isAttacking = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fleeRadius);
    }
   
}

public enum EnemyType
{
    Warrior,
    Mage
}
