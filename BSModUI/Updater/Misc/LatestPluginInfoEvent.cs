using System;
using UnityEngine.Events;

namespace BSModUI.Updater.Misc
{
    /// <summary>
    /// Just an event used so that LatestPluginInfo can be passed as an argument
    /// </summary>
    [Serializable]
    public class LatestPluginInfoEvent : UnityEvent<LatestPluginInfo>
    {

    }
}