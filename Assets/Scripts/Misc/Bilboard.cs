using System.Collections;
using System.Collections.Generic;
using Systems.Health;
using UnityEngine;

public class Bilboard : MonoBehaviour
{

    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(_camera.transform);
    }
}
