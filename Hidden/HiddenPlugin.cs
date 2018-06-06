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

        public void OnLevelWasLoaded(int level) {
            //
        }

        public void OnLevelWasInitialized(int level) {
            //
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

<<<<<<< HEAD
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
=======
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
            
>>>>>>> c23fec4175211ae0913996f00342624f74616c4c
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
<<<<<<< HEAD
=======

>>>>>>> c23fec4175211ae0913996f00342624f74616c4c
        }

        public void OnUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }
    }
}
