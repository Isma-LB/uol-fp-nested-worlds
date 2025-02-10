using IsmaLB.UI;
using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    public class SettingsScreenController : MonoBehaviour
    {
        [SerializeField] UIPanel screenUIPanel;

        public void Open(UnityAction onCloseCallback)
        {
            screenUIPanel.Open(onCloseCallback);
        }
    }
}
