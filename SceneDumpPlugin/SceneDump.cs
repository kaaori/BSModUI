using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IllusionPlugin;
using UnityEngine;

namespace SceneDumpPlugin
{
    public class SceneDump : IEnhancedPlugin
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.F5))
            {
                SceneDumper.DumpScene();
            }
        }

        public void OnFixedUpdate()
        {

        }

        public string Name => "SceneDumper";
        public string Version => "1.0.0";
        public void OnLateUpdate()
        {

        }

        public string[] Filter { get; }
    }
    public static class SceneDumper
    {
        public static void DumpScene()
        {
            string filename = Application.dataPath + "/unity-scene.txt";

            var gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    DumpGameObject(gameObject, writer, "");
                }
            }
        }

        private static void DumpGameObject(GameObject gameObject, StreamWriter writer, string indent)
        {
            writer.WriteLine("{0}+{1}", indent, gameObject.name);

            foreach (Component component in gameObject.GetComponents<Component>())
            {
                DumpComponent(component, writer, indent + "  ");
            }

            foreach (Transform child in gameObject.transform)
            {
                DumpGameObject(child.gameObject, writer, indent + "  ");
            }
        }

        private static void DumpComponent(Component component, StreamWriter writer, string indent)
        {
            writer.WriteLine("{0}{1}", indent, (component == null ? "(null)" : component.GetType().Name));
        }
    }
}
