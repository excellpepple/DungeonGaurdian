using UnityEngine;
using Systems.Spawner;

namespace Systems.Health
{

    public class Health : MonoBehaviour, IDamagable, IHealable
    {
        public float maxHealth;

        //Current Health
        private float currentHealth;

        //When the health drops below 0 fire this event.
        public delegate void Die();
        public event Die OnDie;

        private SpawnManager manager;

        //Everytime health changes fire this event.
        public delegate void HealthChanged(float currentHealth);
        public event HealthChanged OnHealthChanged;

        bool isDead;
        private void Start()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth);

            manager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnManager>();
        }

        public void TakeDamage(float damageToInflict)
        {
            if (isDead)
                return;

            currentHealth -= damageToInflict;
            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                OnDie?.Invoke();
                //manager.DecreaseCount();

                isDead = true;

                LevelManger.current.GainExperience(25);
            }
        }
        public void ChangeHealth(float newHealth)
        {
            maxHealth = newHealth;
            currentHealth = newHealth;
            OnHealthChanged?.Invoke(currentHealth);
        }

        public void TakeHealth(float healthToTake)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + healthToTake);
            OnHealthChanged?.Invoke(currentHealth);
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float CurrentHealthPercentage()
        {
            return (currentHealth / maxHealth) * 100;
        }
    }

}