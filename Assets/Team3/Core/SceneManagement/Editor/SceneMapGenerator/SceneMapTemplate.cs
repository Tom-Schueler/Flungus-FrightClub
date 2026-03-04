using System.Text;

namespace KekwDetlef.SceneManagement.Editor
{
    public static class SceneMapTemplate
    {
        public static string FinishedSceneMapString(string uiSceneEnumStringFormated, string uiSceneDictionaryStringFormated, string worldSceneEnumStringFormated, string worldSceneDictionaryStringFormated)
        {
            StringBuilder final = new StringBuilder();

            final.Append("// This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. \n\n"); // ForeWord
            final.Append("using System.Collections.Generic; \nusing UnityEngine.AddressableAssets;\n\n"); // Using directives
            final.Append("namespace KekwDetlef.SceneManagement\n{\n");
            final.Append("    public static class SceneMap\n    {\n"); // class
            final.Append("        public enum UIScene\n        {\n           "); // ui enum start
            final.Append(uiSceneEnumStringFormated);
            final.Append("\n        }\n\n        public enum WorldScene\n        {\n           "); // ui enum end & world enum start
            final.Append(worldSceneEnumStringFormated);
            final.Append("\n        }\n\n        public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()\n        {\n            "); // world enum end & ui dict start
            final.Append(uiSceneDictionaryStringFormated);
            final.Append("\n        };\n\n       public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()\n        {\n            "); // ui dict end & world dict start
            final.Append(worldSceneDictionaryStringFormated);
            final.Append("\n        };\n    }\n}"); // world dict end

            return final.ToString();
        }
    }
}