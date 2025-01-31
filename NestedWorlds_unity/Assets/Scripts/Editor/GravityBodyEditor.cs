using UnityEngine;
using UnityEditor;

namespace IsmaLB.Editors
{
    [CustomEditor(typeof(GravityBody))]
    public class GravityBodyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GravityBody body = (GravityBody)target;
            if (body.attractor && GUILayout.Button("Align with attractor"))
            {
                Rigidbody rb = body.GetComponent<Rigidbody>();
                rb.transform.rotation = body.attractor.Align(rb, 1);
            }
        }
    }
}