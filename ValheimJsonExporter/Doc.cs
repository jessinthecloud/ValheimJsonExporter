using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using BepInEx;


namespace ValheimJsonExporter
{
    public class Doc
    {
        public bool Generated { get; private set; }
        public string FilePath { get; protected set; }
        protected static string writepath = "ValheimJsonExporter/Docs/";

        private StreamWriter writer;

        public Doc(string filePath)
        {
            FilePath = Path.Combine(Paths.PluginPath, writepath, filePath);
            // Debug.Log(" ----------- FilePath: "+FilePath);

            // Ensure we only create json files
            if (!FilePath.EndsWith(".json"))
            {
                FilePath += ".json";
            }

            // Create directory if it doesn't exist
            (new FileInfo(FilePath)).Directory.Create();

            writer = File.CreateText(FilePath);
        }

        public void AddText(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }

        public void Save()
        {
            writer.Flush();
            writer.Close();
            Generated = true;
        }
    }
}
