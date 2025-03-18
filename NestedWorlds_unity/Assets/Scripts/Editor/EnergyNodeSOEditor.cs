using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using IsmaLB.Levels;


namespace IsmaLB.Editors
{
    [CustomEditor(typeof(EnergyNodeSO))]
    public class EnergyNodeSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EnergyNodeSO energyNode = (EnergyNodeSO)target;

            // show state
            EditorGUI.BeginChangeCheck();
            LevelState newState = (LevelState)EditorGUILayout.EnumPopup("State", energyNode.State);
            if (EditorGUI.EndChangeCheck())
            {
                energyNode.SetState(newState);
            }
            // show level
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Level", energyNode.level, typeof(LevelEventSO), false);
            EditorGUI.EndDisabledGroup();
        }
    }
}

