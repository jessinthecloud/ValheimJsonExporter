using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using BepInEx;

namespace ValheimJsonExporter.Docs
{
    public class SpriteDoc : Doc
    {
        public SpriteDoc() : base("sprite-list.json")
        {
            SceneManager.sceneLoaded += docSprites;
        }

        public SpriteDoc(string file) : base(file)
        {
            SceneManager.sceneLoaded += docCSSprites;
        }

        public void docSprites(Scene scene, LoadSceneMode mode)
        {
            if (Generated)
            {
                return;
            }

            if (scene.name != "main")
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting Sprites");

            // create array to hold all of the recipe objects
            SimpleJson.JsonArray spritesArr = new SimpleJson.JsonArray();

            var sprites = Resources.FindObjectsOfTypeAll<Sprite>().OrderBy(x => x.name);

            foreach (var sprite in sprites)
            {
                // create object to hold the sprite data
                SimpleJson.JsonObject jsonSprite = new SimpleJson.JsonObject();

                jsonSprite.Add("raw_name", ValheimJsonExporter.Localize(sprite.name));
                jsonSprite.Add("var_name", sprite.name);
                jsonSprite.Add("texture", sprite.texture.name);
                jsonSprite.Add("coords", sprite.textureRect.ToString());               

                // add sprite to array
                spritesArr.Add(jsonSprite);
            }

            // write JSON to the file
            AddText(spritesArr.ToString());
            Save();

            // create an image from the sprite
            // do this separate because... crashes
           /* foreach (var sprite in sprites)
            {
                // textureRect is whole atlas?
                Texture2D readableTex = duplicateTexture(sprite.texture, sprite.textureRect);
                byte[] data = ImageConversion.EncodeToJPG(readableTex); 

                string file = Paths.PluginPath + "/ValheimJsonExporter/Docs/sprites/" + sprite.name + ".jpg";

                File.WriteAllBytes(file, data);

                Jotunn.Logger.LogInfo(" ----===----VALHEIM JSON EXPORTER ---- sprite saved to " + file);

            }*/

        } // end docSprites

        public void docCSSprites(Scene scene, LoadSceneMode mode)
        {
            if (Generated)
            {
                return;
            }

            if (scene.name != "main")
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting Sprites");

            // create array to hold all of the recipe objects
            SimpleJson.JsonArray spritesArr = new SimpleJson.JsonArray();

            var sprites = Resources.FindObjectsOfTypeAll<Sprite>().OrderBy(x => x.name);

            foreach (var sprite in sprites)
            {
                // create object to hold the sprite data
                SimpleJson.JsonObject jsonSprite = new SimpleJson.JsonObject();

                jsonSprite.Add("name", ValheimJsonExporter.Localize(sprite.name));
                jsonSprite.Add("texture_name", sprite.texture.name);
                jsonSprite.Add("textureRect", sprite.textureRect.ToString());               

                // add sprite to array
                spritesArr.Add(jsonSprite);
            }

            // write JSON to the file
            AddText(spritesArr.ToString());
            Save();
        } // end docSprites

        // https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-script/44734346#44734346
        Texture2D duplicateTexture(Texture2D source, Rect sourceRect)
        {
            // Create a temporary RenderTexture of the same size as the texture
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            // Blit the pixels on texture to the RenderTexture
            Graphics.Blit(source, renderTex);

            // Backup the currently set RenderTexture
            RenderTexture previous = RenderTexture.active;

            // Set the current RenderTexture to the temporary one we created
            RenderTexture.active = renderTex;

            // Create a new readable Texture2D to copy the pixels to it
            Texture2D readableTex = new Texture2D(source.width, source.height);

            // Copy the pixels from the RenderTexture to the new Texture
            readableTex.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableTex.Apply();

            // Reset the active RenderTexture
            RenderTexture.active = previous;

            // Release the temporary RenderTexture
            RenderTexture.ReleaseTemporary(renderTex);

            // "readableTex" now has the same pixels from "source" and it's readable.
            return readableTex;
        }
    }
}
