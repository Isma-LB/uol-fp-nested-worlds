using System.Collections;
using UnityEngine;

namespace IsmaLB.UI
{
    public class InGameUICanvas : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        Transform target;
        IEnumerator Start()
        {
            yield return null;
            Camera mainCamera = Camera.main;
            if (mainCamera)
            {
                target = mainCamera.transform;
                canvas.worldCamera = mainCamera;
            }
            else
            {
                Debug.LogWarning("InGameCanvas Could not find the main camera");
            }
        }
        void LateUpdate()
        {
            if (target)
            {
                transform.LookAt(target, transform.parent.up);
            }
        }
    }
}
