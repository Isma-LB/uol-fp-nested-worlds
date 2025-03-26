using System;
using UnityEngine;

namespace IsmaLB.SphereGravity
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : AttractorAligned
    {
        [SerializeField] bool autoAlignRotation = true;
        [SerializeField, Range(0.001f, 1)] float alignRotationSpeed = 0.1f;
        Rigidbody rb;

        public Rigidbody Rigidbody { get => rb; }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            if (attractor == null) return;
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
