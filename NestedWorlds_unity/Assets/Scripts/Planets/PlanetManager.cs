using UnityEngine;

namespace IsmaLB.Planets
{
    public class PlanetManager : MonoBehaviour
    {
        [SerializeField] GravityBody player;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            player.attractor = FindAnyObjectByType<Planet>().Attractor;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
