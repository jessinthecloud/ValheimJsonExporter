using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ValheimJsonExporter.Docs
{
    public class PieceTableDoc : Doc
    {
        public PieceTableDoc() : base("ValheimJsonExporter/Docs/piece-table-list.json")
        {
            PieceManager.OnPiecesRegistered += docPieceTables;
        }

        public void docPieceTables()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("Documenting piece tables");

            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTables");
            var nameMap = ReflectionHelper.GetPrivateField<Dictionary<string, string>>(PieceManager.Instance, "PieceTableNameMap");

            foreach (var pair in pieceTables)
            {
                string alias = "";

                if (nameMap.ContainsValue(pair.Key))
                {
                    foreach (string key in nameMap.Keys)
                    {
                        if (nameMap[key] == pair.Key)
                        {
                            alias = key;
                            break;
                        }
                    }
                }

                AddTableRow(pair.Key, alias, pair.Value.m_pieces.Count.ToString());
            }

            Save();
        }
    }
}
