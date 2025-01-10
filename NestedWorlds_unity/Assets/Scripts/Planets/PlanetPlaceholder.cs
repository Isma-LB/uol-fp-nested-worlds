using UnityEngine;

namespace IsmaLB.Planets
{
    public class PlanetPlaceholder : MonoBehaviour
    {
        [SerializeField, TagSelector] string playerTag = "Player";
        public bool IsPlayerInRange { get; private set; } = false;
        void Start()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                IsPlayerInRange = true;
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                IsPlayerInRange = false;
            }
        }
    }
}
