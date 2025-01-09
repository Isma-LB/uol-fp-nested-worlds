using UnityEngine;

namespace IsmaLB.Planets
{
    [RequireComponent(typeof(GravityAttractor))]
    public class Planet : MonoBehaviour
    {
        public GravityAttractor Attractor { get; private set; }
        public int SceneIndex { get => gameObject.scene.buildIndex; }
        void Awake()
        {
            Attractor = GetComponent<GravityAttractor>();
            PlanetsListSO.OnPlanetLoaded(this);
        }
        void OnDestroy()
        {
            PlanetsListSO.OnPlanetUnloaded(this);
        }
    }
}
