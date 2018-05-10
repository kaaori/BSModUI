using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRUI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BSModUI
{
    class ModMenuViewController : VRUIViewController
    {
        Button backbutton;
        ModMenu modmenu;
        SongListViewController songlist;
        protected override void DidActivate()
        {
            modmenu = FindObjectOfType<ModMenu>();
            Console.WriteLine("CONTROLLER ACTIVATED!!!!!!");
            backbutton = modmenu.CreateBackButton(rectTransform);
            (backbutton.transform as RectTransform).anchorMin = new Vector2(0, 0);
            (backbutton.transform as RectTransform).anchorMin = new Vector2(0, 0);
            backbutton.onClick.AddListener(delegate ()
            {
                DismissModalViewController(null,false);
            });
            /*songlist = modmenu.CreateList(rectTransform);
            (songlist.transform as RectTransform).anchoredPosition = new Vector2(0.5f, 0.5f); */
        }
    }
}
