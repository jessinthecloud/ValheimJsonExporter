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

            Jotunn.Logger.LogInfo("Documenting pieces");

        
            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTables");

            foreach (var pair in pieceTables)
            {
                foreach (GameObject obj in pair.Value.m_pieces)
                {
                    Piece piece = obj.GetComponent<Piece>();

                    if (piece == null)
                    {
                        continue;
                    }

                    string resources = "<ul>";

                    foreach (Piece.Requirement req in piece.m_resources)
                    {
                        resources += "<li>" + req.m_amount + " " +
                            ValheimJsonExporter.Localize(req?.m_resItem?.m_itemData?.m_shared?.m_name) + "</li>";
                    }

                    resources += "</ul>";

                    AddTableRow(
                        pair.Key,
                        obj.name,
                        ValheimJsonExporter.Localize(piece.m_name),
                        ValheimJsonExporter.Localize(piece.m_description),
                        resources);
                }
            }

            Save();
        }
    }
}
