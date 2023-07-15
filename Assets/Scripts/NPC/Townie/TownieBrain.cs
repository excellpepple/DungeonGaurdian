using UnityEngine;

namespace NPC.Townie
{
    public class TownieBrain : MonoBehaviour
    {

        [SerializeField]
        [Range(0, 1)]
        private float intensity = 0.3f;

        private void Update()
        {
            transform.position += new Vector3(Mathf.Sin(Time.time + Random.Range(0, 20)) , 0, Mathf.Sin(Time.time + Random.Range(0, 20))) * (Time.deltaTime * intensity);
        }
    }
}
