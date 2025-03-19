using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    public class EventSFXController : MonoBehaviour
    {
        [Serializable]
        public class EventSFX
        {
            [SerializeField] AudioResource sfx;
            [SerializeField] EventSO eventSO;
            public void Subscribe()
            {
                eventSO.OnEvent += OnEvent;
            }
            public void Unsubscribe()
            {
                eventSO.OnEvent -= OnEvent;
            }
            private void OnEvent()
            {
                AudioManager.PlaySFX(sfx, Vector3.zero);
            }
        }
        [SerializeField] EventSFX test;
        public List<EventSFX> soundEffects = new();
        void OnEnable()
        {
            foreach (EventSFX eventSFX in soundEffects)
            {
                eventSFX.Subscribe();
            }
        }
        void OnDisable()
        {
            foreach (EventSFX eventSFX in soundEffects)
            {
                eventSFX.Unsubscribe();
            }
        }
    }
}
