using UnityEngine;

namespace IsmaLB
{
    public class GravityAttractor : MonoBehaviour
    {
        [SerializeField] float gravity = -10f;
        internal void Attract(Rigidbody rb)
        {
            Vector3 gravityUp = (rb.position - transform.position).normalized;
            rb.AddForce(gravityUp * gravity);
        }
        internal Quaternion Align(Rigidbody rb, float slerpSpeed)
        {
            Vector3 gravityUp = (rb.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(rb.transform.up, gravityUp) * rb.transform.rotation;
            targetRotation = Quaternion.Slerp(rb.rotation, targetRotation, slerpSpeed);
            return targetRotation;
        }
    }
}
