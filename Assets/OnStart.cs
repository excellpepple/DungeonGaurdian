using Lean.Transition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    [SerializeField] private LeanManualAnimation fade;

    private void Awake()
    {
        fade.BeginTransitions();
    }
}