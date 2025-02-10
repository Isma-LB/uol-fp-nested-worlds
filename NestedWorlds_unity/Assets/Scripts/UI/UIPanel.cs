using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IsmaLB.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] Selectable autoSelectedObject;
        [SerializeField] bool isOpenByDefault = false;
        [SerializeField] FadeSettingsSO fadeSettings;
        CanvasGroup canvasGroup;
        UnityAction closeCallback;
        bool isPanelOpen;
        public bool IsOpen => isPanelOpen;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            SetState(isOpenByDefault);
        }

        public void Close()
        {
            if (isPanelOpen == false) return;
            isPanelOpen = false;
            StartCoroutine(FadeOut());
            closeCallback?.Invoke();
            closeCallback = null;
        }
        public void Open() => Open(null);
        public void Open(UnityAction onCloseCallback)
        {
            if (isPanelOpen == true) return;
            isPanelOpen = true;

            closeCallback = onCloseCallback;
            StartCoroutine(FadeIn());
            if (autoSelectedObject != null) autoSelectedObject.Select();
        }
        void SetState(bool isOpen)
        {
            canvasGroup.alpha = isOpen ? 1 : 0;
            canvasGroup.blocksRaycasts = isOpen;
            canvasGroup.interactable = isOpen;
            isPanelOpen = isOpen;
        }
        IEnumerator FadeIn()
        {
            yield return FadeAlpha(0, 1);
            SetState(true);
        }
        IEnumerator FadeOut()
        {
            yield return FadeAlpha(1, 0);
            SetState(false);
        }
        IEnumerator FadeAlpha(float from, float to)
        {
            float amount = 0;
            while (amount < 1)
            {
                yield return null;
                amount += Time.unscaledDeltaTime * fadeSettings.Speed;

                canvasGroup.alpha = Mathf.Lerp(from, to, fadeSettings.EvaluateEase(amount));
            }
            canvasGroup.alpha = to;
        }
    }
}
