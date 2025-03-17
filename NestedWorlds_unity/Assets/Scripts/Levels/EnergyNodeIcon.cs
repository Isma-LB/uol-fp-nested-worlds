using UnityEngine;

namespace IsmaLB
{
    public class EnergyNodeIcon : MonoBehaviour
    {
        Transform target;
        void Start()
        {
            target = Camera.main.transform;
        }
        void LateUpdate()
        {
            SpinToFaceCamera();
        }

        private void SpinToFaceCamera()
        {
            // project the target direction on the up plane to only rotate there
            Vector3 dir = Vector3.ProjectOnPlane(target.position - transform.position, transform.up);
            transform.rotation = Quaternion.LookRotation(dir, transform.up);
        }
    }
}
