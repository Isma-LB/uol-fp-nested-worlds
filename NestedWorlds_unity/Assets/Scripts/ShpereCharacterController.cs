using UnityEngine;

namespace IsmaLB
{

    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class SphereCharacterController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 300f;
        [SerializeField] float jumpSpeed = 12f;

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
            Vector3 deltaPosition = GetProjectedForward() * forwardMovement * Time.fixedDeltaTime * moveSpeed;
            if (IsGrounded && jump)
            {
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Acceleration);
            }
            rb.MovePosition(deltaPosition + rb.position);
            Quaternion deltaRotation = Quaternion.AngleAxis(rotationAmount * Time.fixedDeltaTime * rotationSpeed, transform.up);
            rb.MoveRotation(deltaRotation * rb.rotation);
        }
        private void CheckGrounded()
        {
            IsGrounded = false;
            float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2, capsuleCollider.height);

            Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight * 0.5f);
            Ray ray = new Ray(capsuleBottom, -transform.up);
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (Physics.Raycast(ray, out groundHit, capsuleCollider.radius * 5f))
            {
                float normalAngle = Vector3.Angle(groundHit.normal, transform.up);
                Debug.Log(normalAngle);
            }
        }
        private Vector3 GetProjectedForward()
        {
            return Vector3.ProjectOnPlane(transform.forward, groundHit.normal);
        }
    }
}
