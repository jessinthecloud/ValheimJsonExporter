using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;

namespace ValheimJsonExporter.Docs
{
    public class CraftingStationDoc : Doc
    {
        public CraftingStationDoc() : base("ValheimJsonExporter/Docs/crafting-station-list.json")
        {
            ItemManager.OnItemsRegistered += DocCraftingStations;
        }

        private void DocCraftingStations()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("--- VALHEIM JSON EXPORTER Documenting crafting stations ---");


            // create array to hold all of the stations
            SimpleJson.JsonArray stations = new SimpleJson.JsonArray();

            foreach (Recipe recipe in ObjectDB.instance.m_recipes)
            {
                CraftingStation station = recipe.m_craftingStation;
                
                if (recipe == null || !recipe.m_craftingStation || stations.Contains(station.m_name))
                {
                    continue;
                }
                
                // Object to hold info about a single item
                SimpleJson.JsonObject jsonStation = new SimpleJson.JsonObject();


                jsonStation.Add("raw_name", ValheimJsonExporter.Localize(station.m_name)); // CraftingStation
                jsonStation.Add("var_name", station.m_name); // CraftingStation
                jsonStation.Add("true_name", station.name);
                jsonStation.Add("discover_range", station.m_discoverRange);
                jsonStation.Add("range_build", station.m_rangeBuild);
                jsonStation.Add("craft_require_roof", station.m_craftRequireRoof);
                jsonStation.Add("craft_require_fire", station.m_craftRequireFire);
                jsonStation.Add("use_distance", station.m_useDistance);
                jsonStation.Add("use_timer", station.m_useTimer);
                // List<StationExtension>()
                // jsonStation.Add("attached_extensions_true", station.m_attachedExtensions.toString());
                // List<StationExtension>()
                // jsonStation.Add("attached_extensions_raw", ValheimJsonExporter.Localize(station.m_attachedExtensions.toString())); 
                // jsonStation.Add("update_extension_interval", station.m_updateExtensionInterval);

                stations.Add(jsonStation);

            }

            // write to the file
            AddText(stations.ToString());

            Save();
        }
    }
}
