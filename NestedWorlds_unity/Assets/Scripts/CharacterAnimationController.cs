using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] SphereCharacterController characterController;
        [SerializeField] Animator animator;
        [Header("SFX")]
        [SerializeField] AudioResource jumpSFX;
        // Update is called once per frame
        void Update()
        {
            float speed = characterController.ForwardSpeed;
            animator.SetFloat("Speed", speed);

            if (characterController.JumpThisFrame)
            {
                AudioManager.PlaySFX(jumpSFX, transform.position);
            }
        }
    }
}
