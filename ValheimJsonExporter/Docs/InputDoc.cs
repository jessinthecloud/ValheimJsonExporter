using UnityEngine;
using UnityEngine.SceneManagement;

namespace ValheimJsonExporter.Docs
{
    public class InputDoc : Doc
    {

        public InputDoc() : base("input-list.json")
        {
            SceneManager.sceneLoaded += docInputs;
        }
        
        public InputDoc(string file) : base(file)
        {
            SceneManager.sceneLoaded += docCSInputs;
        }

        public void docInputs(Scene scene, LoadSceneMode mode)
        {
            if (Generated)
            {
                return;
            }

            if (scene.name != "main")
            {
                return;
            }

            Jotunn.Logger.LogInfo("Documenting inputs");

            var buttons = ZInput.instance.m_buttons;

            foreach (var pair in buttons)
            {
                ZInput.ButtonDef button = pair.Value;
                //AddText(pair.Key, button.m_key.ToString(), button.m_axis, button.m_gamepad.ToString());
            }

            Save();
        }

        public void docCSInputs(Scene scene, LoadSceneMode mode)
        {
            if (Generated)
            {
                return;
            }

            if (scene.name != "main")
            {
                return;
            }

            Jotunn.Logger.LogInfo("Documenting inputs");

            var buttons = ZInput.instance.m_buttons;
            
            SimpleJson.JsonArray inputs = new SimpleJson.JsonArray();

            foreach (var pair in buttons)
            {
                ZInput.ButtonDef button = pair.Value;

                SimpleJson.JsonObject input = new SimpleJson.JsonObject();
                
                input.Add("name", pair.Key);
                input.Add("keycode", button.m_key.ToString());
                input.Add("axis", button.m_axis);
                input.Add("gamepad", button.m_gamepad.ToString());

                inputs.Add(input);
            }

            AddText(inputs.ToString());
            
            Save();
        }
    }
}
