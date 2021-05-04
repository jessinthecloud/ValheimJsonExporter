﻿using Jotunn.Managers;
using UnityEngine;

namespace ValheimJsonExporter.Docs
{
    public class RecipeDoc : Doc
    {
        public RecipeDoc() : base("ValheimJsonExporter/Docs/conceptual/objects/recipe-list.json")
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

                // create array to hold resources for this recipe
                SimpleJson.JsonArray jsonResources = new SimpleJson.JsonArray();

                foreach (Piece.Requirement req in recipe.m_resources)
                {
                    // create object to hold the resource data
                    SimpleJson.JsonObject jsonResource = new SimpleJson.JsonObject();

                    jsonResource.Add("amount", req.m_amount);
                    jsonResource.Add("amountPerLevel", req.m_amountPerLevel);
                    jsonResource.Add("recover", req.m_recover);
                    // recipe?.m_item?.m_itemData?.m_dropPrefab?.name is unique item name?
                    jsonResource.Add("name", ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name));

                    // add to resources array
                    jsonResources.Add(jsonResource);
                    
                } // end each resource

                // Object to hold info about a single item
                SimpleJson.JsonObject jsonRecipe = new SimpleJson.JsonObject();

                jsonRecipe.Add("name", recipe.name);
                jsonRecipe.Add("itemName", ValheimJsonExporter.Localize(recipe?.m_item?.m_itemData?.m_shared?.m_name));
                jsonRecipe.Add("enabled", recipe.m_enabled);
                jsonRecipe.Add("minStationLevel", recipe.m_minStationLevel);
                // jsonRecipe.Add("craftingStation", recipe.m_craftingStation.m_name); // CraftingStation
                // jsonRecipe.Add("repairStation", recipe.m_repairStation.m_name); // CraftingStation
                jsonRecipe.Add("resources", jsonResources);

                // add recipe to recipes array
                recipesArr.Add(jsonRecipe);
            }

            // write to the file
            AddText(recipesArr.ToString());

            Save();
        }
    }
}