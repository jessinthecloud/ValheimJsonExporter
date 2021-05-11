﻿using Jotunn.Managers;
using UnityEngine;

namespace ValheimJsonExporter.Docs
{
    public class RecipeDoc : Doc
    {
        public RecipeDoc() : base("ValheimJsonExporter/Docs/recipe-list.json")
        {
            ItemManager.OnItemsRegistered += DocRecipes;
        }

        private void DocRecipes()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("VALHEIM JSON EXPORTER Documenting recipes");

            // create array to hold all of the recipe objects
            SimpleJson.JsonArray recipesArr = new SimpleJson.JsonArray();

            foreach (Recipe recipe in ObjectDB.instance.m_recipes)
            {
                if (recipe == null)
                {
                    continue;
                }

                // create array to hold requirements for this recipe
                SimpleJson.JsonArray jsonRequirements = new SimpleJson.JsonArray();

                foreach (Piece.Requirement req in recipe.m_resources)
                {
                    // create object to hold the requirement data
                    SimpleJson.JsonObject jsonRequirement = new SimpleJson.JsonObject();

                    jsonRequirement.Add("amount", req.m_amount);
                    jsonRequirement.Add("amount_per_level", req.m_amountPerLevel);
                    jsonRequirement.Add("recover", req.m_recover);
                    // recipe?.m_item?.m_itemData?.m_dropPrefab?.name is unique item name? -- is always null...
                    // jsonRequirement.Add("prefab_name", ValheimJsonExporter.Localize(recipe?.m_item?.m_itemData?.m_dropPrefab?.name));
                    // item name
                    jsonRequirement.Add("raw_name", ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name));
                    jsonRequirement.Add("var_name", req?.m_resItem?.m_itemData?.m_shared?.m_name);

                    // add to requirements array
                    jsonRequirements.Add(jsonRequirement);

                } // end each requirement

                // Object to hold info about a single item
                SimpleJson.JsonObject jsonRecipe = new SimpleJson.JsonObject();

                // names
                if(recipe?.m_item?.m_itemData?.m_shared?.m_name != null)
                {
                    jsonRecipe.Add("raw_name", ValheimJsonExporter.Localize(recipe?.m_item?.m_itemData?.m_shared?.m_name)); // item name
                }
                else
                {
                    jsonRecipe.Add("raw_name", null); // item name
                }
                jsonRecipe.Add("var_name", recipe?.m_item?.m_itemData?.m_shared?.m_name); // item name
                jsonRecipe.Add("true_name", recipe.name); // true name of recipe

                jsonRecipe.Add("enabled", recipe.m_enabled);
                jsonRecipe.Add("min_station_level", recipe.m_minStationLevel);

                if (recipe.m_craftingStation)
                {
                    jsonRecipe.Add("raw_crafting_station_name", ValheimJsonExporter.Localize(recipe.m_craftingStation.m_name)); // CraftingStation
                    jsonRecipe.Add("var_crafting_station_name", recipe.m_craftingStation.m_name); // CraftingStation
                    jsonRecipe.Add("true_crafting_station_name", recipe.m_craftingStation.name); // CraftingStation
                }
                else
                {
                    jsonRecipe.Add("raw_crafting_station_name", null); // CraftingStation
                }
                if (recipe.m_repairStation)
                {
                    jsonRecipe.Add("raw_repair_station_name", ValheimJsonExporter.Localize(recipe.m_repairStation.m_name)); // CraftingStation
                    jsonRecipe.Add("var_repair_station_name", recipe.m_repairStation.m_name); // CraftingStation
                    jsonRecipe.Add("true_repair_station_name", recipe.m_repairStation.name); // CraftingStation
                }
                else
                {
                    jsonRecipe.Add("raw_repair_station_name", null); // CraftingStation
                }

                jsonRecipe.Add("requirements", jsonRequirements);

                // add recipe to recipes array
                recipesArr.Add(jsonRecipe);
            }

            // write to the file
            AddText(recipesArr.ToString());

            Save();
        }
    }
}
