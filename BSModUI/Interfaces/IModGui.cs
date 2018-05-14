using BSModUI.Updater.Interfaces;

namespace BSModUI.Interfaces
{
    public interface IModGui : IGithubInfoPlugin
    {
        string Image { get; }
        // VRUIViewController CustomViewController { get; set; }
        bool IsEnabled { get; }
    }


}
