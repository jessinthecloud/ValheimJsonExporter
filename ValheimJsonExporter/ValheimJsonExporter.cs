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
                // new PrefabDoc(),    // crafting stations and such?
                new StatusEffectDoc(),
                new ItemDoc(),      // equippable/consumable/craftable items
                new RecipeDoc(),
                // new InputDoc(),
                // new PieceTableDoc(),
                // new PieceDoc(),                
                // new RPCDoc()
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
            text = Regex.Replace(text, @"[^\u0000-\u007F]+", string.Empty);

            return text;
        }

    }
}