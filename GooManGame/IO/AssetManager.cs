using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PixelEngine;

namespace GooManGame {
    public enum AssetType {
        None = 0,

        Text,
        StaticSprite
    }

    public static class AssetManager {
        public static string ManifestPath => IO.AssetsPath + "manifest.ini";
        
        static Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

        static Dictionary<string, ManifestEntry> manifest;
        static bool NameInManifest(string name)
            => manifest.ContainsKey(name);

        public static void LoadManifest() {

        }

        public static void Load(string assetName) {
            if (!string.IsNullOrEmpty(assetName) && NameInManifest(assetName)) {

                loadedSprites[assetName] = Sprite.Load($"assetName");
            }
        }

        public static void Clear()
            => loadedSprites.Clear();

        struct ManifestEntry {
            public AssetType type;
            public string path;

            public ManifestEntry(AssetType type, string path) {
                this.type = type;
                this.path = path;
            }

            public static implicit operator ManifestEntry(string input) {
                ManifestEntry result = new ManifestEntry();
                string[] contents = input.Split(' ');

                result.type = (AssetType)Enum.Parse(typeof(AssetType), contents[0]);
                result.path = contents[1];

                return result;
            }
        }
    }
}
