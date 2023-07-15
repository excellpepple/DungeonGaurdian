using Systems.Health;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    [SerializeField]
    private IDamagable component1;

    [SerializeField]
    private IHealable component1Heal;

    [SerializeField]
    private IDamagable component2;

    [SerializeField]
    private IHealable component2Heal;

    public void Start()
    {
        component1 = GameObject.Find("Enemy").GetComponent<IDamagable>();
        component1Heal = GameObject.Find("Enemy").GetComponent<IHealable>();


        component2 = GameObject.Find("NPC").GetComponent<IDamagable>();
        component2Heal = GameObject.Find("NPC").GetComponent<IHealable>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            component1Heal.TakeHealth(23);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            component1.TakeDamage(13);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            component2Heal.TakeHealth(23);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            component2.TakeDamage(13);
        }
    }

}
