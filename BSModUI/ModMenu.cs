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
        static RectTransform _rightPos;
        static VRUIViewController _rightScreen;
        static ModMenu _instance;

        private MainMenuViewController _mainMenuViewController;
        private ModMenuViewController _modMenuController;

        //private SongListViewController _songlist;

        private Button _cogWheelButton;
        private Button _backArrow;

        public static void OnLoad()
        {
            if (ModMenu._instance != null)
            {
                return;
            }

            new GameObject("modmenu").AddComponent<ModMenu>();
            Utils.Log("Modmenu GameObj instanced");
        }

        void Awake()
        {
            Utils.Log("Mod Menu Awake");   
            
            ModMenu._instance = this;
            _mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().FirstOrDefault();

            // Get all buttons and filter to just settings button
            var buttons = UnityEngine.Object.FindObjectsOfType<Button>().ToList();
            _cogWheelButton = buttons.FirstOrDefault(x => x.name == "SettingsButton");

            // Get all buttons and filter to just back arrow modal button
            var backarrows = Resources.FindObjectsOfTypeAll<Button>();
            _backArrow = backarrows.FirstOrDefault(x => x.name == "BackArrowButton");

            // Get song list (For the saver plugin? Unused?)
            // _songlist = Resources.FindObjectsOfTypeAll<SongListViewController>().FirstOrDefault();

            AddModMenuButton();
        }

        private void AddModMenuButton()
        {
            Utils.Log("Default button added");
            _mainMenuViewController = FindObjectOfType<MainMenuViewController>();
            _rightScreen =
                ReflectionUtil.GetPrivateField<VRUIViewController>(_mainMenuViewController,
                    "_releaseInfoViewController");
            _rightPos = _rightScreen.gameObject.transform as RectTransform;

            var modmenubutton = CreateButton(_rightPos);
            if (modmenubutton == null)
            {
                Utils.Log("Mod menu button returned as null", Utils.Severity.Error);
                return;
            }
            try
            {
                // (modmenubutton.transform as RectTransform).anchoredPosition = new Vector2(30f, 7f);
                // (modmenubutton.transform as RectTransform).sizeDelta = new Vector2(28f, 10f);

                // Change button text and add listener
                modmenubutton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Mod Menu";
                modmenubutton.onClick.AddListener(delegate()
                {
                    try
                    {
                        Utils.Log("Mod menu pressed");
                        if (_modMenuController == null)
                        {
                            _modMenuController = CreateViewController();
                        }
                        _rightScreen.PresentModalViewController(_modMenuController, null);
                    }
                    catch (Exception ex)
                    {
                        Utils.Log(ex.Message, Utils.Severity.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                Utils.Log(ex.Message, Utils.Severity.Error);
            }
        }

        public ModMenuViewController CreateViewController()
        {
            var vc = new GameObject().AddComponent<ModMenuViewController>();

            vc.rectTransform.anchorMin = new Vector2(0f, 0f);
            vc.rectTransform.anchorMax = new Vector2(1f, 1f);
            vc.rectTransform.sizeDelta = new Vector2(0f, 0f);
            vc.rectTransform.anchoredPosition = new Vector2(0f, 0f);

            return vc;
        }

        public Button CreateButton(RectTransform parent)
        {
            try
            {
                if (_cogWheelButton == null)
                {
                    Utils.Log("Failed to create button", Utils.Severity.Error);
                    return null;
                }

                // Create temporary button to return as new button
                var tmp = Instantiate(_cogWheelButton, parent, false);
                DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
                tmp.onClick = new Button.ButtonClickedEvent();
                return tmp;
            }
            catch (Exception ex)
            {
                Utils.Log(ex.Message, Utils.Severity.Error);
                return null;
            }
        }

        public Button CreateBackButton(RectTransform parent)
        {
            if (_backArrow == null)
            {
                Console.WriteLine("Failed to create back button ");
                return null;
            }

            var tmp = Instantiate(_backArrow, parent, false);
            DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
            tmp.onClick = new Button.ButtonClickedEvent();
            return tmp;
        }

        /*  public SongListViewController CreateList(RectTransform parent)
          {
              if(songlist == null)
              {
                  Console.WriteLine("Failed to create list");
                  return null;
              }
              SongListViewController tmp = Instantiate(songlist, parent, false);
              return tmp;
          } */
    }
}