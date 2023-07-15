using UnityEngine;

namespace Systems.Health
{
    [RequireComponent(typeof(Health))]
    public class HealthChangeHandler : MonoBehaviour
    {

        private Health health;

        private float currentHealth;

        [SerializeField]
        private GameObject bloodSplatter;

        [SerializeField]
        private GameObject healingEffect;

        private void Start()
        {
            health = GetComponent<Health>();

            health.OnHealthChanged += HandleHealthChange;
        }

        private void HandleHealthChange(float newValue)
        {
            if(currentHealth > newValue)
            {
                Instantiate(bloodSplatter, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(healingEffect, transform.position, transform.rotation);
            }
        }

        private void OnEnable()
        {
            health.OnHealthChanged += HandleHealthChange;
        }

        private void OnDisable()
        {
            health.OnHealthChanged -= HandleHealthChange;
        }

    }
}