using System.Collections.Generic;
using System.Linq;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRUI;

namespace BSModUI
{
    public class ModMenuPlugin : IEnhancedPlugin
    {
        public string Name => "Beat Saber Mod UI";

        public string Version => "0.0.1";

        public string[] Filter { get; }

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ModMenuUi.OnLoad();
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
            if(Input.GetKeyDown(KeyCode.Insert))
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