using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IllusionPlugin;

namespace TestModWithUi
{
    public class TestModWithUiPlugin : BSModUI.ModGui, IPlugin
    {

        public void OnApplicationStart()
        {
        }

        public void OnApplicationQuit()
        {
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

        public string Name { get; }
        public string Version { get; }

    }
}
