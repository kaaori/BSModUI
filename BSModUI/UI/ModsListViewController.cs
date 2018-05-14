using System;
using System.Collections.Generic;
using System.Linq;
using BSModUI.Interfaces;
using BSModUI.Misc;
using HMUI;
using IllusionInjector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

namespace BSModUI.UI
{
    class ModsListViewController : VRUIViewController, TableView.IDataSource
    {
        private new ModMenuMasterViewController _parentViewController;

        private ModMenuUi _modMenuUi;

        private Button _pageUpButton;
        private Button _pageDownButton;
        private Button _toggleButton;
        private UnityEngine.UI.Toggle _toggleSwitch;
        private List<ModSelection> _modSelections = new List<ModSelection>();
        private TextMeshProUGUI _compatibilitytext;
        private List<Mod> _mods = new List<Mod>();

        private TableView _modsTableView;
        SongListTableCell _songListTableCellInstance;

        protected override void DidActivate()
        {

            _modMenuUi = FindObjectOfType<ModMenuUi>();
            _parentViewController = transform.parent.GetComponent<ModMenuMasterViewController>();

            try
            {
                _toggleSwitch = Resources.FindObjectsOfTypeAll<UnityEngine.UI.Toggle>().First();
                Utils.Log(_toggleSwitch == null ? "Toggle switch null" : "Toggle switch not null!");
                if (_pageDownButton == null)
                {
                    _pageDownButton = _modMenuUi.CreateButton(rectTransform, "PageDownButton");

                    ((RectTransform)_pageDownButton.transform).anchorMin = new Vector2(0.5f, 0f);
                    ((RectTransform)_pageDownButton.transform).anchorMax = new Vector2(0.5f, 0f);
                    ((RectTransform)_pageDownButton.transform).anchoredPosition = new Vector2(0f, 10f);
                    _pageDownButton.interactable = true;
                    _pageDownButton.onClick.AddListener(delegate ()
                    {
                        _modsTableView.PageScrollDown();
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
                        _modsTableView.PageScrollUp();
                        Utils.Log("Page up pressed");
                    });
                }

            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
            }


            _songListTableCellInstance = Resources.FindObjectsOfTypeAll<SongListTableCell>().First(x => (x.name == "SongListTableCell"));

            LoadMods();

            base.DidActivate();
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

                RefreshScreen();

            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
            }
        }

        // ReSharper disable once InconsistentNaming
        public List<Mod> GetModsFromIPA()
        {
            var modsList = PluginManager.Plugins.Select(plugin => new Mod
            {
                Name = plugin.Name ?? "No name",
                Version = plugin.Version ?? "No version",
                GetPlugin = plugin,
            }).ToList();
            return modsList;
        }

        public void RefreshScreen()
        {
            if (_modsTableView == null)
            {
                _modsTableView = new GameObject().AddComponent<TableView>();

                _modsTableView.transform.SetParent(rectTransform, false);

                _modsTableView.dataSource = this;

                try
                {
                    var viewportMask = Instantiate(Resources.FindObjectsOfTypeAll<UnityEngine.UI.Mask>().First(), _modsTableView.transform, false);

                    _modsTableView.GetComponentsInChildren<RectTransform>().First(x => x.name == "Content").transform.SetParent(viewportMask.rectTransform, false);
                }
                catch
                {
                    Utils.Log("Can't create mask for Content!", Utils.Severity.Warning);
                }

                ((RectTransform)_modsTableView.transform).anchorMin = new Vector2(0f, 0.5f);
                ((RectTransform)_modsTableView.transform).anchorMax = new Vector2(1f, 0.5f);
                ((RectTransform)_modsTableView.transform).sizeDelta = new Vector2(0f, 60f);
                ((RectTransform)_modsTableView.transform).anchoredPosition = new Vector2(0f, 0f);

                _modsTableView.DidSelectRowEvent += _modsTableView_DidSelectRowEvent;

                ReflectionUtil.SetPrivateField(_modsTableView, "_pageUpButton", _pageUpButton);
                ReflectionUtil.SetPrivateField(_modsTableView, "_pageDownButton", _pageDownButton);

                _modsTableView.ScrollToRow(0, false);
            }
            else
            {
                _modsTableView.ReloadData();
                _modsTableView.ScrollToRow(0, false);
            }
        }

        private void _modsTableView_DidSelectRowEvent(TableView sender, int row)
        {
            try
            {
                if (_parentViewController.ModDetailsViewController == null)
                {
                    _parentViewController.ModDetailsViewController = Instantiate(Resources.FindObjectsOfTypeAll<SongDetailViewController>().First(), rectTransform, false);

                    SetModDetailsData(_parentViewController.ModDetailsViewController, row);

                    _parentViewController.PushViewController(_parentViewController.ModDetailsViewController, false);

                    _parentViewController.ModDetailsPushed = true;
                }
                else
                {
                    if (_parentViewController.ModDetailsPushed)
                    {
                        SetModDetailsData(_parentViewController.ModDetailsViewController, row);
                    }
                    else
                    {
                        SetModDetailsData(_parentViewController.ModDetailsViewController, row);
                        _parentViewController.PushViewController(_parentViewController.ModDetailsViewController, false);

                        _parentViewController.ModDetailsPushed = true;
                    }
                }
            }
            catch (Exception e)
            {
                Utils.Log(e.ToString(), Utils.Severity.Error);
            }
        }

        private void SetModDetailsData(SongDetailViewController modDetails, int selectedMod)
        {
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "SongNameText").text = _mods[selectedMod].Name;
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "DurationText").text = "Version";
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "DurationValueText").text = _mods[selectedMod].Version;

            try
            {
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "BPMText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "BPMValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NotesCountText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NotesCountValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "ObstaclesCountText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "ObstaclesCountValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "Title").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "HighScoreText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "HighScoreValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "MaxComboText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "MaxComboValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "MaxRankText").gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "MaxRankValueText").gameObject);

                Destroy(modDetails.GetComponentsInChildren<RectTransform>().First(x => x.name == "YourStats").gameObject);

            }
            catch (Exception e)
            {
                Utils.Log(e.ToString(), Utils.Severity.Warning);
            }
            if (_toggleButton == null)
            {
                _toggleButton = modDetails.GetComponentInChildren<Button>();
            }
            if (_compatibilitytext == null)
            {
                var temp = modDetails.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "DurationText");

                _compatibilitytext = _modMenuUi.CreateTMPText(temp.rectTransform, "a", new Vector2(0.7f, 0.5f));
                // ive tried a lot of stuff im sorry for this horrible solution
                // it's ok ^ - Kaori
                _compatibilitytext.text = "                   This plugin doesnt work with BSMODUI";
            }
            if (_mods[selectedMod].GetPlugin is IModGui)
            {
                //Utils.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                //TODO: IMPLEMENT STUFFS
                _toggleButton.gameObject.SetActive(true);
                _modMenuUi.SetButtonText(ref _toggleButton, "Disable");
                _compatibilitytext.gameObject.SetActive(false);
            }
            else
            {
                _toggleButton.gameObject.SetActive(false);
                _compatibilitytext.gameObject.SetActive(true);
            }
        }

        public float RowHeight()
        {
            return 10f;
        }

        public int NumberOfRows()
        {
            return _mods.Count;
        }

        public TableCell CellForRow(int row)
        {
            var tableCell = Instantiate(_songListTableCellInstance);

            var mod = _mods.ElementAtOrDefault(row);
            tableCell.songName = mod?.Name;
            tableCell.author = mod?.Version;

            return tableCell;
        }


    }
}
