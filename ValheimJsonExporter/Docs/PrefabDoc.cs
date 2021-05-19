using Jotunn.Managers;
using UnityEngine;

namespace ValheimJsonExporter.Docs
{
    public class PrefabDoc : Doc
    {
        public PrefabDoc() : base("ValheimJsonExporter/Docs/prefab-list.json")
        {
            PrefabManager.OnPrefabsRegistered += DocPrefabs;
        }

        private void DocPrefabs()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting PREFABS");

            SimpleJson.JsonArray prefabArr = new SimpleJson.JsonArray();



            foreach (GameObject obj in ZNetScene.instance.m_prefabs)
            {

                SimpleJson.JsonObject prefab = new SimpleJson.JsonObject();
                
                SimpleJson.JsonArray components = new SimpleJson.JsonArray();

                foreach (Component comp in obj.GetComponents<Component>())
                {
                    SimpleJson.JsonObject component = new SimpleJson.JsonObject();

                    component.Add("raw_name", ValheimJsonExporter.Localize(comp.GetType().Name));
                    component.Add("true_name", comp.GetType().Name);
                }

                prefab.Add("var_name", null);
                prefab.Add("raw_name", ValheimJsonExporter.Localize(obj.name));
                prefab.Add("true_name", obj.name);
                prefab.Add("components", components);

                prefabArr.Add(prefab);
            }

            // write to the file
            AddText(prefabArr.ToString());

            Save();
        }
    }
}
