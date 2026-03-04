using UnityEditor;

namespace KekwDetlef.SceneManagement
{
    [CustomEditor(typeof(SimpleSceneLoader))]
    public class SimpleSceneChangerInspectorOverride : UnityEditor.Editor
    {
        SerializedProperty sceneTypeProp, uiSceneInfoProp, worldSceneInfoProp, loadModeProp;

        void OnEnable()
        {
            SimpleSceneLoader me = (SimpleSceneLoader)target;

            sceneTypeProp = serializedObject.FindProperty(me.NO_sceneType);
            loadModeProp = serializedObject.FindProperty(me.NO_loadMode);
            uiSceneInfoProp = serializedObject.FindProperty(me.NO_uiSceneInfo);
            worldSceneInfoProp = serializedObject.FindProperty(me.NO_worldSceneInfo);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(sceneTypeProp);
            EditorGUILayout.PropertyField(loadModeProp);

            if ((SceneType)sceneTypeProp.enumValueIndex == SceneType.UI)
            {
                EditorGUILayout.PropertyField(uiSceneInfoProp);
            }
            else
            {
                EditorGUILayout.PropertyField(worldSceneInfoProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}


