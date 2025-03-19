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
        [SerializeField] AudioSource stepsAudioSource;
        // Update is called once per frame
        void Update()
        {
            float speed = characterController.ForwardSpeed;
            animator.SetFloat("Speed", speed);

            if (speed != 0 && stepsAudioSource.isPlaying == false)
            {
                stepsAudioSource.Play();
            }
            else if (speed == 0 && stepsAudioSource.isPlaying)
            {
                stepsAudioSource.Stop();
            }
            if (characterController.JumpThisFrame)
            {
                AudioManager.PlaySFX(jumpSFX, transform.position);
            }
        }
    }
}
