using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hidden
{
    class HiddenMod : MonoBehaviour
    {
        private static HiddenMod Instance;
        private List<GameObject> _notes = new List<GameObject>();
        public static void OnLoad()
        {
            if (Instance != null)
            {
                Instance.Awake();
                return;
            }
            if (GameObject.FindObjectOfType<HiddenMod>() != null)
            {
                return;
            }
            new GameObject("hiddenmod").AddComponent<HiddenMod>();
        }

        private void Awake()
        {
            // Get notes
            //var notes = ObjectPool.GetSpawned(FindObjectsOfType<GameObject>().FirstOrDefault(x => x.name == "GameNote(Clone)"), _notes, true);
            try
            {
                var noteForType = FindObjectsOfType<GameObject>().FirstOrDefault(x => x.name == "GameNote(Clone)");
                var notes = ObjectPool.GetSpawned(noteForType, _notes, true);
                Console.WriteLine(notes.Count);
            }
            catch (Exception)
            {
                Console.WriteLine("Hidden: failed");
            }
            Instance = this;
        }


        private float _notesScale = 500f;
    }
}