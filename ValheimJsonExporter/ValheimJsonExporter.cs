// ValheimJsonExporter
// a Valheim mod skeleton using Jötunn
// 
// File:    ValheimJsonExporter.cs
// Project: ValheimJsonExporter

using BepInEx;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BepInEx.Configuration;
using Jotunn.Utils;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using ValheimJsonExporter.Docs;
using System.Web;

namespace ValheimJsonExporter
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ValheimJsonExporter : BaseUnityPlugin
    {
        public const string PluginGUID = "jessinthecloud.ValheimJsonExporter";
        public const string PluginName = "ValheimJsonExporter";
        public const string PluginVersion = "0.0.1";

        private List<Doc> docs;

        private void Awake()
        {
            docs = new List<Doc>()
            {
                new CraftingStationDoc(),
                new StatusEffectDoc(),
                new ItemDoc(),
                new RecipeDoc(),
                new PieceTableDoc(),
                new PieceDoc(),                
                new PrefabDoc(),
                // new SpriteDoc(),                
            };

            Debug.Log("Initialized ValheimJsonExporter");
        }

        // Localize and strip text from text
        public static string Localize(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "NULL";
            }

            string text = Localization.instance.Localize(key).Replace("\n", "<br/>");
            // strip non-ascii
            text = Regex.Replace(text, @"[^\u0000-\u007F]+", string.Empty);
            // escape for JS
            // text = HttpUtility.JavaScriptStringEncode(text); // is just blank, BepinEx needs System.Web.dll

            return text;
        }

    }
}
