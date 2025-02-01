using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] bool isOpenByDefault = false;
        [SerializeField] float fadeSpeed = 2;
        [SerializeField] AnimationCurve fadeEase;
        CanvasGroup canvasGroup;
        UnityAction closeCallback;
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            SetState(isOpenByDefault);
        }

        public void Close()
        {
            StartCoroutine(FadeOut());
            closeCallback?.Invoke();
            closeCallback = null;
        }
        public void Open() => Open(null);
        public void Open(UnityAction onCloseCallback)
        {
            closeCallback = onCloseCallback;
            StartCoroutine(FadeIn());
        }
        void SetState(bool isOpen)
        {
            canvasGroup.alpha = isOpen ? 1 : 0;
            canvasGroup.blocksRaycasts = isOpen;
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
                amount += Time.deltaTime * fadeSpeed;

                canvasGroup.alpha = Mathf.Lerp(from, to, fadeEase.Evaluate(amount));
            }
            canvasGroup.alpha = to;
        }
    }
}
