using UnityEngine.SceneManagement;
using VersionChecker.Interfaces;
using Object = UnityEngine.Object;

namespace SuddenDeath
{
    public class SuddenDeath : IVerCheckPlugin
    {
        public string Name => "Sudden Death Plugin";
        public string Version => "0.0.1";

        public string GithubAuthor => "";
        public string GithubProjName => "";
        
        public string[] Filter { get; }

        private GameEnergyCounter _energyCounter;
        private GameplayManager _gameplayManager;
        private GameSongController _songController;
        private float _oldEnergy = 0f; 

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
            _energyCounter = UnityEngine.Object.FindObjectOfType<GameEnergyCounter>();
            _songController = UnityEngine.Object.FindObjectOfType<GameSongController>();
            _gameplayManager = UnityEngine.Object.FindObjectOfType<GameplayManager>();
            if (_energyCounter != null)
            {
                _oldEnergy = _energyCounter.energy;
            }
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
            if (_energyCounter == null) return;

            if (_gameplayManager.gameState != GameplayManager.GameState.Failed && _energyCounter.energy < _oldEnergy)
            {
                _gameplayManager.HandleGameEnergyDidReach0();
                //_songController.FailStopSong();
                return;
            }
            _oldEnergy = _energyCounter.energy;
            
        }

    }
}
