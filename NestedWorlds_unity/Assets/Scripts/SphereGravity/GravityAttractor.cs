using UnityEngine;

namespace IsmaLB.SphereGravity
{
    public class GravityAttractor : MonoBehaviour
    {
        [SerializeField] float gravity = -10f;
        public void Attract(Rigidbody rb)
        {
            Vector3 gravityUp = (rb.transform.position - transform.position).normalized;
            rb.AddForce(gravityUp * gravity);
        }
        public Quaternion Align(Rigidbody rb, float slerpSpeed)
        {
            Vector3 gravityUp = (rb.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(rb.transform.up, gravityUp) * rb.transform.rotation;
            targetRotation = Quaternion.Slerp(rb.rotation, targetRotation, slerpSpeed);
            return targetRotation;
        }
        public Quaternion Align(Transform body)
        {
            Vector3 gravityUp = (body.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(body.up, gravityUp) * body.rotation;
            return targetRotation;
        }
    }
}
