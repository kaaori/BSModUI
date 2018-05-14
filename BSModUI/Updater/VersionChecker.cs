using System.Collections.Generic;
using System.Linq;
using BSModUI.Interfaces;
using BSModUI.Misc;
using BSModUI.UI;
using BSModUI.Updater.Misc;
using UnityEngine;

namespace BSModUI.Updater
{
    public class VersionChecker : MonoBehaviour {

        /// <summary>
        /// Event to be called when the info of a plugin has been set
        /// Used internally to trigger an add to the list.
        /// </summary>
        private LatestPluginInfoEvent _onInfoSet;

        /// <summary>
        /// Triggers whenever a plugin is added to the list of infos.
        /// </summary>
        public LatestPluginInfoEvent OnPluginInfoAdded;

        /// <summary>
        /// A list of the loaded plugins which implement IGithubInfoPlugin
        /// </summary>
        internal IEnumerable<IModGui> Plugins => IllusionInjector.PluginManager.Plugins.Where(o => o is IModGui).Cast<IModGui>(); //the plugins that can be updated

        /// <summary>
        /// A list of the version info about each plugin
        /// </summary>
        public readonly List<LatestPluginInfo> PluginInfos = new List<LatestPluginInfo>(); //list of all the plugins iterated through

        /// <summary>
        /// A gameobject used when calling coroutine methods
        /// only used so that the none-static monobehaviour methods can be called.
        /// </summary>
        private VcInterop _versionCheckerInterop;

        internal static VersionChecker Instance;

        /// <summary>
        /// Call this in the OnLevelWasLoaded or we get issues
        /// </summary>
        public static void OnLoad() {
            if (Instance != null) return;
            ModMenuUi._instance.gameObject.AddComponent<VersionChecker>();
        }
        
        void Awake()
        {
            Instance = this;

            Init();

            if (PluginInfos.Count != 0) return; //if the array contains some objects we must have already ran this
                                                
            //get the plugins which implement IVerCheckPlugins
            //Don't worry about this, it handles all the retrieval on creation and calls OnInfoSet when the version has been set
            Plugins.ForEach(plugin => new LatestPluginInfo(plugin, _versionCheckerInterop, _onInfoSet));
        }

        void Init() {
            if (_versionCheckerInterop == null) {
                _versionCheckerInterop = ModMenuUi._instance.gameObject.AddComponent<VcInterop>();
            }

            if (OnPluginInfoAdded == null) {
                OnPluginInfoAdded = new LatestPluginInfoEvent();
            }

            if (_onInfoSet == null)
            {
                _onInfoSet = new LatestPluginInfoEvent(); //initializes the event
                _onInfoSet.AddListener(OnSet); //adds OnSet as the listener
            }
        }

        /// <summary>
        /// Called when a plugin's version info has been set
        /// </summary>
        /// <param name="info">the latest version info</param>
        private void OnSet(LatestPluginInfo info) {
            Utils.Log($"{info.Plugin.Name} is {(info.IsLatestVersion ? " " : " not ")} up to date");
            PluginInfos.Add(info); //adds it to the local array of Plugins
            OnPluginInfoAdded?.Invoke(info);
        }

    }
}