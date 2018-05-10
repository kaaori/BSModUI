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
    /**
     * ModUI Framework implementation ideas:
     *
     * - Mod references BSModUI.dll
     * - Mod inherits ModGui baseclass
     * - Mod 
     *
     *
     */

    class ModMenuViewController : VRUIViewController
    {
        private ModMenu _modMenu = FindObjectOfType<ModMenu>();

        private TextMeshProUGUI _loadingText;
        private TextMeshProUGUI _pageText;

        private Button _backButton;

        private Button _prevPage;
        private Button _nextPage;

        //private SongListViewController _songList;

        public ModMenuViewController(/*SongListViewController songList*/)
        {

            //this._songList = songList;
        }

        protected override void DidActivate()
        {
            // Find ModMenu object and create back button on screen
            _backButton = _modMenu.CreateBackButton(rectTransform);

            if (_backButton == null)
            {
                Utils.Log("Back button was null, did not activate", Utils.Severity.Warning);
            }
            else
            {
                Utils.Log("View Controller activated");

                // Direct casts for MaXiMuM safety
                ((RectTransform) _backButton.transform).anchorMin = new Vector2(0, 0);
                ((RectTransform) _backButton.transform).anchorMin = new Vector2(0, 0);

                // Back to release info
                _backButton.onClick.AddListener(delegate ()
                {
                    DismissModalViewController(null);
                });

                /*songList = modmenu.CreateList(rectTransform);
                (songList.transform as RectTransform).anchoredPosition = new Vector2(0.5f, 0.5f); */
            }
        }

        // ReSharper disable once InconsistentNaming
        private TextMeshProUGUI CreateTMPText(string text, Vector2 position)
        {
            var textMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            textMesh.rectTransform.SetParent(rectTransform, false);
            textMesh.text = text;
            textMesh.fontSize = 4;
            //textMesh.color = new Color(1f, 0.09f, 0.353f);
            textMesh.color = Color.white;
            textMesh.font = Resources.Load<TMP_FontAsset>("Teko-Medium SDF No Glow");
            textMesh.rectTransform.anchorMin = new Vector2(0.5f, 1f);
            textMesh.rectTransform.anchorMax = new Vector2(1f, 1f);
            textMesh.rectTransform.sizeDelta = new Vector2(60f, 10f);
            textMesh.rectTransform.anchoredPosition = position;

            return textMesh;
        }
    }
}
