using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRUI;

namespace BSModUI
{
    class ModMenu : MonoBehaviour
    {
        public static ModMenu Instance;
        private MainMenuViewController _mainMenuViewController;
        private VRUIScreen _curRightScreen;
        private Vector3 zeroVector;

        private List<Button> _buttons;

        public static void OnLoad()
        {
            if (Instance != null)
            {
                return;
            }
            new GameObject("Mod Menu").AddComponent<ModMenu>();
        }

        private void Awake()
        {
            Instance = this;
            _mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().FirstOrDefault();
            _buttons = UnityEngine.Object.FindObjectsOfType<Button>().ToList();

            if (_mainMenuViewController == null)
            {
                Console.WriteLine("Could not find menu view controller");
            }
            else
            {
                SetupModMenuScreen();
                var modMenuButton = AddButton("ModMenuButton", "Mods", ModMenuBtnOnClick);
                var test2Button = AddButton("TestButton", "Test", TestOnClick, new Vector3(1f,0f,0f));
            }
        }

        private Button AddButton(string buttonName, string buttonText, UnityAction call)
        {
            // Copy tutorial button to new instance
            var settingsButton = _buttons.FirstOrDefault(x => x.name.ToLowerInvariant() == "settingsbutton");
            var newButton = Instantiate(settingsButton, _curRightScreen.transform);
            newButton.name = buttonName;

            // Destroy tutorial event handler on new button
            var eventOnClick = newButton.GetComponent<GameEventOnUIButtonClick>();
            Destroy(eventOnClick);

            // Change text and set up listener
            newButton.GetComponentInChildren<TMP_Text>().text = buttonText;
            newButton.onClick = new Button.ButtonClickedEvent();
            newButton.onClick.AddListener(call);
            return newButton;
        }

        // Overload to allow for button transform offsetting
        private Button AddButton(string buttonName, string buttonText, UnityAction call, Vector3 offset)
        {
            var newButton = AddButton(buttonName, buttonText, call);
            var pos = newButton.transform.position;
            pos -= offset;
            newButton.transform.position = pos;
            return newButton;
        }

        private void TestOnClick()
        {
            Console.WriteLine("Wow it did a thing");
        }

        private void ModMenuBtnOnClick()
        {
            // TODO: Actually make this open/unhide a menu & any implemented buttons.
            Console.WriteLine("Click handled");
        }

        private void SetupModMenuScreen()
        {
            Console.WriteLine("Screen system found, Right screen: " + _mainMenuViewController.screen.screenSystem.rightScreen.name);
            _curRightScreen = _mainMenuViewController.screen.screenSystem.rightScreen;
        }

        private void OnLateUpdate()
        {
        }
    }
}
