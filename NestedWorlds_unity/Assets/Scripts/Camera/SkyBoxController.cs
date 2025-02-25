using UnityEngine;

namespace IsmaLB.Cameras
{
    [ExecuteAlways]
    public class SkyBoxController : MonoBehaviour
    {
        [SerializeField] Transform sunLight;
        [SerializeField] Material skyBoxMaterial;

        void LateUpdate()
        {
            UpdateSunDirection();
        }

        void UpdateSunDirection()
        {
            skyBoxMaterial.SetVector("_sun_dir", sunLight.forward);
        }
    }
}
