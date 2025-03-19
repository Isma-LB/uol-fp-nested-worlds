using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using IsmaLB.Audio;

namespace IsmaLB.Editors
{
    [CustomEditor(typeof(EventSFXController))]
    public class EventSFXControllerEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement { };
            var list = new MultiColumnListView
            {
                bindingPath = "soundEffects",
                showAddRemoveFooter = true,
                showBoundCollectionSize = false,
                reorderable = true,
                showFoldoutHeader = true,
                headerTitle = "Events mapped to SFX",
            };
            list.columns.Add(new Column { bindingPath = "eventSO", title = "On Event", stretchable = true, optional = false });
            list.columns.Add(new Column { bindingPath = "sfx", title = "Play", stretchable = true, optional = false });
            list.Bind(serializedObject);
            container.Add(new Label("Plays sound effects in response to events"));
            container.Add(list);
            return container;
        }
    }
}