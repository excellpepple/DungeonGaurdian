using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoverArea : MonoBehaviour
{
    private CoverSpots[] covers;
    private void Awake()
    {
        covers = new[] {GetComponentInChildren<CoverSpots>()};
    }

    public CoverSpots GetRandomCover(Vector3 agentLocation)
    {
        return covers[Random.Range(0, covers.Length - 1)];
    }
}
