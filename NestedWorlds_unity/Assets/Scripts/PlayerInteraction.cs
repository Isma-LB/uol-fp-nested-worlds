using System;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        SphereCollider trigger;
        IIntractable currentIntractable;

        void OnEnable()
        {
            trigger = GetComponent<SphereCollider>();
            inputReader.interactEvent += OnInteractInput;
        }
        void OnDisable()
        {
            inputReader.interactEvent -= OnInteractInput;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IIntractable>(out IIntractable intractable)) return;

            if (currentIntractable == null)
            {
                intractable.Select();
                currentIntractable = intractable;
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IIntractable>(out IIntractable intractable)) return;

            if (intractable == currentIntractable)
            {
                intractable.Deselect();
                currentIntractable = null;
                // Debug.Log("out range:" + other.name);
            }
        }

        private void OnInteractInput()
        {
            currentIntractable?.Interact();
            // Debug.Log("Interacted with: " + currentIntractable);
        }
    }
}
