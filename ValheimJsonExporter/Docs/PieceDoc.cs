using System;
using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ValheimJsonExporter.Docs
{
    public class PieceDoc : Doc
    {
        
        public PieceDoc() : base("piece-list.json")
        {
            PieceManager.OnPiecesRegistered += docPieces;
        }

        public PieceDoc(string file) : base(file)
        {
            PieceManager.OnPiecesRegistered += docCSPieces;
        }

        public void docPieces()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON ExPORTER Documenting PIECES");

        
            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTables");

            // pieces
            SimpleJson.JsonArray piecesArr = new SimpleJson.JsonArray();

            foreach (var pair in pieceTables)
            {
                foreach (GameObject obj in pair.Value.m_pieces)
                {
                    Piece piece = obj.GetComponent<Piece>();

                    if (piece == null)
                    {
                        continue;
                    }

                    
                    // create array to hold requirements for this piece
                    SimpleJson.JsonArray jsonRequirements = new SimpleJson.JsonArray();
                    
                    foreach (Piece.Requirement req in piece.m_resources)
                    {
                        //resources += "<li>" + req.m_amount + " " +
                            //ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name) + "</li>";

                        // create object to hold the requirement data
                        SimpleJson.JsonObject jsonRequirement = new SimpleJson.JsonObject();

                        jsonRequirement.Add("amount", req.m_amount);
                        jsonRequirement.Add("amount_per_level", req.m_amountPerLevel);
                        jsonRequirement.Add("recover", req.m_recover);
                        // item name
                        jsonRequirement.Add("raw_name", ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name));
                        jsonRequirement.Add("var_name", req?.m_resItem?.m_itemData?.m_shared?.m_name);

                        // add to requirements array
                        jsonRequirements.Add(jsonRequirement);
                    } // end foreach requirement

                    SimpleJson.JsonObject jsonPiece = new SimpleJson.JsonObject();

                    jsonPiece.Add("raw_name", ValheimJsonExporter.Localize(piece.m_name));
                    jsonPiece.Add("var_name", piece.m_name);
                    jsonPiece.Add("true_name", obj.name);
                    jsonPiece.Add("prefab_name", obj.name);
                    jsonPiece.Add("description", piece.m_description);
                    jsonPiece.Add("piece_table_true_name", pair.Key);
                    // public bool m_enabled = true;
                    jsonPiece.Add("enabled", piece.m_enabled);
                    // public PieceCategory m_category;
                    jsonPiece.Add("category", piece.m_category);
                    // public bool m_isUpgrade;
                    jsonPiece.Add("is_upgrade", piece.m_isUpgrade);
                    // public int m_comfort;
                    jsonPiece.Add("comfort", piece.m_comfort);
                    // public ComfortGroup m_comfortGroup;
                    jsonPiece.Add("comfort_group", piece.m_comfortGroup);
                    // Rules
                    // booleans 
                    jsonPiece.Add("ground_piece", piece.m_groundPiece);
                    jsonPiece.Add("allow_alt_ground_placement", piece.m_allowAltGroundPlacement);
                    jsonPiece.Add("ground_only", piece.m_groundOnly);
                    jsonPiece.Add("cultivated_ground_only", piece.m_cultivatedGroundOnly);
                    jsonPiece.Add("water_piece", piece.m_waterPiece);
                    jsonPiece.Add("clip_ground", piece.m_clipGround);
                    jsonPiece.Add("clip_everything", piece.m_clipEverything);
                    jsonPiece.Add("no_in_water", piece.m_noInWater);
                    jsonPiece.Add("not_on_wood", piece.m_notOnWood);
                    jsonPiece.Add("not_on_tilting_surface", piece.m_notOnTiltingSurface);
                    jsonPiece.Add("in_ceiling_only", piece.m_inCeilingOnly);
                    jsonPiece.Add("not_on_floor", piece.m_notOnFloor);
                    jsonPiece.Add("no_clipping", piece.m_noClipping);
                    jsonPiece.Add("only_in_teleport_area", piece.m_onlyInTeleportArea);
                    jsonPiece.Add("allowed_in_dungeons", piece.m_allowedInDungeons);
                    // public float m_spaceRequirement;
                    jsonPiece.Add("space_requirement", piece.m_spaceRequirement);
                    // public bool m_repairPiece;
                    jsonPiece.Add("repair_piece", piece.m_repairPiece);
                    // public bool m_canBeRemoved = true;
                    jsonPiece.Add("can_be_removed", piece.m_canBeRemoved);
                    // public Heightmap.Biome m_onlyInBiome; (enum)
                    jsonPiece.Add("only_in_biome", piece.m_onlyInBiome);
                    // public EffectList m_placeEffect = new EffectList();
                    // jsonPiece.Add("place_effect", piece.m_placeEffect);
                    // public string m_dlc = "";
                    jsonPiece.Add("dlc", piece.m_dlc);
                    // public Sprite m_icon;
                    // jsonPiece.Add("icon", piece.m_icon.name);

                    jsonPiece.Add("requirements", jsonRequirements);
                    
                    // public CraftingStation m_craftingStation;
                    if (piece.m_craftingStation)
                    {
                        jsonPiece.Add("raw_crafting_station_name", ValheimJsonExporter.Localize(piece.m_craftingStation.m_name)); // CraftingStation
                        jsonPiece.Add("var_crafting_station_name", piece.m_craftingStation.m_name); // CraftingStation
                        jsonPiece.Add("true_crafting_station_name", piece.m_craftingStation.name); // CraftingStation
                    }

                    piecesArr.Add(jsonPiece);

                } // end foreach piece
            } // end foreach piece table

            // write to the file
            AddText(piecesArr.ToString());

            Save();
        }

        public void docCSPieces()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON ExPORTER Documenting PIECES");
        
            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTables");

            // pieces
            SimpleJson.JsonArray piecesArr = new SimpleJson.JsonArray();

            foreach (var pair in pieceTables)
            {
                foreach (GameObject obj in pair.Value.m_pieces)
                {
                    Piece piece = obj.GetComponent<Piece>();

                    if (piece == null)
                    {
                        continue;
                    }
                    
                    // create array to hold requirements for this piece
                    SimpleJson.JsonArray jsonRequirements = new SimpleJson.JsonArray();

                    // public Requirement[] m_resources = new Requirement[0];
                    foreach (Piece.Requirement req in piece.m_resources)
                    {
                        
                        // create object to hold the requirement data
                        SimpleJson.JsonObject jsonRequirement = new SimpleJson.JsonObject();

                        jsonRequirement.Add("m_amount", req.m_amount);
                        // item name
                        jsonRequirement.Add("m_name", ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name));

                        // add to requirements array
                        jsonRequirements.Add(jsonRequirement);
                    } // end foreach requirement

                    SimpleJson.JsonObject jsonPiece = new SimpleJson.JsonObject();

                    jsonPiece.Add("piece_table", pair.Key);
                    jsonPiece.Add("prefab_name", obj.name);
                    jsonPiece.Add("m_name", ValheimJsonExporter.Localize(piece.m_name));
                    jsonPiece.Add("m_description", piece.m_description);
                    jsonPiece.Add("m_enabled", piece.m_enabled);
                    jsonPiece.Add("m_category", piece.m_category);
                    jsonPiece.Add("m_isUpgrade", piece.m_isUpgrade);
                    jsonPiece.Add("m_comfort", piece.m_comfort);
                    jsonPiece.Add("m_comfortGroup", piece.m_comfortGroup);
                    // Rules
                    jsonPiece.Add("m_groundPiece", piece.m_groundPiece);
                    jsonPiece.Add("m_allowAltGroundPlacement", piece.m_allowAltGroundPlacement);
                    jsonPiece.Add("m_groundOnly", piece.m_groundOnly);
                    jsonPiece.Add("m_cultivatedGroundOnly", piece.m_cultivatedGroundOnly);
                    jsonPiece.Add("m_waterPiece", piece.m_waterPiece);
                    jsonPiece.Add("m_clipGround", piece.m_clipGround);
                    jsonPiece.Add("m_clipEverything", piece.m_clipEverything);
                    jsonPiece.Add("m_noInWater", piece.m_noInWater);
                    jsonPiece.Add("m_notOnWood", piece.m_notOnWood);
                    jsonPiece.Add("m_notOnTiltingSurface", piece.m_notOnTiltingSurface);
                    jsonPiece.Add("m_inCeilingOnly", piece.m_inCeilingOnly);
                    // public bool m_notOnFloor;
                    jsonPiece.Add("m_notOnFloor", piece.m_notOnFloor);
                    // public bool m_noClipping;
                    jsonPiece.Add("m_noClipping", piece.m_noClipping);
                    // public bool m_onlyInTeleportArea;
                    jsonPiece.Add("m_onlyInTeleportArea", piece.m_onlyInTeleportArea);
                    // public bool m_allowedInDungeons;
                    jsonPiece.Add("m_allowedInDungeons", piece.m_allowedInDungeons);
                    // public float m_spaceRequirement;
                    jsonPiece.Add("m_spaceRequirement", piece.m_spaceRequirement);
                    // public bool m_repairPiece;
                    jsonPiece.Add("m_repairPiece", piece.m_repairPiece);
                    // public bool m_canBeRemoved = true;
                    jsonPiece.Add("m_canBeRemoved", piece.m_canBeRemoved);
                    // public Heightmap.Biome m_onlyInBiome; (enum)
                    jsonPiece.Add("m_onlyInBiome", piece.m_onlyInBiome);
                    // public EffectList m_placeEffect = new EffectList();
                    // jsonPiece.Add("m_placeEffect", piece.m_placeEffect);
                    // public string m_dlc = "";
                    jsonPiece.Add("m_dlc", piece.m_dlc);
                    // public CraftingStation m_craftingStation;
                    if (piece.m_craftingStation)
                    {
                        jsonPiece.Add("m_craftingStation", ValheimJsonExporter.Localize(piece.m_craftingStation.m_name)); // CraftingStation
                    }
                    // public Sprite m_icon;
                    jsonPiece.Add("m_icon", piece.m_icon);

                    jsonPiece.Add("m_resources", jsonRequirements);

                    piecesArr.Add(jsonPiece);

                } // end foreach piece
            } // end foreach piece table

            // write to the file
            AddText(piecesArr.ToString());

            Save();
        }
    }
}
