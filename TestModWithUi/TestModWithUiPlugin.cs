

using BSModUI.Interfaces;
using UnityEngine.SceneManagement;

namespace TestModWithUi
{
    public class TestModWithUiPlugin : IModGui
    {

        public void OnApplicationStart()
        {
        }

        public void OnApplicationQuit()
        {
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        }

        public void OnSceneUnloaded(Scene scene) {
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) {
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }
        
        public void OnLateUpdate() {
        }

        public string Name => "Beat Saber Mod Test";

        public string Version => "0.0.1";
        public string Author => "Nothing Yet";
        public string Image => "Nothing Yet";
        public string GithubProjName => "";

        public bool IsEnabled => true;
        

        public string[] Filter { get; }
        
    }
}
