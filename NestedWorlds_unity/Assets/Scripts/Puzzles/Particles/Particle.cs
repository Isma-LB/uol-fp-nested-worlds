using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles.Particles
{
    public class Particle : MonoBehaviour
    {
        [SerializeField] float initialVelocity;
        [SerializeField] float disableDelay = 5;

        void OnEnable()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
