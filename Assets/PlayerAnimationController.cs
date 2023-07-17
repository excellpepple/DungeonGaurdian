using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator PlayerAnim;

    [SerializeField]
    private Health PlayerHealth;

    [SerializeField]
    private CombatController CombatController;

    [Space]
    [SerializeField]
    private ParticleSystem HitEffecct;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerHealth.OnHealthChanged += OnGetHit;

        CombatController.OnAttack += Attack;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnGetHit(float h)
    {
        PlayerAnim.SetTrigger("Hit");

        if (HitEffecct != null)
            HitEffecct.Play();
    }

    private void Attack()
    {
        PlayerAnim.SetTrigger("Attack");
    }
}