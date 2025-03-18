using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using IsmaLB.Levels;

namespace IsmaLB.Editors
{
    [CustomEditor(typeof(PuzzleProgressionSO))]
    public class PuzzleProgressionSOEditor : Editor
    {
        bool showOrderedNodes = true;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();

            PuzzleProgressionSO puzzleProgression = (PuzzleProgressionSO)target;

            // show ordered list
            showOrderedNodes = EditorGUILayout.BeginFoldoutHeaderGroup(showOrderedNodes, "Ordered Nodes");
            EditorGUI.BeginDisabledGroup(true);
            if (showOrderedNodes)
            {
                for (int i = 0; i < puzzleProgression.orderedNodes.Count; i++)
                {
                    EnergyNodeSO so = puzzleProgression.orderedNodes[i];
                    string label = "Element " + i;
                    EditorGUILayout.ObjectField(label, so, typeof(EnergyNodeSO), false);
                }
            }

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
