using UnityEngine;

namespace IsmaLB
{
    public abstract class PlayerRangeItem : MonoBehaviour
    {
        [SerializeField, TagSelector] string playerTag = "Player";
        [SerializeField] GameObject rangeIndicator;
        public bool IsPlayerInRange { get; private set; } = false;
        protected virtual void Awake()
        {
            HandleRangeIndicatorGraphic(false);
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                IsPlayerInRange = true;
                HandleRangeIndicatorGraphic(true);
            }
        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                IsPlayerInRange = false;
                HandleRangeIndicatorGraphic(false);
            }
        }
        protected virtual void HandleRangeIndicatorGraphic(bool on)
        {
            if (rangeIndicator)
                rangeIndicator.SetActive(on);
        }
    }
}