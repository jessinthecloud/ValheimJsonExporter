using System;
using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ValheimJsonExporter.Docs
{
    public class PieceDoc : Doc
    {
        public PieceDoc() : base("ValheimJsonExporter/Docs/piece-list.json")
        {
            PieceManager.OnPiecesRegistered += docPieces;
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
                    jsonPiece.Add("description", piece.m_description);
                    jsonPiece.Add("piece_table_true_name", pair.Key);
                    jsonPiece.Add("requirements", jsonRequirements);

                    piecesArr.Add(jsonPiece);

                } // end foreach piece
            } // end foreach piece table

            // write to the file
            AddText(piecesArr.ToString());

            Save();
        }
    }
}
