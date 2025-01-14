using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles.Particles
{

    public class ParticlePool : MonoBehaviour
    {
        List<GameObject> particlesPool = new();
        [SerializeField] int particleMaxCount = 500;
        [SerializeField] GameObject particlePrefab;

        public static ParticlePool SharedInstance;

        void Awake()
        {
            SharedInstance = this;
        }
        void Start()
        {
            GameObject temp;
            for (int i = 0; i < particleMaxCount; i++)
            {
                temp = Instantiate(particlePrefab, transform);
                temp.SetActive(false);
                particlesPool.Add(temp);
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
