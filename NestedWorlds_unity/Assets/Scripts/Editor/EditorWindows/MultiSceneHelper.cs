using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace IsmaLB.Editors
{
    public class MultiSceneHelper : EditorWindow
    {
        private Object baseScene = null;
        private Object additiveScene = null;
        private bool autoLoad = true;

        [MenuItem("NestedWorlds/MultiSceneHelper")]
        private static void ShowWindow()
        {
            var window = GetWindow<MultiSceneHelper>();
            window.titleContent = new GUIContent("MultiSceneHelper");
            window.Show();
        }
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Base scene", EditorStyles.label);
            baseScene = EditorGUILayout.ObjectField(baseScene, typeof(SceneAsset), false);
            GUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            GUILayout.Label("PlanetScene", EditorStyles.label);
            additiveScene = EditorGUILayout.ObjectField(additiveScene, typeof(SceneAsset), false);
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck() && autoLoad && baseScene != null && additiveScene != null)
            {
                LoadMultiSceneSetup();
            }
            autoLoad = EditorGUILayout.Toggle("Auto Load on scene selection", autoLoad);
            if (GUILayout.Button("Load Scenes"))
            {
                LoadMultiSceneSetup();
            }
        }
        void LoadMultiSceneSetup()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(baseScene));
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(additiveScene), OpenSceneMode.Additive);
            }
        }
    }
}