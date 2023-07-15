using UnityEngine;

namespace NPC.Townie
{

    [RequireComponent(typeof(Rigidbody))]
    public class TownieGroup : MonoBehaviour
    {

        [SerializeField]
        private Transform target;

        [SerializeField]
        private float attackDistance;

        [SerializeField]
        private float speed = 10f;

        private void Update()
        {
            if (Mathf.Abs(Vector3.Distance(target.position, transform.position)) <= attackDistance)
            {
                Debug.Log("Attack");
            }
            else
            {
                Vector3 movement = (target.position - transform.position) * (Time.deltaTime * speed);
                movement.y = 0;
                transform.position += movement;

                transform.LookAt(new Vector3(target.position.x, 0, target.position.y));
            }
        }

    }
}
