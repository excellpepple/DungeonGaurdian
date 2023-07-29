using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Fireball SFX")]
    [field: SerializeField] public EventReference fireballFired { get; private set; }
    [field: SerializeField] public EventReference fireballExplosion{ get; private set; }
    
    [field: Header("Arrow SFX")]
    [field: SerializeField] public EventReference ArrowReleased { get; private set; }
    [field: SerializeField] public EventReference ArrowHit { get; private set; }
    
    [field: Header("Sword SFX")]
    [field: SerializeField] public EventReference SwordSwing { get; private set; }
    [field: SerializeField] public EventReference SwordHit { get; private set; }
    
    [field: Header("Death SFX")]
    [field: SerializeField] public EventReference DeathSound { get; private set; }
    
    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference CursorSfx { get; private set; }
    [field: SerializeField] public EventReference PauseSfx { get; private set; }
    [field: SerializeField] public EventReference SelectSfx { get; private set; }
    
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
