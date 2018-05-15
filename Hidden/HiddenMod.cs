using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hidden
{
    class HiddenMod : MonoBehaviour
    {
        private static HiddenMod _instance;

        private List<NoteController> _notes;

        private SongObjectSpawnController _spawnController;

        public static void OnLoad()
        {
            if (_instance != null)
            {
                return;
            }
            new GameObject("HiddenMod").AddComponent<HiddenMod>();
        }

        private void Awake()
        {
            _instance = this;
            _spawnController = FindObjectOfType<SongObjectSpawnController>();

            if (_spawnController == null)
            {
                Console.WriteLine("Spawn controller was null");
                return;
            }

            // Get notes
            var gameNotePrefab = ReflectionUtil.GetPrivateField<NoteController>(_spawnController, "_gameNotePrefab");
            _notes = gameNotePrefab.GetSpawned<NoteController>();

            if (_notes.Count > 0)
            {
                Console.WriteLine("notes found");
            }
            else
            {
                Console.WriteLine("Notes not found");
                _notes = UnityEngine.Object.FindObjectsOfType<NoteController>().ToList();
                if (_notes.Count > 0)
                {
                    Console.WriteLine("Notes found with Object.FindObj");
                }
                else
                {
                    Console.WriteLine("Notes not found with Object.FindObj");
                }
            }
        }
    }
}
