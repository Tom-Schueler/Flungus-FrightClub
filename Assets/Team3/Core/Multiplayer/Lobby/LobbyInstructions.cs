using UnityEngine;

namespace Team3.Multiplayer.Lobby
{
    public static class LobbyInstructions
    {
        public static bool IsSet { private set; get; }
        public static LobbyInstructionType LobbyInstruction { private set; get; }
        public static string LobbyCode { private set; get; }

        public static void SetInstructions(LobbyInstructionType lobbyInstruction, string lobbyCode = null)
        {
            if (IsSet)
            {
                Debug.Log($"LobbyInstructions cant be overwritten. They have to be Cleard first.");
                return;
            }
            
            LobbyInstruction = lobbyInstruction;
            LobbyCode = lobbyCode;

            IsSet = true;
        }

        public static void ClearInstructions()
        {
            IsSet = false;

            LobbyInstruction = LobbyInstructionType.None;
            LobbyCode = string.Empty;
        }
    }
}
