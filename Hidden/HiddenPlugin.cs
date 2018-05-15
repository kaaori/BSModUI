using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hidden
{
    public class HiddenPlugin : IEnhancedPlugin
    {
        public void OnFixedUpdate()
        {

        }

        public string Name => "Hidden Plugin";

        public string Version => "0.0.1";

        public string[] Filter { get; }

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
        }

        //https://answers.unity.com/questions/1113318/applicationloadlevelapplicationloadedlevel-obsolet.html
        //buildIndex == loadedLevel
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
            if (scene.buildIndex == 2)
            {
                HiddenMod.OnLoad();
            }
        }

        public void OnSceneUnloaded(Scene scene) {
            //
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) {
            
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {

        }

        public void OnUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }
    }
}
