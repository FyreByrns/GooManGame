using System;
using System.Collections.Generic;
using System.IO;
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
        static Dictionary<string, ManifestEntry> manifest = new Dictionary<string, ManifestEntry>();

        static bool NameInManifest(string name)
            => manifest.ContainsKey(name);

        public static Sprite GetSprite(string assetName) {
            if (loadedSprites.ContainsKey(assetName))
                return loadedSprites[assetName];
            else return null;
        }

        public static void LoadManifest() {
            if (!File.Exists(ManifestPath))
                File.WriteAllLines(ManifestPath,
                    new[] {
                        "# Asset manifest ",
                        "# format:",
                        "# asset name|asset type|relative path to asset"
                    });

            foreach (string s in File.ReadAllLines(ManifestPath)) {
                if (s.StartsWith("#")) continue;

                string[] contents = s.Split('|');
                manifest[contents[0]] = new ManifestEntry((AssetType)Enum.Parse(typeof(AssetType), contents[1]), IO.AssetsPath + contents[2]);
            }
        }

        public static void Load(string assetName) {
            if (!string.IsNullOrEmpty(assetName) && NameInManifest(assetName)) {
                ManifestEntry assetManifest = manifest[assetName];

                switch (assetManifest.type) {
                    case AssetType.Text:
                        break;
                    case AssetType.StaticSprite:
                        loadedSprites[assetName] = Sprite.Load(assetManifest.path);
                        break;
                    default:
                        Debug.RaiseError($"Asset {assetName}'s type ({assetManifest.type}) cannot be parsed. The asset has not been loaded.");
                        break;
                }
            }
            else 
                Debug.RaiseWarning($"Asset {assetName} not found in manifest. This will cause problems.");
        }

        public static void Unload(string assetName) {
            if (!string.IsNullOrEmpty(assetName) && NameInManifest(assetName)) {
                ManifestEntry assetManifest = manifest[assetName];

                switch (assetManifest.type) {
                    case AssetType.Text:
                        break;
                    case AssetType.StaticSprite:
                        loadedSprites.Remove(assetName);
                        break;
                }
            }
            else
                Debug.RaiseWarning($"Attempting to unload {assetName} which does not exist in manifest.");
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
