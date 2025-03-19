using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    public class PlaySFX : MonoBehaviour
    {
        [SerializeField] AudioResource sfx;
        [SerializeField] bool playOnAwake = false;
        void Awake()
        {
            if (playOnAwake) TriggerSFX();
        }
        public void TriggerSFX()
        {
            AudioManager.PlaySFX(sfx, transform.position);
        }
    }
}
