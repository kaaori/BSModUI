using System.Collections.Generic;
using System.Linq;
using VersionChecker.Interfaces;
using VersionChecker.Misc;
using UnityEngine;

namespace VersionChecker {
    public class Plugin : IVerCheckPlugin {

        public string Name => "Version Checker";
        public string Version => "1.2";
        public string GithubAuthor => "artman41";
        public string GithubProjName => "";
        public string[] Filter { get; }

        /// <summary>
        /// Event to be called when the info of a plugin has been set
        /// </summary>
        private LatestPluginInfoEvent OnInfoSet;
        
        /// <summary>
        /// A list of the loaded plugins which implement IVerCheckPlugin
        /// </summary>
        private IEnumerable<IVerCheckPlugin> Plugins => IllusionInjector.PluginManager.Plugins.Where(o => o is IVerCheckPlugin).Cast<IVerCheckPlugin>(); //the plugins that can be updated

        /// <summary>
        /// A list of the version info about each plugin
        /// </summary>
        public static readonly List<LatestPluginInfo> PluginInfos = new List<LatestPluginInfo>(); //list of all the plugins iterated through

        /// <summary>
        /// A gameobject used when calling coroutine methods
        /// only used so that the none-static monobehaviour methods can be called.
        /// </summary>
        private VCInterop _versionCheckerInterop;

        public void OnLevelWasLoaded(int level) {
            if (level == 1) { //if we're in the menu
                Init(); //run the init
            }
        }

        void Init() {
            if (OnInfoSet == null) {
                OnInfoSet = new LatestPluginInfoEvent(); //initializes the event
                OnInfoSet.AddListener(OnSet); //adds OnSet as the listener
            }
            if (_versionCheckerInterop == null) {
                var x = new GameObject(); //creates the interop object if it doesn't exist so we can use it
                _versionCheckerInterop = x.AddComponent<VCInterop>();
            }

            if (PluginInfos.Count != 0) return; //if the array contains some objects we must have already ran this
            foreach (var plugin in Plugins) { //get the plugins which implement IVerCheckPlugins
                //Don't worry about this, it handles all the retrieval on creation and calls OnInfoSet when the version has been set
                new LatestPluginInfo(plugin, _versionCheckerInterop, OnInfoSet);
            }
        }

        /// <summary>
        /// Called when a plugin's version info has been set
        /// </summary>
        /// <param name="info">the latest version info</param>
        private void OnSet(LatestPluginInfo info) {
            Log($"{info.Plugin.Name} is{(info.IsLatestVersion ? " " : " not ")}up to date");
            PluginInfos.Add(info); //adds it to the local array of Plugins
        }
        
        public void OnApplicationStart() {
            //
        }

        public void OnApplicationQuit() {
            //
        }

        public void OnLevelWasInitialized(int level) {
            //
        }

        public void OnUpdate() {
            //
        }
        
        public void OnFixedUpdate() {
            //
        }

        public void OnLateUpdate() {
            //
        }

        void Log(string s) {
            Debug.Log($"[{Name}] {s}");
        }
        
    }
}