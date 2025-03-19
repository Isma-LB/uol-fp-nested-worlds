using UnityEngine;

namespace IsmaLB.Cameras
{
    [ExecuteAlways]
    public class SkyBoxController : MonoBehaviour
    {
        [SerializeField] Transform sunLight;
        [SerializeField] Material skyBoxMaterial;
        [SerializeField] PlanetEventSO planetChangedEvent;

        void OnEnable()
        {
            if (planetChangedEvent)
                planetChangedEvent.OnPlanetEvent += HandlePlanetChange;
        }
        void OnDisable()
        {
            if (planetChangedEvent)
                planetChangedEvent.OnPlanetEvent -= HandlePlanetChange;
        }

        private void HandlePlanetChange(Vector3 position)
        {
            UpdateSpherePosition(position);
        }

        void LateUpdate()
        {
            UpdateSunDirection();
        }

        void UpdateSunDirection()
        {
            skyBoxMaterial.SetVector("_sun_dir", sunLight.forward);
        }

        void UpdateSpherePosition(Vector3 pos)
        {
            skyBoxMaterial.SetVector("_sphere_center", pos);
        }
    }
}
