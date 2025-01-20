using UnityEngine;

namespace IsmaLB
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] SphereCharacterController characterController;
        [SerializeField] Animator animator;
        // Update is called once per frame
        void Update()
        {
            float speed = characterController.ForwardSpeed;
            animator.SetFloat("Speed", speed);
        }
    }
}
