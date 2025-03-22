using UnityEngine;
using UnityEditor;

namespace IsmaLB.Editors
{
    [CustomEditor(typeof(AttractorAligned), true)]
    public class AttractorAlignedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            AttractorAligned body = (AttractorAligned)target;
            if (body.attractor && GUILayout.Button("Align with attractor"))
            {
                body.transform.rotation = body.attractor.Align(body.transform);
            }
        }
    }
}