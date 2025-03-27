using System;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        [SerializeField, Range(0, 5)] float radius = 2f;
        [SerializeField] LayerMask intractableLayer;
        private Collider[] results = new Collider[10];
        IIntractable currentIntractable, tempIntractable, closestIntractable;

        void OnEnable()
        {
            inputReader.interactEvent += OnInteractInput;
        }
        void OnDisable()
        {
            inputReader.interactEvent -= OnInteractInput;
        }

        void Update()
        {
            FindIntractableItems();
        }
        void FindIntractableItems()
        {
            int detectedCount = Physics.OverlapSphereNonAlloc(transform.position, radius, results, intractableLayer);
            if (detectedCount > 0)
            {
                float minDistance = float.MaxValue;
                // get the closest
                for (int i = 0; i < detectedCount; i++)
                {
                    if (!results[i].TryGetComponent<IIntractable>(out tempIntractable)) continue;
                    float dist = Vector3.Distance(transform.position, results[i].transform.position);
                    if (dist < minDistance)
                    {
                        closestIntractable = tempIntractable;
                        minDistance = dist;
                    }
                }
                if (currentIntractable != closestIntractable)
                {
                    DeselectCurrent();
                    closestIntractable.Select();
                    currentIntractable = closestIntractable;
                }
            }
            else if (currentIntractable != null)
            {
                DeselectCurrent();
            }
        }

        private void OnInteractInput()
        {
            currentIntractable?.Interact();
        }
        void DeselectCurrent()
        {
            if ((Component)currentIntractable != null)
            {
                currentIntractable.Deselect();
                currentIntractable = null;
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
