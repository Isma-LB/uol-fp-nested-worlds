using UnityEngine;

namespace IsmaLB.Planets
{
    [RequireComponent(typeof(GravityAttractor))]
    public class Planet : MonoBehaviour
    {
        public GravityAttractor Attractor { get; private set; }

        void Awake()
        {
            Attractor = GetComponent<GravityAttractor>();
            if (PlanetManager.PlanetsList)
                PlanetManager.PlanetsList.OnPlanetLoaded(this);
        }
    }
}
