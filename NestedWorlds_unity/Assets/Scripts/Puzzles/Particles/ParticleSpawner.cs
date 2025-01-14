using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles.Particles
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] float spawnRate = 1;
        GameObject tempParticle;

        IEnumerator Start()
        {
            while (true)
            {
                SpawnParticle();
                yield return new WaitForSeconds(spawnRate);
            }

        }

        void SpawnParticle()
        {
            tempParticle = ParticlePool.SharedInstance.GetPooledObject();
            if (tempParticle != null)
            {
                tempParticle.transform.SetPositionAndRotation(transform.position, transform.rotation);
                tempParticle.SetActive(true);
            }
        }
    }
}