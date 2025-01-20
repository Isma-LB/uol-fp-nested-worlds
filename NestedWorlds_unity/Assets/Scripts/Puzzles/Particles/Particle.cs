using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles.Particles
{
    public class Particle : MonoBehaviour
    {
        float initialVelocity;
        float disableDelay;
        Rigidbody2D rb;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void Setup(float _disableDelay, float _initialVelocity)
        {
            initialVelocity = _initialVelocity;
            disableDelay = _disableDelay;
        }
        void OnEnable()
        {
            rb.AddForce(transform.up * initialVelocity, ForceMode2D.Impulse);
            StartCoroutine(AutoDisable());
        }
        void OnDisable()
        {
            StopAllCoroutines();
        }
        IEnumerator AutoDisable()
        {
            yield return new WaitForSeconds(disableDelay);
            gameObject.SetActive(false);
        }
    }
}
