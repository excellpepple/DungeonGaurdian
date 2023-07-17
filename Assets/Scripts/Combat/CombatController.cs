using System;
using System.Text;
using Systems.Health;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ManaChangeEvent : UnityEvent<int, int>
{ }

public class CombatController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private MeleeWeaponSO meleeWeapon;
    [SerializeField] private RangedWeapon rangedWeapon;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private float manaRegenerationRate = 10f;

    public event Action OnAttack;

    public ManaChangeEvent onManaChange;

    private int currentMana;
    private bool canAttack = true;
    public bool canShoot = false;
    public bool canDash = false;

    private void Start()
    {
        currentMana = maxMana;
        StartCoroutine(ManaRegeneration());
    }

    public void MeleeAttack()
    {
        if (!canAttack)
        {
            return;
        }

        OnAttack?.Invoke();

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, meleeWeapon.attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            IDamagable damageable = enemy.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(meleeWeapon.attackDamage);
            }
        }

            ;
        StartCoroutine(AttackDelay(meleeWeapon.attackDelay));
        //onManaChange.Invoke(currentMana, maxMana);
    }

    public void RangedAttack()
    {
        if (!canAttack)
        {
            return;
        }

        if (currentMana >= rangedWeapon.manaCost)
        {
            GameObject projectile = Instantiate(rangedWeapon.projectilePrefab, attackPoint.position, transform.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();

            if (projectileScript != null)
            {
                projectileScript.Initialize(rangedWeapon.attackDamage, enemyLayer);
            }

            currentMana -= rangedWeapon.manaCost;
            StartCoroutine(AttackDelay(rangedWeapon.attackDelay));
            onManaChange.Invoke(currentMana, maxMana);
        }
    }

    private System.Collections.IEnumerator AttackDelay(float delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    public System.Collections.IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }

    private System.Collections.IEnumerator ManaRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (currentMana < maxMana)
            {
                currentMana += Mathf.FloorToInt(manaRegenerationRate);
                currentMana = Mathf.Clamp(currentMana, 0, maxMana);
                onManaChange.Invoke(currentMana, maxMana);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, meleeWeapon.attackRange);
    }
}