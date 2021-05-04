﻿using Jotunn.Managers;
using UnityEngine;

namespace ValheimJsonExporter.Docs
{
    public class PrefabDoc : Doc
    {
        public PrefabDoc() : base("ValheimJsonExporter/Docs/conceptual/prefabs/prefab-list.json")
        {
            PrefabManager.OnPrefabsRegistered += DocPrefabs;
        }

        private void DocPrefabs()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("Documenting prefabs");

            AddHeader(1, "Prefab list");
            AddText("All of the prefabs currently in the game.");
            AddText("This file is automatically generated from Valheim using the ValheimJsonExporter mod found on our GitHub.");
            AddTableHeader("Name", "Components");

            foreach (GameObject obj in ZNetScene.instance.m_prefabs)
            {
                string components = "<ul>";

                foreach (Component comp in obj.GetComponents<Component>())
                {
                    components += "<li>" + comp.GetType().Name + "</li>";
                }

                components += "</ul>";

                AddTableRow(obj.name, components);
            }

            Save();
        }
    }
}
