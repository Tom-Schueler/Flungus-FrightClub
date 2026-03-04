using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Team3.Multiplayer.Lobby
{
    public class Instructor : MonoBehaviour
    {
        public UnityEvent OnInstructionFinished;
        
        [SerializeField] private LobbyInstructionType lobbyInstruction;
        [SerializeField] private TMP_InputField lobbyCodeInput;

        public string NO_OnInstructionFinished => nameof(OnInstructionFinished);
        public string NO_LobbyInstruction => nameof(lobbyInstruction);
        public string NO_LobbyCodeInput => nameof(lobbyCodeInput);

        public void SetInstructions()
        {
            string code = lobbyInstruction == LobbyInstructionType.Join ? lobbyCodeInput.text : null;
            LobbyInstructions.SetInstructions(lobbyInstruction, code);

            OnInstructionFinished?.Invoke();
        }
    }
}
