using System;
using UnityEngine;

namespace IsmaLB.Planets
{
    [RequireComponent(typeof(GravityAttractor))]
    public class Planet : MonoBehaviour
    {
        [SerializeField] Transform nextPlanetPlaceholder = null;
        [SerializeField, Min(1)] float scaleDelta = 20;
        public enum Transition { forward, current, backwards };
        public GravityAttractor Attractor { get; private set; }
        public int SceneIndex { get => gameObject.scene.buildIndex; }
        Vector3 placeHolderOffset;
        bool setup = false;
        void Awake()
        {
            Attractor = GetComponent<GravityAttractor>();
            gameObject.SetActive(false);
            placeHolderOffset = transform.TransformDirection(nextPlanetPlaceholder.localPosition);
            PlanetsListSO.OnPlanetLoaded(this);
        }
        void OnDestroy()
        {
            PlanetsListSO.OnPlanetUnloaded(this);
        }
        public void SetupRelativeTo(Planet origin, Transition transition)
        {
            if (setup) return;
            Activate();

            if (transition == Transition.forward)
            {
                transform.localScale /= scaleDelta;
                transform.position = origin.transform.position * 1 / origin.transform.localScale.x + origin.placeHolderOffset;
            }
            else if (transition == Transition.backwards)
            {
                transform.localScale *= scaleDelta;
                transform.position = origin.transform.position * 1 / origin.transform.localScale.x + placeHolderOffset * -scaleDelta;
            }
        }

        internal void SetupAsCurrent() => Activate();
        void Activate()
        {
            setup = true;
            gameObject.SetActive(true);
        }
    }
}
