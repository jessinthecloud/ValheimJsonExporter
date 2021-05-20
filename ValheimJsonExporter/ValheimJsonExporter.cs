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
                new SpriteDoc(),                
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
           /* 
            * none of this is working
            // remove invalid hex
            // not working
            text = Regex.Replace(text, @"(?i)[\x00-\x1F\x7F]+", string.Empty);
            // not working
            text = Regex.Replace(text, @"(?i)[\u0000-\u001F\u007F]+", string.Empty);

            // YOU MADE ME DO THIS, C#
            text = text.Replace('\u0000', ' ')
        .Replace('\u0001', ' ')
        .Replace('\u0002', ' ')
        .Replace('\u0003', ' ')
        .Replace('\u0004', ' ')
        .Replace('\u0005', ' ')
        .Replace('\u0006', ' ')
        .Replace('\u0007', ' ')
        .Replace('\u0008', ' ')
        .Replace('\u0009', ' ')
        .Replace('\u000a', ' ')
        .Replace('\u000b', ' ')
        .Replace('\u000c', ' ')
        .Replace('\u000d', ' ')
        .Replace('\u000e', ' ')
        .Replace('\u000f', ' ')
        .Replace('\u0010', ' ')
        .Replace('\u0011', ' ')
        .Replace('\u0012', ' ')
        .Replace('\u0013', ' ')
        .Replace('\u0014', ' ')
        .Replace('\u0015', ' ')
        .Replace('\u0016', ' ')
        .Replace('\u0017', ' ')
        .Replace('\u0018', ' ')
        .Replace('\u0019', ' ')
        .Replace('\u001a', ' ')
        .Replace('\u001b', ' ')
        .Replace('\u001c', ' ')
        .Replace('\u001d', ' ')
        .Replace('\u001e', ' ')
        .Replace('\u001f', ' ');*/

            // escape for JS
            // text = HttpUtility.JavaScriptStringEncode(text); // is just blank, BepinEx needs System.Web.dll

            return text;
        }

    }
}
