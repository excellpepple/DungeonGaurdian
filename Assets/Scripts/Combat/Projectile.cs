using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damageAmount;
    private LayerMask enemyLayer;

    public string TargetTag;

    public float moveSpeed = 10;

    public Side ProjectileSide;

    private bool hasLanded;

    [SerializeField]
    private ParticleSystem onLandParticle;

    [SerializeField]
    private ParticleSystem trailParticle;

    private void Update()
    {
        if (!hasLanded)
            MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }

    public void Initialize(int damage, LayerMask layer)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fireballFired, this.transform.position);
        damageAmount = damage;
        enemyLayer = layer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Envo")
        {
            Land();
            return;
        }
        if (ProjectileSide == Side.player)
        {
            if (!other.CompareTag("Enemy"))
                return;
        }
        else
        {
            if (!other.CompareTag("Player"))
                return;
        }

        IDamagable damageable = other.GetComponent<IDamagable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
        Land();
    }

    private void Land()
    {
        if (onLandParticle != null)
        {
            onLandParticle.gameObject.SetActive(true);
            onLandParticle.Play();
        }

        if (trailParticle != null)
            trailParticle.Stop();

        hasLanded = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fireballExplosion, this.transform.position);
        Destroy(gameObject, 3);
    }
}

public enum Side
{
    player,
    Enemy
}