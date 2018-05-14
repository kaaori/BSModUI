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

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Utils.Log("Main menu active");
                ModMenuUi.OnLoad();
                VersionChecker.OnLoad();
            }
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnLevelWasInitialized(int level)
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