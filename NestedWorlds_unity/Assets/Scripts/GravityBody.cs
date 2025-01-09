using System;
using UnityEngine;

namespace IsmaLB
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : MonoBehaviour
    {
        [SerializeField] bool autoAlignRotation = true;
        [SerializeField, Range(0.001f, 1)] float alignRotationSpeed = 0.1f;
        public GravityAttractor attractor;
        Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            attractor.Attract(rb);
            if (autoAlignRotation)
                rb.MoveRotation(attractor.Align(rb, alignRotationSpeed));
        }
        void OnValidate()
        {
            if (TryGetComponent<Rigidbody>(out var rigidbody))
            {
                if (autoAlignRotation)
                {

                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                }
                else
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                }
            }
        }
    }
}
