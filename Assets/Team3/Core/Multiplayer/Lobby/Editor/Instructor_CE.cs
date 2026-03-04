using UnityEditor;

namespace Team3.Multiplayer.Lobby
{
    [CustomEditor(typeof(Instructor))]
    public class Instructor_CE : Editor
    {
        private SerializedProperty lobbyInstructionProp, lobbyCodeInputProp, onInstructionFinishedProp;
        private Instructor me;

        public void Awake()
        {
            me = (Instructor)target;

            lobbyInstructionProp = serializedObject.FindProperty(me.NO_LobbyInstruction);
            lobbyCodeInputProp = serializedObject.FindProperty(me.NO_LobbyCodeInput);
            onInstructionFinishedProp = serializedObject.FindProperty(me.NO_OnInstructionFinished);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(lobbyInstructionProp);

            if ((LobbyInstructionType)lobbyInstructionProp.enumValueIndex == LobbyInstructionType.Join)
            {
                EditorGUILayout.PropertyField(lobbyCodeInputProp);
            }

            EditorGUILayout.PropertyField(onInstructionFinishedProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
