using System;
using System.Collections;
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
        public IEnumerator TransitionTransform(float deltaScale, AnimationCurve speed)
        {
            Vector3 initialScale = transform.localScale;
            Vector3 targetScale = transform.localScale * deltaScale;

            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = transform.position * deltaScale;

            float t = 0;
            yield return new WaitForFixedUpdate();
            while (t < 1)
            {
                t += Time.fixedDeltaTime * speed.Evaluate(t);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                yield return new WaitForFixedUpdate();
            }
            transform.localScale = targetScale;
            transform.position = targetPosition;
        }
    }
}
