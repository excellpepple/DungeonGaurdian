using UnityEngine;
using Systems.Health;
using UnityEngine.AI;

namespace NPC.Archer
{
    [RequireComponent(typeof(Rigidbody), typeof(Health))]
    public class ArcherBrain : MonoBehaviour
    {

        [SerializeField]
        private Transform target;
        [SerializeField]
        private float maxDistance;
        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float reloadTime = 0.3f;

        private float reloadTimeLeft = 0;

        [SerializeField]
        private Transform shootPosition;
        [SerializeField]
        private GameObject arrowPrefab;

        bool isDead = false;

        private new Rigidbody rigidbody;
        private Health health;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            health = GetComponent<Health>();

            if (!target)
            {
                target = GameObject.Find("Player").transform;
            }

            health.OnDie += Die;
        }

        private bool IsInTargetRange()
        {
            float currentDistance = Mathf.Abs(Vector3.Distance(target.position, transform.position));

            if (currentDistance < maxDistance)
                return true;
            return false;
        }

        private void Update()
        {
            reloadTimeLeft -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (isDead)
                return;

            Vector3 targetPos = new Vector3(target.position.x, 0, target.position.z);
            Vector3 localPos = new Vector3(transform.position.x, 0, transform.position.z);
            rigidbody.MoveRotation(Quaternion.LookRotation(targetPos - localPos));
            if (IsInTargetRange())
            {
                Attack();
            }
            else
            {
                Move();
            }

            
        }

        private void Attack()
        {
            if (reloadTimeLeft <= 0)
            {
                Instantiate(arrowPrefab, shootPosition.position, transform.rotation);
                reloadTimeLeft = reloadTime;
            }
        }

        private void Die()
        {
            isDead = true;
        }

        private void Move()
        {
            Vector3 movementVector = target.position - transform.position;
            rigidbody.velocity = movementVector * (Time.fixedDeltaTime * speed);
        }

        private void OnEnable()
        {
            if (health != null)
                health.OnDie += Die;
        }

        private void OnDisable()
        {
            health.OnDie -= Die;
        }

    }
}
