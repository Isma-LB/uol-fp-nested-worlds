using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles.Particles
{

    public class ParticlePool : MonoBehaviour
    {
        List<GameObject> particlesPool = new();
        [SerializeField] int particleMaxCount = 500;
        [SerializeField] Particle particlePrefab;
        [SerializeField, Range(1, 5)] float particleInitialVelocity = 2;
        [SerializeField, Range(1, 10)] float particleDisableDelay = 2;

        public static ParticlePool SharedInstance;

        void Awake()
        {
            SharedInstance = this;
        }
        void Start()
        {
            Particle temp;
            for (int i = 0; i < particleMaxCount; i++)
            {
                temp = Instantiate(particlePrefab, transform);
                temp.Setup(particleDisableDelay, particleInitialVelocity);
                temp.gameObject.SetActive(false);
                particlesPool.Add(temp.gameObject);
            }
        }
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < particlesPool.Count; i++)
            {
                if (!particlesPool[i].activeSelf)
                {
                    return particlesPool[i];
                }
            }

            return null;
        }
    }
}
