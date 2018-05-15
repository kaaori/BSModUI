using BSModUI.Interfaces;
using IllusionPlugin;
using UnityEngine.SceneManagement;

namespace SuddenDeath
{
    public class SuddenDeath : IModGui
    {
        public string Name => "Sudden Death Plugin";
        public string Author { get; }
        public string Version => "0.0.1";

        public string GithubProjName { get; }
        
        public string Image { get; }
        public bool IsEnabled { get; }

        public string[] Filter { get; }

        private GameEnergyCounter _energyCounter;
        private GameplayManager _gameplayManager;
        private GameSongController _songController;
        private float _oldEnergy = 0f; 

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

        public void OnActiveSceneChanged(Scene arg0, Scene scene)
        {
            _energyCounter = UnityEngine.Object.FindObjectOfType<GameEnergyCounter>();
            _songController = UnityEngine.Object.FindObjectOfType<GameSongController>();
            _gameplayManager = UnityEngine.Object.FindObjectOfType<GameplayManager>();
            if (_energyCounter != null)
            {
                _oldEnergy = _energyCounter.energy;
            }
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
