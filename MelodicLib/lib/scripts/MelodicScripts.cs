﻿using System.IO;
using UnityEngine;

namespace MelodicLib.lib.scripts
{
    class MelodicScripts
    {
        public static Sprite Sprite2(string iconpath)
        {
            var path = Path.Combine(MelodicLib.PersistentDataPath, iconpath);
            if (!File.Exists(path))
            {
                MelodicLog.Log($"ERROR: [{MelodicLib.modName} | Items]: Specified Icon path not found: " + path);

                Debug.LogError($"[{MelodicLib.modName} | Items]: Specified Icon path not found: " + path);
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
    }
}
