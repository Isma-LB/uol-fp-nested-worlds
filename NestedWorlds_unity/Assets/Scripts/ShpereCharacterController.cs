using UnityEngine;

namespace IsmaLB
{

    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class SphereCharacterController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 300f;
        [SerializeField] float jumpSpeed = 12f;
        [SerializeField]

        public bool IsGrounded { get; private set; }
        Rigidbody rb;
        CapsuleCollider capsuleCollider;
        RaycastHit groundHit;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            CheckGrounded();
        }
        public void Move(float forwardMovement, float rotationAmount, bool jump)
        {
            // position
            Vector3 deltaPosition = forwardMovement * Time.fixedDeltaTime * moveSpeed * GetProjectedForward();
            rb.MovePosition(deltaPosition + rb.position);
            // rotation
            Quaternion deltaRotation = Quaternion.AngleAxis(rotationAmount * Time.fixedDeltaTime * rotationSpeed, transform.up);
            rb.MoveRotation(deltaRotation * rb.rotation);
            // jump
            if (IsGrounded && jump)
            {
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Acceleration);
            }
        }
        private void CheckGrounded()
        {
            IsGrounded = false;
            float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2, capsuleCollider.height);
            Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center + capsuleHeight * 0.5f * Vector3.down);
            Ray ray = new(capsuleBottom, -transform.up);
            if (Physics.Raycast(ray, out groundHit, capsuleCollider.radius * 5f))
            {
                float normalAngle = Vector3.Angle(groundHit.normal, transform.up);
                float maxDist = capsuleCollider.radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - capsuleCollider.radius + .02f;
                if (groundHit.distance <= maxDist)
                    IsGrounded = true;
            }
        }
        private Vector3 GetProjectedForward()
        {
            return Vector3.ProjectOnPlane(transform.forward, groundHit.normal);
        }
    }
}
