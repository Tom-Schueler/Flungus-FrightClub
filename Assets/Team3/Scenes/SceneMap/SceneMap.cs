// This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 

using System.Collections.Generic; 
using UnityEngine.AddressableAssets;

namespace KekwDetlef.SceneManagement
{
    public static class SceneMap
    {
        public enum UIScene
        {
           NONE = 0
            ,MAINMENU = 1
            ,SETTINGS = 2
            ,BACKGROUND = 3
            ,PERKSMENU = 4
            ,FIRSTSCENE = 8
            ,HUD = 5
            ,AUDIOSETTINGS = 11
            ,KEYBINDINGS = 12
            ,GENERALSETTINGS = 13
            ,INVENTORYMENU = 9
            ,ENDSCREEN = 14
            ,CREDITS = 15
            ,MULTIPLAYERLOBBY = 7
            ,SCOREBOARD = 16
            ,CREDENTIALS = 17
            ,WEAPONSELECTION = 10
        }

        public enum WorldScene
        {
           NONE = 0
            ,WORLD = 1
            ,MATCH = 2
        }

        public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
        {
            { UIScene.NONE, null }
            ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
            ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
            ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
            ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
            ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
            ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
            ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
            ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
            ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
            ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
            ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
            ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
            ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
            ,{ UIScene.SCOREBOARD, new("fb27e9fe6c628bf4ab11e971f06703ac") }
            ,{ UIScene.CREDENTIALS, new("c76ad384616ede544b5b68e9d0f0bcfe") }
            ,{ UIScene.WEAPONSELECTION, new("fd0c94e081b1e724f9b9237eafa1a256") }
        };

       public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
        {
            { WorldScene.NONE, null }
            ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
            ,{ WorldScene.MATCH, new("ab4c1e0e5146f214ea14c6b5699c5717") }
        };
    }
}
// ----- Previous SceneMap [Deprecated scince 2025-08-05 13:23:53] -----
// // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// 
// using System.Collections.Generic; 
// using UnityEngine.AddressableAssets;
// 
// namespace KekwDetlef.SceneManagement
// {
//     public static class SceneMap
//     {
//         public enum UIScene
//         {
//            NONE = 0
//             ,MAINMENU = 1
//             ,SETTINGS = 2
//             ,BACKGROUND = 3
//             ,PERKSMENU = 4
//             ,FIRSTSCENE = 8
//             ,HUD = 5
//             ,AUDIOSETTINGS = 11
//             ,KEYBINDINGS = 12
//             ,GENERALSETTINGS = 13
//             ,INVENTORYMENU = 9
//             ,ENDSCREEN = 14
//             ,CREDITS = 15
//             ,MULTIPLAYERLOBBY = 7
//             ,SCOREBOARD = 16
//             ,CREDENTIALS = 17
//         }
// 
//         public enum WorldScene
//         {
//            NONE = 0
//             ,WORLD = 1
//             ,MATCH = 2
//         }
// 
//         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
//         {
//             { UIScene.NONE, null }
//             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
//             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
//             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
//             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
//             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
//             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
//             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
//             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
//             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
//             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
//             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
//             ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
//             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
//             ,{ UIScene.SCOREBOARD, new("fb27e9fe6c628bf4ab11e971f06703ac") }
//             ,{ UIScene.CREDENTIALS, new("c76ad384616ede544b5b68e9d0f0bcfe") }
//         };
// 
//        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
//         {
//             { WorldScene.NONE, null }
//             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
//             ,{ WorldScene.MATCH, new("ab4c1e0e5146f214ea14c6b5699c5717") }
//         };
//     }
// }
// // ----- Previous SceneMap [Deprecated scince 2025-08-01 13:27:20] -----
// // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // 
// // using System.Collections.Generic; 
// // using UnityEngine.AddressableAssets;
// // 
// // namespace KekwDetlef.SceneManagement
// // {
// //     public static class SceneMap
// //     {
// //         public enum UIScene
// //         {
// //            NONE = 0
// //             ,MAINMENU = 1
// //             ,SETTINGS = 2
// //             ,BACKGROUND = 3
// //             ,PERKSMENU = 4
// //             ,FIRSTSCENE = 8
// //             ,HUD = 5
// //             ,AUDIOSETTINGS = 11
// //             ,KEYBINDINGS = 12
// //             ,GENERALSETTINGS = 13
// //             ,INVENTORYMENU = 9
// //             ,ENDSCREEN = 14
// //             ,CREDITS = 15
// //             ,MULTIPLAYERLOBBY = 7
// //             ,SCOREBOARD = 16
// //         }
// // 
// //         public enum WorldScene
// //         {
// //            NONE = 0
// //             ,WORLD = 1
// //             ,MATCH = 2
// //         }
// // 
// //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// //         {
// //             { UIScene.NONE, null }
// //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// //             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// //             ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
// //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// //             ,{ UIScene.SCOREBOARD, new("fb27e9fe6c628bf4ab11e971f06703ac") }
// //         };
// // 
// //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// //         {
// //             { WorldScene.NONE, null }
// //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// //             ,{ WorldScene.MATCH, new("ab4c1e0e5146f214ea14c6b5699c5717") }
// //         };
// //     }
// // }
// // // ----- Previous SceneMap [Deprecated scince 2025-07-25 14:22:18] -----
// // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // 
// // // using System.Collections.Generic; 
// // // using UnityEngine.AddressableAssets;
// // // 
// // // namespace KekwDetlef.SceneManagement
// // // {
// // //     public static class SceneMap
// // //     {
// // //         public enum UIScene
// // //         {
// // //            NONE = 0
// // //             ,MAINMENU = 1
// // //             ,SETTINGS = 2
// // //             ,BACKGROUND = 3
// // //             ,PERKSMENU = 4
// // //             ,FIRSTSCENE = 8
// // //             ,HUD = 5
// // //             ,AUDIOSETTINGS = 11
// // //             ,KEYBINDINGS = 12
// // //             ,GENERALSETTINGS = 13
// // //             ,INVENTORYMENU = 9
// // //             ,ENDSCREEN = 14
// // //             ,CREDITS = 15
// // //             ,MULTIPLAYERLOBBY = 7
// // //         }
// // // 
// // //         public enum WorldScene
// // //         {
// // //            NONE = 0
// // //             ,WORLD = 1
// // //             ,MATCH = 2
// // //         }
// // // 
// // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // //         {
// // //             { UIScene.NONE, null }
// // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // //             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// // //             ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
// // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // //         };
// // // 
// // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // //         {
// // //             { WorldScene.NONE, null }
// // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // //             ,{ WorldScene.MATCH, new("ab4c1e0e5146f214ea14c6b5699c5717") }
// // //         };
// // //     }
// // // }
// // // // ----- Previous SceneMap [Deprecated scince 2025-07-25 13:26:34] -----
// // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // 
// // // // using System.Collections.Generic; 
// // // // using UnityEngine.AddressableAssets;
// // // // 
// // // // namespace KekwDetlef.SceneManagement
// // // // {
// // // //     public static class SceneMap
// // // //     {
// // // //         public enum UIScene
// // // //         {
// // // //            NONE = 0
// // // //             ,MAINMENU = 1
// // // //             ,SETTINGS = 2
// // // //             ,BACKGROUND = 3
// // // //             ,PERKSMENU = 4
// // // //             ,FIRSTSCENE = 8
// // // //             ,HUD = 5
// // // //             ,AUDIOSETTINGS = 11
// // // //             ,KEYBINDINGS = 12
// // // //             ,GENERALSETTINGS = 13
// // // //             ,INVENTORYMENU = 9
// // // //             ,ENDSCREEN = 14
// // // //             ,CREDITS = 15
// // // //             ,MULTIPLAYERLOBBY = 7
// // // //         }
// // // // 
// // // //         public enum WorldScene
// // // //         {
// // // //            NONE = 0
// // // //             ,WORLD = 1
// // // //         }
// // // // 
// // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // //         {
// // // //             { UIScene.NONE, null }
// // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // //             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // // //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// // // //             ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
// // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // //         };
// // // // 
// // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // //         {
// // // //             { WorldScene.NONE, null }
// // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // //         };
// // // //     }
// // // // }
// // // // // ----- Previous SceneMap [Deprecated scince 2025-07-22 14:04:29] -----
// // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // 
// // // // // using System.Collections.Generic; 
// // // // // using UnityEngine.AddressableAssets;
// // // // // 
// // // // // namespace KekwDetlef.SceneManagement
// // // // // {
// // // // //     public static class SceneMap
// // // // //     {
// // // // //         public enum UIScene
// // // // //         {
// // // // //            NONE = 0
// // // // //             ,MAINMENU = 1
// // // // //             ,SETTINGS = 2
// // // // //             ,BACKGROUND = 3
// // // // //             ,PERKSMENU = 4
// // // // //             ,PRELOBBY = 6
// // // // //             ,MULTIPLAYERLOBBY = 7
// // // // //             ,FIRSTSCENE = 8
// // // // //             ,HUD = 5
// // // // //             ,AUDIOSETTINGS = 11
// // // // //             ,KEYBINDINGS = 12
// // // // //             ,GENERALSETTINGS = 13
// // // // //             ,INVENTORYMENU = 9
// // // // //             ,ENDSCREEN = 14
// // // // //             ,CREDITS = 15
// // // // //         }
// // // // // 
// // // // //         public enum WorldScene
// // // // //         {
// // // // //            NONE = 0
// // // // //             ,WORLD = 1
// // // // //         }
// // // // // 
// // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // //         {
// // // // //             { UIScene.NONE, null }
// // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // // //             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // // // //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// // // // //             ,{ UIScene.CREDITS, new("b6c4be9ee429f844d98d6290a1ad198e") }
// // // // //         };
// // // // // 
// // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // //         {
// // // // //             { WorldScene.NONE, null }
// // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // //         };
// // // // //     }
// // // // // }
// // // // // // ----- Previous SceneMap [Deprecated scince 2025-07-22 13:49:02] -----
// // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // 
// // // // // // using System.Collections.Generic; 
// // // // // // using UnityEngine.AddressableAssets;
// // // // // // 
// // // // // // namespace KekwDetlef.SceneManagement
// // // // // // {
// // // // // //     public static class SceneMap
// // // // // //     {
// // // // // //         public enum UIScene
// // // // // //         {
// // // // // //            NONE = 0
// // // // // //             ,MAINMENU = 1
// // // // // //             ,SETTINGS = 2
// // // // // //             ,BACKGROUND = 3
// // // // // //             ,PERKSMENU = 4
// // // // // //             ,PRELOBBY = 6
// // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // //             ,FIRSTSCENE = 8
// // // // // //             ,HUD = 5
// // // // // //             ,AUDIOSETTINGS = 11
// // // // // //             ,KEYBINDINGS = 12
// // // // // //             ,GENERALSETTINGS = 13
// // // // // //             ,INVENTORYMENU = 9
// // // // // //             ,ENDSCREEN = 14
// // // // // //         }
// // // // // // 
// // // // // //         public enum WorldScene
// // // // // //         {
// // // // // //            NONE = 0
// // // // // //             ,WORLD = 1
// // // // // //         }
// // // // // // 
// // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // //         {
// // // // // //             { UIScene.NONE, null }
// // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // // // //             ,{ UIScene.KEYBINDINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // // // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // // // // //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// // // // // //         };
// // // // // // 
// // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // //         {
// // // // // //             { WorldScene.NONE, null }
// // // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // // //         };
// // // // // //     }
// // // // // // }
// // // // // // // ----- Previous SceneMap [Deprecated scince 2025-07-11 13:32:40] -----
// // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // 
// // // // // // // using System.Collections.Generic; 
// // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // 
// // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // {
// // // // // // //     public static class SceneMap
// // // // // // //     {
// // // // // // //         public enum UIScene
// // // // // // //         {
// // // // // // //            NONE = 0
// // // // // // //             ,MAINMENU = 1
// // // // // // //             ,SETTINGS = 2
// // // // // // //             ,BACKGROUND = 3
// // // // // // //             ,PERKSMENU = 4
// // // // // // //             ,PRELOBBY = 6
// // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // //             ,FIRSTSCENE = 8
// // // // // // //             ,HUD = 5
// // // // // // //             ,AUDIOSETTINGS = 11
// // // // // // //             ,CONTROLLSETTINGS = 12
// // // // // // //             ,GENERALSETTINGS = 13
// // // // // // //             ,INVENTORYMENU = 9
// // // // // // //             ,ENDSCREEN = 14
// // // // // // //         }
// // // // // // // 
// // // // // // //         public enum WorldScene
// // // // // // //         {
// // // // // // //            NONE = 0
// // // // // // //             ,WORLD = 1
// // // // // // //         }
// // // // // // // 
// // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // //         {
// // // // // // //             { UIScene.NONE, null }
// // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // // // // //             ,{ UIScene.CONTROLLSETTINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // // // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // // // // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // // // // // //             ,{ UIScene.ENDSCREEN, new("b3597faa294592c4fa33b8ad0044a228") }
// // // // // // //         };
// // // // // // // 
// // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // //         {
// // // // // // //             { WorldScene.NONE, null }
// // // // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // // // //         };
// // // // // // //     }
// // // // // // // }
// // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-07-09 09:35:56] -----
// // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // 
// // // // // // // // using System.Collections.Generic; 
// // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // 
// // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // {
// // // // // // // //     public static class SceneMap
// // // // // // // //     {
// // // // // // // //         public enum UIScene
// // // // // // // //         {
// // // // // // // //            NONE = 0
// // // // // // // //             ,MAINMENU = 1
// // // // // // // //             ,SETTINGS = 2
// // // // // // // //             ,BACKGROUND = 3
// // // // // // // //             ,PERKSMENU = 4
// // // // // // // //             ,PRELOBBY = 6
// // // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // // //             ,FIRSTSCENE = 8
// // // // // // // //             ,HUD = 5
// // // // // // // //             ,AUDIOSETTINGS = 11
// // // // // // // //             ,CONTROLLSETTINGS = 12
// // // // // // // //             ,GENERALSETTINGS = 13
// // // // // // // //             ,INVENTORYMENU = 9
// // // // // // // //         }
// // // // // // // // 
// // // // // // // //         public enum WorldScene
// // // // // // // //         {
// // // // // // // //            NONE = 0
// // // // // // // //             ,WORLD = 1
// // // // // // // //         }
// // // // // // // // 
// // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // //         {
// // // // // // // //             { UIScene.NONE, null }
// // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // // // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // // // // // //             ,{ UIScene.CONTROLLSETTINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // // // // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // // // // // //             ,{ UIScene.INVENTORYMENU, new("b6c4047819f4ad74180ed6587d72d7ce") }
// // // // // // // //         };
// // // // // // // // 
// // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // //         {
// // // // // // // //             { WorldScene.NONE, null }
// // // // // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // // // // //         };
// // // // // // // //     }
// // // // // // // // }
// // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-07-03 13:54:15] -----
// // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // 
// // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // 
// // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // {
// // // // // // // // //     public static class SceneMap
// // // // // // // // //     {
// // // // // // // // //         public enum UIScene
// // // // // // // // //         {
// // // // // // // // //            NONE = 0
// // // // // // // // //             ,MAINMENU = 1
// // // // // // // // //             ,SETTINGS = 2
// // // // // // // // //             ,BACKGROUND = 3
// // // // // // // // //             ,PERKSMENU = 4
// // // // // // // // //             ,PRELOBBY = 6
// // // // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // // // //             ,FIRSTSCENE = 8
// // // // // // // // //             ,HUD = 5
// // // // // // // // //             ,AUDIOSETTINGS = 11
// // // // // // // // //             ,CONTROLLSETTINGS = 12
// // // // // // // // //             ,GENERALSETTINGS = 13
// // // // // // // // //         }
// // // // // // // // // 
// // // // // // // // //         public enum WorldScene
// // // // // // // // //         {
// // // // // // // // //            NONE = 0
// // // // // // // // //             ,WORLD = 1
// // // // // // // // //         }
// // // // // // // // // 
// // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // //         {
// // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // // // // // //             ,{ UIScene.AUDIOSETTINGS, new("8f8d252e055d9bc48b047b1deb3daabb") }
// // // // // // // // //             ,{ UIScene.CONTROLLSETTINGS, new("0ca3ff7623b12bb46a3035b3ebf618f6") }
// // // // // // // // //             ,{ UIScene.GENERALSETTINGS, new("2f181d6ef8b9225488f21be4d739982f") }
// // // // // // // // //         };
// // // // // // // // // 
// // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // //         {
// // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // // // // // //         };
// // // // // // // // //     }
// // // // // // // // // }
// // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-07-01 16:02:29] -----
// // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // 
// // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // 
// // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // {
// // // // // // // // // //     public static class SceneMap
// // // // // // // // // //     {
// // // // // // // // // //         public enum UIScene
// // // // // // // // // //         {
// // // // // // // // // //            NONE = 0
// // // // // // // // // //             ,MAINMENU = 1
// // // // // // // // // //             ,SETTINGS = 2
// // // // // // // // // //             ,BACKGROUND = 3
// // // // // // // // // //             ,PERKSMENU = 4
// // // // // // // // // //             ,PRELOBBY = 6
// // // // // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // // // // //             ,FIRSTSCENE = 8
// // // // // // // // // //             ,HUD = 5
// // // // // // // // // //         }
// // // // // // // // // // 
// // // // // // // // // //         public enum WorldScene
// // // // // // // // // //         {
// // // // // // // // // //            NONE = 0
// // // // // // // // // //             ,WORLD = 1
// // // // // // // // // //         }
// // // // // // // // // // 
// // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // //         {
// // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // // // // // //             ,{ UIScene.HUD, new("66c4c75a851ae9541be45b4fbd1a260a") }
// // // // // // // // // //         };
// // // // // // // // // // 
// // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // //         {
// // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // //             ,{ WorldScene.WORLD, new("25f3bc1c9fa9f4cd6b0a6960838ec061") }
// // // // // // // // // //         };
// // // // // // // // // //     }
// // // // // // // // // // }
// // // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-06-30 10:55:20] -----
// // // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // // 
// // // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // // 
// // // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // // {
// // // // // // // // // // //     public static class SceneMap
// // // // // // // // // // //     {
// // // // // // // // // // //         public enum UIScene
// // // // // // // // // // //         {
// // // // // // // // // // //            NONE = 0
// // // // // // // // // // //             ,MAINMENU = 1
// // // // // // // // // // //             ,SETTINGS = 2
// // // // // // // // // // //             ,BACKGROUND = 3
// // // // // // // // // // //             ,PERKSMENU = 4
// // // // // // // // // // //             ,PRELOBBY = 6
// // // // // // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // // // // // //             ,FIRSTSCENE = 8
// // // // // // // // // // //         }
// // // // // // // // // // // 
// // // // // // // // // // //         public enum WorldScene
// // // // // // // // // // //         {
// // // // // // // // // // //            NONE = 0
// // // // // // // // // // //         }
// // // // // // // // // // // 
// // // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // // //         {
// // // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // // // // // //             ,{ UIScene.FIRSTSCENE, new("00670399f17b40f45b0d934f01d3eab3") }
// // // // // // // // // // //         };
// // // // // // // // // // // 
// // // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // // //         {
// // // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // // //         };
// // // // // // // // // // //     }
// // // // // // // // // // // }
// // // // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-06-24 09:52:37] -----
// // // // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // // // 
// // // // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // // // 
// // // // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // // // {
// // // // // // // // // // // //     public static class SceneMap
// // // // // // // // // // // //     {
// // // // // // // // // // // //         public enum UIScene
// // // // // // // // // // // //         {
// // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // //             ,MAINMENU = 1
// // // // // // // // // // // //             ,SETTINGS = 2
// // // // // // // // // // // //             ,BACKGROUND = 3
// // // // // // // // // // // //             ,PERKSMENU = 4
// // // // // // // // // // // //             ,PRELOBBY = 6
// // // // // // // // // // // //             ,MULTIPLAYERLOBBY = 7
// // // // // // // // // // // //         }
// // // // // // // // // // // // 
// // // // // // // // // // // //         public enum WorldScene
// // // // // // // // // // // //         {
// // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // //         }
// // // // // // // // // // // // 
// // // // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // // // //         {
// // // // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // // // // // //             ,{ UIScene.PERKSMENU, new("617237bda28bd3e4da33a2c6792148b3") }
// // // // // // // // // // // //             ,{ UIScene.PRELOBBY, new("2573ee49869a4445cbdeaee54598999b") }
// // // // // // // // // // // //             ,{ UIScene.MULTIPLAYERLOBBY, new("af83a586991a144c18bd6f5191ebb839") }
// // // // // // // // // // // //         };
// // // // // // // // // // // // 
// // // // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // // // //         {
// // // // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // // // //         };
// // // // // // // // // // // //     }
// // // // // // // // // // // // }
// // // // // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-06-23 22:50:51] -----
// // // // // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // // // // 
// // // // // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // // // // 
// // // // // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // // // // {
// // // // // // // // // // // // //     public static class SceneMap
// // // // // // // // // // // // //     {
// // // // // // // // // // // // //         public enum UIScene
// // // // // // // // // // // // //         {
// // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // //             ,MAINMENU = 1
// // // // // // // // // // // // //             ,SETTINGS = 2
// // // // // // // // // // // // //             ,BACKGROUND = 3
// // // // // // // // // // // // //         }
// // // // // // // // // // // // // 
// // // // // // // // // // // // //         public enum WorldScene
// // // // // // // // // // // // //         {
// // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // //         }
// // // // // // // // // // // // // 
// // // // // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // // // // //         {
// // // // // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // // // // // //             ,{ UIScene.SETTINGS, new("6ab36bc97fe6143939b652d186d4ee0b") }
// // // // // // // // // // // // //             ,{ UIScene.BACKGROUND, new("ab8bb89f4fa494f25abd2510f664b7da") }
// // // // // // // // // // // // //         };
// // // // // // // // // // // // // 
// // // // // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // // // // //         {
// // // // // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // // // // //         };
// // // // // // // // // // // // //     }
// // // // // // // // // // // // // }
// // // // // // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-06-20 09:46:40] -----
// // // // // // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // // // // // 
// // // // // // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // // // // // 
// // // // // // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // // // // // {
// // // // // // // // // // // // // //     public static class SceneMap
// // // // // // // // // // // // // //     {
// // // // // // // // // // // // // //         public enum UIScene
// // // // // // // // // // // // // //         {
// // // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // // //             ,MAINMENU = 1
// // // // // // // // // // // // // //         }
// // // // // // // // // // // // // // 
// // // // // // // // // // // // // //         public enum WorldScene
// // // // // // // // // // // // // //         {
// // // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // // //         }
// // // // // // // // // // // // // // 
// // // // // // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // // // // // //         {
// // // // // // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // // // // // //             ,{ UIScene.MAINMENU, new("2df3fef04bac0444e89b0a6ddb8e2c34") }
// // // // // // // // // // // // // //         };
// // // // // // // // // // // // // // 
// // // // // // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // // // // // //         {
// // // // // // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // // // // // //         };
// // // // // // // // // // // // // //     }
// // // // // // // // // // // // // // }
// // // // // // // // // // // // // // // ----- Previous SceneMap [Deprecated scince 2025-06-20 09:44:26] -----
// // // // // // // // // // // // // // // // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// // // // // // // // // // // // // // // 
// // // // // // // // // // // // // // // using System.Collections.Generic; 
// // // // // // // // // // // // // // // using UnityEngine.AddressableAssets;
// // // // // // // // // // // // // // // 
// // // // // // // // // // // // // // // namespace KekwDetlef.SceneManagement
// // // // // // // // // // // // // // // {
// // // // // // // // // // // // // // //     public static class SceneMap
// // // // // // // // // // // // // // //     {
// // // // // // // // // // // // // // //         public enum UIScene
// // // // // // // // // // // // // // //         {
// // // // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // // // //         }
// // // // // // // // // // // // // // // 
// // // // // // // // // // // // // // //         public enum WorldScene
// // // // // // // // // // // // // // //         {
// // // // // // // // // // // // // // //            NONE = 0
// // // // // // // // // // // // // // //         }
// // // // // // // // // // // // // // // 
// // // // // // // // // // // // // // //         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
// // // // // // // // // // // // // // //         {
// // // // // // // // // // // // // // //             { UIScene.NONE, null }
// // // // // // // // // // // // // // //         };
// // // // // // // // // // // // // // // 
// // // // // // // // // // // // // // //        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
// // // // // // // // // // // // // // //         {
// // // // // // // // // // // // // // //             { WorldScene.NONE, null }
// // // // // // // // // // // // // // //         };
// // // // // // // // // // // // // // //     }
// // // // // // // // // // // // // // // }
