﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ValheimJsonExporter.Docs
{
    public class PieceDoc : Doc
    {
        public PieceDoc() : base("ValheimJsonExporter/Docs/conceptual/pieces/piece-list.json")
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

            AddHeader(1, "Piece list");
            AddText("All of the pieces currently in the game.");
            AddText("This file is automatically generated from Valheim using the ValheimJsonExporter mod found on our GitHub.");
            AddTableHeader("Piece Table", "Prefab Name", "Piece Name", "Piece Description");

            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "pieceTables");

            foreach (var pair in pieceTables)
            {
                foreach (GameObject obj in pair.Value.m_pieces)
                {
                    Piece piece = obj.GetComponent<Piece>();
                    AddTableRow(
                        pair.Key,
                        obj.name,
                        ValheimJsonExporter.Localize(piece.m_name),
                        ValheimJsonExporter.Localize(piece.m_description));
                }
            }

            Save();
        }
    }
}