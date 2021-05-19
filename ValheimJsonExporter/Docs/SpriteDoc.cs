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
        public SpriteDoc() : base("ValheimJsonExporter/Docs/sprite-list.json")
        {
            SceneManager.sceneLoaded += docSprites;
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

            Jotunn.Logger.LogInfo("VALHEIM JSON EXPORTER Documenting Sprites");

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
                // jsonSprite.Add("image", ImageConversion.EncodeToJPG(sprite.texture, 100));
                Texture2D readableTex = duplicateTexture(sprite.texture, sprite.textureRect);
                byte[] data = ImageConversion.EncodeToJPG(readableTex); // tex.EncodeToJPG();
                // byte[] data = sprite.texture.EncodeToJPG();

                string file = Paths.PluginPath + "/ValheimJsonExporter/Docs/sprites/" + sprite.name + ".jpg";
                File.WriteAllBytes(file, data);
                
                Jotunn.Logger.LogInfo(" ----===----VALHEIM JSON EXPORTER ---- sprite saved to "+file);

                // add sprite to array
                spritesArr.Add(jsonSprite);
            }

            // write to the file
            AddText(spritesArr.ToString());

            Save();
        } // end docSprites

        // https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-script/44734346#44734346
        Texture2D duplicateTexture(Texture2D source, Rect sourceRect)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableTex = new Texture2D(source.width, source.height);
            // sprite.textureRect
            readableTex.ReadPixels(sourceRect, 0, 0);
            // readableTex.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableTex.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableTex;
        }
    }
}
