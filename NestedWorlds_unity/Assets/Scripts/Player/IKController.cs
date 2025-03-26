using UnityEngine;

namespace IsmaLB.Player
{
    [RequireComponent(typeof(Animator))]
    public class IKController : MonoBehaviour
    {
        [SerializeField] LayerMask groundLayer = 0;
        [SerializeField, Range(0, 2)] float checkDistance = 1.5f;
        [SerializeField, Range(0, 1)] float footOffset = 0.07f;
        [SerializeField, Range(0, 1)] float steepHight = 0.2f;
        [SerializeField, Range(0, 1)] float feetHight = 0.2f;
        public bool enableIK = true;
        private Animator anim;
        private Vector3 targetPositionL, targetPositionR;
        private Quaternion targetRotationL, targetRotationR;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        private void OnAnimatorIK(int layerIndex)
        {
            if (!enableIK) return;
            //Left foot
            targetPositionL = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
            targetRotationL = anim.GetIKRotation(AvatarIKGoal.LeftFoot);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, getWeight(targetPositionL));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            SetTargetFromRaycast(ref targetPositionL, ref targetRotationL);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, targetPositionL);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, targetRotationL);

            //Right Foot
            targetPositionR = anim.GetIKPosition(AvatarIKGoal.RightFoot);
            targetRotationR = anim.GetIKRotation(AvatarIKGoal.RightFoot);

            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, getWeight(targetPositionR));
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
            SetTargetFromRaycast(ref targetPositionR, ref targetRotationR);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, targetPositionR);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, targetRotationR);
        }
        float getWeight(Vector3 goalPos)
        {
            return Mathf.InverseLerp(steepHight, feetHight, transform.InverseTransformPoint(goalPos).y);
        }
        void SetTargetFromRaycast(ref Vector3 goalPos, ref Quaternion goalRotation)
        {
            RaycastHit hit;
            Ray ray = new Ray(goalPos + transform.up, transform.up * -1f);
            Debug.DrawRay(ray.origin, ray.direction * checkDistance, Color.red);
            if (Physics.Raycast(ray, out hit, checkDistance, groundLayer))
            {
                goalPos = hit.point + transform.up * footOffset;
                goalRotation = Quaternion.FromToRotation(transform.up, hit.normal) * goalRotation;
            }
        }
    }
}