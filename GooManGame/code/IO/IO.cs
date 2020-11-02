using System;
using System.Collections.Generic;
using System.IO;

using PixelEngine;

namespace GooManGame {
    /// <summary>
    /// State of an input.
    /// </summary>
    public enum InputState {
        None = 0,

        JustPressed,
        JustReleased,
        Held,
        Up,
    }

    /// <summary>
    /// Responsible for resolution, language, keybindings etc.
    /// </summary>
    public static class IO {
		#region paths and defaults
		/// <summary>
		/// Relative path from executable to data files.
		/// </summary>
		public static string DataPath = "data/";
        /// <summary>
        /// Relative path within data to config files.
        /// </summary>
        public static string ConfigPath => DataPath + "config/";
        /// <summary>
        /// Relative path within assets to asset files.
        /// </summary>
        public static string AssetsPath => DataPath + "assets/";

        /// <summary>
        /// Path to screen config.
        /// </summary>
        static string iniScreenPath => ConfigPath + "screen.ini";
        /// <summary>
        /// Default screen config values.
        /// </summary>
        static string[] iniScreenDefault = {
            "order: Screen Width, Screen Height, Screen Pixel Scale, FPS Lock",
            "300",
            "150",
            "4",
            "60"
        };

        /// <summary>
        /// Path to keybinding config.
        /// </summary>
        static string iniKeybindsPath => ConfigPath + "keybinds.ini";
        /// <summary>
        /// Default keybinds.
        /// </summary>
        static string[] iniKeybindsDefault = {
            "format: input name|key name",
            "up|Up",
            "down|Down",
            "left|Left",
            "right|Right",
            "cancel|Escape",
            "accept|Enter",
            "control|Control",
            "shift|Shift"
        };

        /// <summary>
        /// Path to mousebinding config.
        /// </summary>
        static string iniMousebindsPath => ConfigPath + "mousebinds.ini";
        /// <summary>
        /// Default mousebinds.
        /// </summary>
        static string[] iniMousebindsDefault = {
            "leftclick|Left",
            "rightclick|Right",
            "middleclick|Middle",
        };

        /// <summary>
        /// Path to multibinding config.
        /// </summary>
        static string iniMultibindsPath => ConfigPath + "multibinds.ini";
        /// <summary>
        /// Default multibinds.
        /// </summary>
        static string[] iniMultibindsDefault = {
            "controlleftclick|leftclick JustPressed,control Held",
            "shiftleftclick|leftclick JustPressed,shift Held",
            "controlrightclick|rightclick JustPressed,control Held",
            "shiftrightclick|rightclick JustPressed,shift Held"
        };
		#endregion paths and defaults

		public static int ScreenWidth = 600;
        public static int ScreenHeight = 350;
        public static int ScreenScale = 4;
        public static int FPSLock = 60;

		#region keybinds
		public static Dictionary<string, List<Key>> Keybinds = new Dictionary<string, List<Key>>();
        /// <summary>
        /// Add keybindings by name.
        /// </summary>
        public static void AddKeybind(string name, params Key[] keys) {
            if (!Keybinds.ContainsKey(name))
                Keybinds[name] = new List<Key>();

            foreach (Key key in keys)
                Keybinds[name].Add(key);
        }
        /// <summary>
        /// Remove keys from a keybinding by name.
        /// </summary>
        public static void RemoveKeybind(string name, params Key[] keys) {
            if (!Keybinds.ContainsKey(name)) return;

            foreach (Key key in keys)
                Keybinds[name].Remove(key);
        }
		#endregion keybinds
		#region mousebinds
		public static Dictionary<string, List<Mouse>> Mousebinds = new Dictionary<string, List<Mouse>>();
        /// <summary>
        /// Add mousebinds by name.
        /// </summary>
        public static void AddMousebind(string name, params Mouse[] mice) {
            if (!Mousebinds.ContainsKey(name))
                Mousebinds[name] = new List<Mouse>();

            foreach (Mouse mouse in mice)
                Mousebinds[name].Add(mouse);
        }
        /// <summary>
        /// Remove mousebinds by name.
        /// </summary>
        public static void RemoveMousebind(string name, params Mouse[] mice) {
            if (!Mousebinds.ContainsKey(name)) return;

            foreach (Mouse mouse in mice)
                Mousebinds[name].Remove(mouse);
        }
		#endregion mousebinds
		#region multibinds
		public static Dictionary<string, List<string[]>> Multibinds = new Dictionary<string, List<string[]>>();
        /// <param name="conditions">[input name] [SPACE] [InputState]</param>
        public static void AddMultibind(string name, params string[] conditions) {
            if (!Multibinds.ContainsKey(name))
                Multibinds[name] = new List<string[]>();

            Multibinds[name].Add(conditions);
        }
		#endregion multibinds

        /// <summary>
        /// Interrogate the state of an input by name.
        /// </summary>
		public static bool InputInState(string input, InputState state) {
            // Check keybinds
            if (Keybinds.ContainsKey(input))
                foreach (Key key in Keybinds[input])
                    switch (state) {
                        case InputState.JustPressed:
                            if (Game.Instance.GetKey(key).Pressed)
                                return true;
                            break;
                        case InputState.JustReleased:
                            if (Game.Instance.GetKey(key).Released)
                                return true;
                            break;
                        case InputState.Held:
                            if (Game.Instance.GetKey(key).Down)
                                return true;
                            break;
                        case InputState.Up:
                            if (Game.Instance.GetKey(key).Up)
                                return true;
                            break;
                    }
            // Check mousebinds
            if (Mousebinds.ContainsKey(input))
                foreach (Mouse mouse in Mousebinds[input])
                    switch (state) {
                        case InputState.JustPressed:
                            if (Game.Instance.GetMouse(mouse).Pressed)
                                return true;
                            break;
                        case InputState.JustReleased:
                            if (Game.Instance.GetMouse(mouse).Released)
                                return true;
                            break;
                        case InputState.Held:
                            if (Game.Instance.GetMouse(mouse).Down)
                                return true;
                            break;
                        case InputState.Up:
                            if (Game.Instance.GetMouse(mouse).Up)
                                return true;
                            break;
                    }

            // Check special binds
            if (Multibinds.ContainsKey(input))
                foreach (string[] binds in Multibinds[input]) {
                    foreach (string condition in binds) {
                        string[] elements = condition.Split(' ');
                        InputState requiredState = (InputState)Enum.Parse(typeof(InputState), elements[1]);

                        if (InputInState(elements[0], requiredState)) continue;
                        else goto trynext;
                    }
                    // If the loop finishes, the bind is a match
                    return true;
                    // Otherwise, try the next bind
                    trynext:;
                }

            return false;
        }

        /// <summary>
        /// [TODO] Save config
        /// </summary>
        public static void SaveConfig() {
            CreateFilesIfNoneExist();


        }

        /// <summary>
        /// Load config and keybindings from file.
        /// </summary>
        public static void LoadConfig() {
            CreateFilesIfNoneExist();

            string[] iniScreen = File.ReadAllLines(iniScreenPath);
            if (!int.TryParse(iniScreen[1], out int _ScreenWidth))
                Debug.RaiseError("Couldn't parse Screen Width");
            else ScreenWidth = _ScreenWidth;
            if (!int.TryParse(iniScreen[2], out int _ScreenHeight))
                Debug.RaiseError("Couldn't parse Screen Height");
            else ScreenHeight = _ScreenHeight;
            if (!int.TryParse(iniScreen[3], out int _ScreenScale))
                Debug.RaiseError("Couldn't parse Screen Scale");
            else ScreenScale = _ScreenScale;
            if (!int.TryParse(iniScreen[4], out int _FPSLock))
                Debug.RaiseError("Couldn't parse FPS lock");
            else FPSLock = _FPSLock;
            iniScreen = null;

            string[] iniKeybinds = File.ReadAllLines(iniKeybindsPath);
            for (int i = 1; i < iniKeybinds.Length; i++) {
                string current = iniKeybinds[i];
                string[] elements = current.Split('|');
                AddKeybind(elements[0], (Key)Enum.Parse(typeof(Key), elements[1]));
            }
            iniKeybinds = null;

            string[] iniMousebinds = File.ReadAllLines(iniMousebindsPath);
            for (int i = 0; i < iniMousebinds.Length; i++) {
                string current = iniMousebinds[i];
                string[] elements = current.Split('|');
                AddMousebind(elements[0], (Mouse)Enum.Parse(typeof(Mouse), elements[1]));
            }
            iniMousebinds = null;

            string[] iniMultibinds = File.ReadAllLines(iniMultibindsPath);
            for(int i = 0; i < iniMultibinds.Length; i++) {
                string current = iniMultibinds[i];
                string[] elements = current.Split('|');
                AddMultibind(elements[0], elements[1].Split(','));
            }
            iniMultibinds = null;
        }

        /// <summary>
        /// Create default configs and folder structure if none exist.
        /// </summary>
        static void CreateFilesIfNoneExist() {
            Directory.CreateDirectory(DataPath);
            Directory.CreateDirectory(ConfigPath);
            Directory.CreateDirectory(AssetsPath);

            if (!File.Exists(iniScreenPath))
                File.WriteAllLines(iniScreenPath, iniScreenDefault);
            if (!File.Exists(iniKeybindsPath))
                File.WriteAllLines(iniKeybindsPath, iniKeybindsDefault);
            if (!File.Exists(iniMousebindsPath))
                File.WriteAllLines(iniMousebindsPath, iniMousebindsDefault);
            if (!File.Exists(iniMultibindsPath))
                File.WriteAllLines(iniMultibindsPath, iniMultibindsDefault);
        }
    }
}
