using UnityEngine.UI;
using Systems.Health;
using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField] private Health health;

    [SerializeField] private Image healthBarFill;

    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;

        health.OnHealthChanged += UpdateUI;
    }
    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateUI;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(_camera.transform);
    }

    private void UpdateUI(float currentHealth)
    {
        healthBarFill.fillAmount = currentHealth / health.GetMaxHealth();
    }
}
