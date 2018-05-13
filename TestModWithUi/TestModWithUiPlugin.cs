using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VersionChecker.Interfaces;

namespace TestModWithUi
{
    public class TestModWithUiPlugin : BSModUI.ModGui, IVerCheckPlugin
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

        public string Name { get; }
        public string Version { get; }
        public string GithubAuthor { get; }
        public string GithubProjName { get; }
        public string[] Filter { get; }
    }
}
