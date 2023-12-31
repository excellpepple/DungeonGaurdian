using UnityEngine;
using Systems.Health;

namespace NPC.Archer
{

    [RequireComponent(typeof(Rigidbody))]
    public class Arrow : MonoBehaviour
    {

        [SerializeField]
        private float speed = 20;
        [SerializeField]
        private float damage = 15;
        private bool once = true;

        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject, 3f); //To Destory if nothing is hit
        }

        private void FixedUpdate()
        {
            if (once)
            {
                rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
                once = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamagable damagable = collision.transform.GetComponent<IDamagable>();

            damagable?.TakeDamage(damage);

            if(damagable != null)
                Destroy(gameObject);
        }

    }
}