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

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting piece tables");

            var pieceTables = ReflectionHelper.GetPrivateField<Dictionary<string, PieceTable>>(PieceManager.Instance, "PieceTables");
            var nameMap = ReflectionHelper.GetPrivateField<Dictionary<string, string>>(PieceManager.Instance, "PieceTableNameMap");

            
            SimpleJson.JsonArray jsonTables = new SimpleJson.JsonArray();
            

            foreach (var pair in pieceTables)
            {
                string alias = "";
                
                SimpleJson.JsonObject jsonPieceTable = new SimpleJson.JsonObject();

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

                jsonPieceTable.Add("raw_name", alias); 
                jsonPieceTable.Add("var_name", null);
                jsonPieceTable.Add("true_name", pair.Key); 
                jsonPieceTable.Add("num_pieces", pair.Value.m_pieces.Count.ToString());

                jsonTables.Add(jsonPieceTable);
            }

            // write to the file
            AddText(jsonTables.ToString());

            Save();
        }
    }
}
