using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllusionPlugin;
using UnityEngine.SceneManagement;

namespace Hidden
{
    public class Hidden : IEnhancedPlugin
    {

        public string Name => "Hidden Plugin";

        public string Version => "0.0.1";

        public string[] Filter { get; }

        private List<GameNoteController> _notes;
        private BaseNoteVisuals _vis;
        private GameplayManager _gameplayManager;
        private SongController _songController;
        private SongObjectSpawnController _spawnController;

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
            _gameplayManager = UnityEngine.Object.FindObjectOfType<GameplayManager>();
            _notes = UnityEngine.Object.FindObjectsOfType<GameNoteController>().ToList();
            _spawnController = UnityEngine.Object.FindObjectOfType<SongObjectSpawnController>();
        }

        public void OnLevelWasLoaded(int level)
        {
            
        }

        public void OnLevelWasInitialized(int level)
        {
            
        }

        public void OnUpdate()
        {
           
        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {
        }

    }
}
