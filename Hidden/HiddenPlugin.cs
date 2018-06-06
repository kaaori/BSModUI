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

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
            //if (scene.buildIndex > 1)
            //{
            //    HiddenMod.OnLoad();
            //    Console.WriteLine("Level with song loaded");
            //}
        }

        public void OnLevelWasLoaded(int level)
        {
            if (level > 1)
            {
                HiddenMod.OnLoad();
                Console.WriteLine("Level with song loaded");
            }
        }

        public void OnLevelWasInitialized(int level)
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
