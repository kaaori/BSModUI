using IllusionPlugin;

namespace BSModUI.Updater.Interfaces
{
    public interface IGithubInfoPlugin : IEnhancedPlugin, IPluginNew
    {
        /// <summary>
        /// The Github Author
        /// </summary>
        string Author { get; }
        /// <summary>
        /// The Github Project Name
        /// </summary>
        string GithubProjName { get; }
    }
}