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
        static RectTransform rightpos;
        static VRUIViewController rightscreen;
        static ModMenu Instance;
        private MainMenuViewController _mainMenuViewController;
        public ModMenuViewController modmenucontroller;
        private SongListViewController songlist;
         private Button _button;
        public Button backarrow; 
       public static void OnLoad()
        {
            Console.WriteLine("thinking a bit");

            if (ModMenu.Instance != null)
            {
                Console.WriteLine("thinking");

                return;
            }
            new GameObject("modmenu").AddComponent<ModMenu>();
            Console.WriteLine("thinking hard");

        }
        void Awake()
        {
            Console.WriteLine("thinking 2 hard");

            ModMenu.Instance = this;
            _mainMenuViewController = Resources.FindObjectsOfTypeAll<MainMenuViewController>().FirstOrDefault();
            var buttons = UnityEngine.Object.FindObjectsOfType<Button>().ToList();
            _button = buttons.FirstOrDefault(x => x.name.ToLowerInvariant() == "settingsbutton");
           var backarrows = Resources.FindObjectsOfTypeAll<Button>();
           backarrow = backarrows.FirstOrDefault(x => x.name == "BackArrowButton");
            songlist = Resources.FindObjectsOfTypeAll<SongListViewController>().FirstOrDefault();

            addinitbutton();
        }
        private void addinitbutton()
        {
            Console.WriteLine("MEGA THINK");
            
            _mainMenuViewController = FindObjectOfType<MainMenuViewController>();
            rightscreen = ReflectionUtil.GetPrivateField<VRUIViewController>(_mainMenuViewController, "_releaseInfoViewController");
            rightpos = rightscreen.gameObject.transform as RectTransform;
            
            Button modmenubutton = CreateButton(rightpos);
            try
            {
               // (modmenubutton.transform as RectTransform).anchoredPosition = new Vector2(30f, 7f);
               // (modmenubutton.transform as RectTransform).sizeDelta = new Vector2(28f, 10f);

                modmenubutton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Mod Menu";
                modmenubutton.onClick.AddListener(delegate ()
                {
                    try
                    {
                        Console.WriteLine("button press");
                        if (modmenucontroller == null)
                        {

                            modmenucontroller = CreateViewController();

                        }
                        rightscreen.PresentModalViewController(modmenucontroller, null, false);
                    }
                    catch(Exception err)
                    {
                        Console.WriteLine("ERROR: " + err);
                    }
                });
            } catch(Exception err)
            {
                Console.WriteLine("ERROR: " + err);
            }
        }

        public ModMenuViewController CreateViewController()
        {

            ModMenuViewController vc = new GameObject().AddComponent<ModMenuViewController>();

            vc.rectTransform.anchorMin = new Vector2(0f, 0f);
            vc.rectTransform.anchorMax = new Vector2(1f, 1f);
            vc.rectTransform.sizeDelta = new Vector2(0f, 0f);
            vc.rectTransform.anchoredPosition = new Vector2(0f, 0f);

            return vc;
        }
        public Button CreateButton(RectTransform parent)
        {
            if(_button == null)
            {
                Console.WriteLine("Failed to create button ");
                return null;
                
            }
            Button tmp = Instantiate(_button, parent, false);
            DestroyImmediate(tmp.GetComponent<GameEventOnUIButtonClick>());
            tmp.onClick = new Button.ButtonClickedEvent();
            return tmp;
        }
        public Button CreateBackButton(RectTransform parent)
        {
            if(backarrow == null)
            {
                Console.WriteLine("Failed to create back button ");
                return null;
            }
            Button tmp = Instantiate(backarrow, parent, false);
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
