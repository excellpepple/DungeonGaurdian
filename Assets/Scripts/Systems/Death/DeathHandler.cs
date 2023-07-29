using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Health
{

    [RequireComponent(typeof(Health))]
    public class DeathHandler : MonoBehaviour
    {

        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();

            health.OnDie += HandleDeath;
        }

        private void HandleDeath()
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.DeathSound, this.transform.position);
            Destroy(gameObject, 2f);
        }

        private void OnEnable()
        {
            if (health != null)
                health.OnDie += HandleDeath;
        }

        private void OnDisable()
        {
            health.OnDie -= HandleDeath;
        }

    }

}