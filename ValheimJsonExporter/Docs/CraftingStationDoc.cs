using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;

namespace ValheimJsonExporter.Docs
{
    public class CraftingStationDoc : Doc
    {
        public CraftingStationDoc() : base("crafting-station-list.json")
        {
            ItemManager.OnItemsRegistered += DocCraftingStations;
        }

        public CraftingStationDoc(string file) : base(file)
        {
            ItemManager.OnItemsRegistered += DocCSCraftingStations;
        }

        private void DocCraftingStations()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting crafting stations");

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
                jsonStation.Add("have_fire", station.m_haveFire);
                jsonStation.Add("show_basic_recipes", station.m_showBasicRecipies);
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

        private void DocCSCraftingStations()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting RAW crafting stations");

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


                jsonStation.Add("m_name", ValheimJsonExporter.Localize(station.m_name)); // CraftingStation
                jsonStation.Add("m_discoverRange", station.m_discoverRange);
                jsonStation.Add("m_rangeBuild", station.m_rangeBuild);
                jsonStation.Add("m_craftRequireRoof", station.m_craftRequireRoof);
                jsonStation.Add("m_craftRequireFire", station.m_craftRequireFire);
                jsonStation.Add("m_useDistance", station.m_useDistance);
                jsonStation.Add("m_useTimer", station.m_useTimer);
                jsonStation.Add("m_haveFire", station.m_haveFire);
                jsonStation.Add("m_showBasicRecipies", station.m_showBasicRecipies);
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
