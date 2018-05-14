using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSModUI;
using BSModUI.Interfaces;
using VRUI;

namespace TestModWithUi
{
    public class TestModWithUiPlugin : IModGui
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
        
        public void OnLateUpdate() {
        }

        public string Name => "Beat Saber Mod Test";

        public string Version => "0.0.1";
        public string Author => "Nothing Yet";
        public string Image => "Nothing Yet";
        public string GithubProjName => "";

        public bool IsEnabled => true;
        

        public string[] Filter { get; }
        
    }
}
