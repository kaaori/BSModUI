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
    class ModMenuUi : MonoBehaviour
    {
        static RectTransform _rightPos;
        static VRUIViewController _rightScreen;
        static ModMenuUi _instance;

        public static List<Sprite> Icons = new List<Sprite>();

        private MainMenuViewController _mainMenuViewController;

        // Custom view controller
        private ModMenuViewController _modMenuController;

        private Button _buttonInstance;
        private Button _cogWheelButtonInstance;
        private Button _backButtonInstance;

        //private Button _backArrow;
        //private Button _forwardArrow;

        private Button _upArrowBtn;
        private Button _downArrowBtn;
        private RectTransform _mainMenuRectTransform;

        public static void OnLoad()
        {
            if (ModMenuUi._instance != null)
            {
                return;
            }

            new GameObject("modmenu").AddComponent<ModMenuUi>();
            Utils.Log("Modmenu GameObj instanced");
        }

        void Awake()
        {
            Utils.Log("Mod Menu Awake");   
            
            ModMenuUi._instance = this;
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                //Utils.Log(sprite.name);
                Icons.Add(sprite);
            }

            try
            {
                // Get necessary button instances and main menu VC
                var allButtons = Resources.FindObjectsOfTypeAll<Button>();

                _buttonInstance = Resources.FindObjectsOfTypeAll<Button>().First(x => x.name == "QuitButton");
                _cogWheelButtonInstance = allButtons.FirstOrDefault(x => x.name == "SettingsButton");
                _downArrowBtn = allButtons.First(x => x.name == "PageDownButton");
                _upArrowBtn = allButtons.First(x => x.name == "PageUpButton");

                _mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().First();
                _mainMenuRectTransform = (RectTransform) _buttonInstance.transform.parent;

                Utils.Log("Buttons and main menu found.");
                AddModMenuButton();
                Utils.Log("Mod menu button created");
            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace+ex.Message, Utils.Severity.Error);
            }
        }

        private void AddModMenuButton()
        {
            try
            {
                _rightScreen =
                    ReflectionUtil.GetPrivateField<VRUIViewController>(_mainMenuViewController,
                        "_releaseInfoViewController");
                _rightPos = _rightScreen.gameObject.transform as RectTransform;
                var modMenuButton = CreateButton(_rightPos);
                SetButtonText(ref modMenuButton, "Mod Menu");
                SetButtonIcon(ref modMenuButton, Icons.First(x => x.name == "SettingsIcon"));

                if (modMenuButton == null)
                {
                    Utils.Log("Mod menu button returned as null", Utils.Severity.Error);
                    return;
                }
                // (modmenubutton.transform as RectTransform).anchoredPosition = new Vector2(30f, 7f);
                // (modmenubutton.transform as RectTransform).sizeDelta = new Vector2(28f, 10f);

                // Change button text and add listener
                //modMenuButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Mod Menu";
                modMenuButton.onClick.AddListener(delegate
                {
                    try
                    {
                        Utils.Log("Mod menu pressed");
                        if (_modMenuController == null)
                        {
                            _modMenuController = CreateViewController();
                        }
                        _rightScreen.PresentModalViewController(_modMenuController, null);
                        Utils.Log("Mod menu setup finished");

                    }
                    catch (Exception ex)
                    {
                        Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
                    }
                });
            }

            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
            }
        }

        public ModMenuViewController CreateViewController()
        {
            var vc = new GameObject("CustomViewController").AddComponent<ModMenuViewController>();

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
                if (_cogWheelButtonInstance == null)
                {
                    Utils.Log("Failed to create button", Utils.Severity.Error);
                    return null;
                }

                // Create temporary button to return as new button
                var tmp = Instantiate(_cogWheelButtonInstance, parent, false);
                DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
                tmp.onClick = new Button.ButtonClickedEvent();

                return tmp;
            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
                return null;
            }
        }
        public Button CreateButton(RectTransform parent, string templateButtonName)
        {
            try
            {
                var templateButton = Resources.FindObjectsOfTypeAll<Button>().First(x=>x.name == templateButtonName);
                if (templateButton == null)
                {
                    Utils.Log("Failed to create button from template, invalid name?", Utils.Severity.Error);
                    return null;
                }

                // Create temporary button to return as new button
                var tmp = Instantiate(templateButton, parent, false);
                DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
                tmp.onClick = new Button.ButtonClickedEvent();

                return tmp;
            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
                return null;
            }
        }

        public Button CreateDownButton(RectTransform parent)
        {
            if (_downArrowBtn == null)
            {
                Utils.Log("Failed to create back button", Utils.Severity.Error);
                return null;
            }

            var tmp = Instantiate(_downArrowBtn, parent, false);
            DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
            tmp.onClick = new Button.ButtonClickedEvent();

            return tmp;
        }

        public Button CreateUpButton(RectTransform parent)
        {
            if (_upArrowBtn == null)
            {
                Utils.Log("Failed to create next button", Utils.Severity.Error);
                return null;
            }

            var tmp = Instantiate(_upArrowBtn, parent, false);
            DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
            tmp.onClick = new Button.ButtonClickedEvent();
            return tmp;
        }

        public void SetButtonText(ref Button button, string text)
        {
            if (button.GetComponentInChildren<TextMeshProUGUI>() != null)
            {

                button.GetComponentInChildren<TextMeshProUGUI>().text = text;
            }

        }

        public void SetButtonIcon(ref Button button, Sprite icon)
        {
            if (button.GetComponentsInChildren<UnityEngine.UI.Image>().Count() > 1)
            {

                button.GetComponentsInChildren<UnityEngine.UI.Image>()[1].sprite = icon;
            }

        }

        public void SetButtonBackground(ref Button button, Sprite background)
        {
            if (button.GetComponentsInChildren<Image>().Any())
            {

                button.GetComponentsInChildren<UnityEngine.UI.Image>()[0].sprite = background;
            }

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