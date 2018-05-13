using VersionChecker.Interfaces;
using VersionChecker.Misc.Github;

namespace VersionChecker.Misc {
    /// <summary>
    /// Stores the latest update info about a plugin
    /// </summary>
    public struct LatestPluginInfo {
        /// <summary>
        /// the plugin in question
        /// </summary>
        public IVerCheckPlugin Plugin { get; set; }

        /// <summary>
        /// bool as to whether the local version and github version match
        /// </summary>
        public bool IsLatestVersion { get; set; }

        /// <summary>
        /// used to determine whether the version has been set.
        /// </summary>
        public bool HasVersionBeenSet { get; private set; }

        /// <summary>
        /// the latest version on github
        /// </summary>
        public string LatestVersion { get; private set; }

        /// <summary>
        /// the unity event to call
        /// </summary>
        private LatestPluginInfoEvent _ue;

        /// <summary>
        /// The retrieved github release page
        /// </summary>
        public GithubReleasePage ReleasePage;

        /// <summary>
        /// Created a base oject then calls the GetGithubJson coroutine using the VCInterop,
        /// calls the LatestPluginInfoEvent once the data has been retrieved
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="interop"></param>
        /// <param name="vCheck"></param>
        public LatestPluginInfo(IVerCheckPlugin plugin, VCInterop interop, LatestPluginInfoEvent vCheck) {
            Plugin = plugin;
            IsLatestVersion = false;
            HasVersionBeenSet = false;
            _ue = vCheck;
            LatestVersion = string.Empty;
            ReleasePage = null;
            interop.StartCoroutine(Util.GetGithubJson(interop, Plugin,
                SetLatestVersion)); //starts a coroutine to retrieve the latest plugin version
        }

        /// <summary>
        /// Gets the latest version of the plugin from github
        /// </summary>
        /// <param name="o">the data returned by the github api</param>
        private void SetLatestVersion(GithubReleasePage o) {
            if (o.tag_name != null) { //if the page exists
                ReleasePage = o; //sets the local page to the page returned incase the data needs to be used elsewhere
                LatestVersion = ReleasePage.tag_name.StartsWith("v")
                    ? ReleasePage.tag_name.Substring(1)
                    : ReleasePage.tag_name; //sets the latest version = the string retrieved from github
                IsLatestVersion = LatestVersion == Plugin.Version; //checks the latest version against the local version
            }
            else { //if there is no github page
                IsLatestVersion = true;
            }

            HasVersionBeenSet = true;
            _ue.Invoke(this); //invokes the event referencing the current object
        }
    }
}