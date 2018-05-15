using System.Collections.Generic;
using System.Linq;
using BSModUI.Interfaces;
using BSModUI.Misc;
using BSModUI.UI;
using BSModUI.Updater;
using BSModUI.Updater.Interfaces;
using BSModUI.Updater.Misc;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRUI;
using Logger = IllusionPlugin.Logger;

namespace BSModUI
{
    public class ModMenuPlugin : IModGui
    {
        public string Name => "Beat Saber Mod UI";

        public string Version => "0.0.1";

        public string Author => "kaaori";

        public string GithubProjName => "BSModUI";

        public string Image => "Nothing Yet";

        public bool IsEnabled => false;

        public string[] Filter { get; }

        private IEnumerable<IModGui> Plugins => VersionChecker.Instance.Plugins;

        internal static Logger debugLogger = null;

        public void OnApplicationStart()
        {
        }
        
        public void OnApplicationQuit()
        {
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) {
                if (debugLogger == null) debugLogger = this.GetLogger();
                debugLogger.Log("Main menu active");
                ModMenuUi.OnLoad();
                VersionChecker.OnLoad();
            }
        }

        public void OnSceneUnloaded(Scene scene) {
        }

        public void OnActiveSceneChanged(Scene prev, Scene next)
        {
        }

        public void OnUpdate()
        {
            // Debug util
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                SceneDumper.DumpScene();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
            }
        }

        public void OnFixedUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }

    }
}