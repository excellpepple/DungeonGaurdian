using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;

public class DmgEffect : MonoBehaviour
{
    [SerializeField] private Health health;

    [SerializeField] private ParticleSystem DmgParticleEffect;

    [SerializeField] private Lean.Transition.LeanManualAnimation dmgDisplay;

    [SerializeField] private Transform effectPoint;




    private void OnEnable()
    {


        health.OnHealthChanged += UpdateUI;
    }
    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateUI;
    }

    // Update is called once per frame


    private void UpdateUI(float currentHealth)
    {
        Instantiate(DmgParticleEffect.gameObject, position: effectPoint.position, rotation: new Quaternion());
        Instantiate(dmgDisplay, position: effectPoint.position, rotation: new Quaternion()).BeginTransitions();
    }
}
