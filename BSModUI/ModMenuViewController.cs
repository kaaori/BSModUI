using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HMUI;
using IllusionInjector;
using IllusionPlugin;
using VRUI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Toggle = UnityEngine.UI.Toggle;

namespace BSModUI
{
    /**
     * ModUI Framework implementation ideas:
     *
     * - Mod references BSModUI.dll
     * - Inherits ModGui baseclass
     * - Calls Setup UI base function with name and ver
     * - Toggle is added by default with override for custom page
     * - 
     */

    struct ModSelection
    {
        public Toggle Toggle;
        public TextMeshProUGUI Text;
        public string PrettyName;

        public ModSelection(Toggle toggle, TextMeshProUGUI text, string prettyName)
        {
            Toggle = toggle;
            Text = text;
            PrettyName = prettyName;
        }
    }

    struct Mod
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        // TODO: prop for Image
    }

    class ModMenuViewController : VRUIViewController, TableView.IDataSource
    {
        private ModMenuUi _modMenuUi = FindObjectOfType<ModMenuUi>();

        private TextMeshProUGUI _pageText;

        private Button _backToMenuButton;

        private Button _pageUpButton;
        private Button _pageDownButton;

        private List<ModSelection> _modSelections = new List<ModSelection>();

        private List<Mod> _mods = new List<Mod>();

        private TableView _modsTableView;
        SongListTableCell _songListTableCellInstance;

        private int _currentPage = 0;
        private int _modsPerPage = 12;
        private bool _isLoading;

        private ModMenuViewController _modList;

        protected override void DidActivate()
        {
            // Find ModMenuUi object and create back button on screen

            _modMenuUi = FindObjectOfType<ModMenuUi>();
            try
            {
                _backToMenuButton = _modMenuUi.CreateButton(rectTransform, "SettingsButton");
                if (_backToMenuButton == null)
                {
                    Utils.Log("Universe collapsing, abort", Utils.Severity.Error);
                    return;
                }

                Utils.Log("View Controller activated");
                try
                {
                    // Direct casts for MaXiMuM safety
                    ((RectTransform)_backToMenuButton.transform).anchorMin = new Vector2(0.5f, 1f);
                    ((RectTransform)_backToMenuButton.transform).anchorMax = new Vector2(0.5f, 1f);
                    ((RectTransform)_backToMenuButton.transform).anchoredPosition = new Vector2(0f, 10f);
                    _backToMenuButton.interactable = true;

                    if (_pageDownButton == null)
                    {
                        _pageDownButton = _modMenuUi.CreateButton(rectTransform, "PageDownButton");

                        ((RectTransform)_pageDownButton.transform).anchorMin = new Vector2(0.5f, 0f);
                        ((RectTransform)_pageDownButton.transform).anchorMax = new Vector2(0.5f, 0f);
                        ((RectTransform)_pageDownButton.transform).anchoredPosition = new Vector2(0f, 10f);
                        _pageDownButton.interactable = true;
                        _pageDownButton.onClick.AddListener(delegate ()
                        {
                            LoadPage(--_currentPage);
                            Utils.Log("Page down pressed");
                        });
                    }
                    if (_pageUpButton == null)
                    {
                        _pageUpButton = _modMenuUi.CreateButton(rectTransform, "PageUpButton");
                        ((RectTransform)_pageUpButton.transform).anchorMin = new Vector2(0.5f, 1f);
                        ((RectTransform)_pageUpButton.transform).anchorMax = new Vector2(0.5f, 1f);
                        ((RectTransform)_pageUpButton.transform).anchoredPosition = new Vector2(0f, -10f);
                        _pageUpButton.interactable = true;
                        _pageUpButton.onClick.AddListener(delegate ()
                        {
                            if (_currentPage > 0)
                            {
                                LoadPage(++_currentPage);
                                Utils.Log("Page up pressed");
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
                }

                // Back to release info and cleanup
                _backToMenuButton.onClick.AddListener(delegate
                {
                    DismissModalViewController(null);
                });
                _songListTableCellInstance = Resources.FindObjectsOfTypeAll<SongListTableCell>().First(x => (x.name == "SongListTableCell"));

                LoadMods();
                base.DidActivate();
            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
            }
        }

        private void LoadPage(int page)
        {
            throw new NotImplementedException();
        }

        private void LoadMods()
        {
            Utils.Log("Loading mods");
            try
            {
                _mods = GetModsFromIPA();
                if (_mods.Count == 0)
                {
                    Utils.Log("No mods found");
                }
                foreach (var mod in _mods)
                {
                    Utils.Log(mod.Name);
                }
                if (_pageText == null)
                {
                    _pageText = CreateTMPText("Plugins", new Vector2(-10f, -10f));
                    _pageText.fontSize = 8;
                }
                RefreshScreen();

            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace+ex.Message,Utils.Severity.Error);
            }
        }

        // ReSharper disable once InconsistentNaming
        public List<Mod> GetModsFromIPA()
        {
            var modsList = PluginManager.Plugins.Select(plugin => new Mod
                {
                    Name = plugin.Name,
                    Version = plugin.Version,
                })
                .ToList();
            return modsList;
        }

        public void RefreshScreen()
        {
            if (_modsTableView == null)
            {
                _modsTableView = new GameObject().AddComponent<TableView>();

                _modsTableView.transform.SetParent(rectTransform, false);

                _modsTableView.dataSource = this;

                ((RectTransform) _modsTableView.transform).anchorMin = new Vector2(0.3f, 0.125f);
                ((RectTransform) _modsTableView.transform).anchorMax = new Vector2(0.7f, 0.875f);
                ((RectTransform) _modsTableView.transform).sizeDelta = new Vector2(0f, 0f);
                ((RectTransform) _modsTableView.transform).anchoredPosition = new Vector2(0f, 0f);

                _modsTableView.DidSelectRowEvent += _modsTableView_DidSelectRowEvent;
            }
            else
            {
                _modsTableView.ReloadData();
            }
        }

        private void _modsTableView_DidSelectRowEvent(TableView sender, int row)
        {
            //throw new NotImplementedException();
            Utils.Log("RowSelect Unimplemented");
        }

        protected override void DidDeactivate()
        {
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

        public float RowHeight()
        {
            return 10f;
        }

        public int NumberOfRows()
        {
            return Math.Min(_modsPerPage, _mods.Count);
        }

        public TableCell CellForRow(int row)
        {
            SongListTableCell _tableCell = Instantiate(_songListTableCellInstance);

            ((RectTransform) _tableCell.transform).anchorMin = new Vector2(0f,1f);
            ((RectTransform) _tableCell.transform).anchorMax = new Vector2(0f,1f);
            ((RectTransform) _tableCell.transform).sizeDelta = new Vector2(0f,10f);

            _tableCell.songName = _mods.ElementAtOrDefault(row).Name;
            _tableCell.author = _mods.ElementAtOrDefault(row).Version;

            return _tableCell;
        }
    }
}
