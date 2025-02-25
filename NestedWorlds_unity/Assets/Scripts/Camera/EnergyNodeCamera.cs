using Unity.Cinemachine;
using UnityEngine;

namespace IsmaLB.Cameras
{
    public class EnergyNodeCamera : MonoBehaviour
    {
        [SerializeField, TagSelector] string playerTag;
        [SerializeField] CinemachineCamera nodeCamera;
        Transform player = null;

        void Start()
        {
            GameObject temp = GameObject.FindWithTag(playerTag);
            if (temp)
            {
                player = temp.transform;
            }
            DisableCamera();
        }
        public void EnableCamera()
        {
            AimToPlayer();
            nodeCamera.gameObject.SetActive(true);
        }
        void DisableCamera()
        {
            nodeCamera.gameObject.SetActive(false);
        }

        /// <summary>
        /// Align node cinemachine camera with the player view. Reduces the rotation required in the blend. 
        /// Uses a transform rig where main object (attached with this component) acts as a pointer and the camera
        /// is a children 
        /// </summary>
        void AimToPlayer()
        {
            if (player == null) return;
            // point camera rig towards the player, use the energy node up as reference
            transform.LookAt(player, transform.parent.up);
        }
    }
}
