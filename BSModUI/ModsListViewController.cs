using HMUI;
using IllusionInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

namespace BSModUI
{
    class ModsListViewController : VRUIViewController, TableView.IDataSource
    {
        private new ModMenuMasterViewController _parentViewController;

        private ModMenuUi _modMenuUi;

        private Button _pageUpButton;
        private Button _pageDownButton;

        private List<ModSelection> _modSelections = new List<ModSelection>();

        private List<Mod> _mods = new List<Mod>();

        private TableView _modsTableView;
        SongListTableCell _songListTableCellInstance;

        protected override void DidActivate()
        {

            _modMenuUi = FindObjectOfType<ModMenuUi>();
            _parentViewController = transform.parent.GetComponent<ModMenuMasterViewController>();

            try
            {

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
                Name = (plugin.Name == null) ? "No name" : plugin.Name,
                Version = (plugin.Version == null) ? "No version" : plugin.Version,
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

                try
                {
                    Mask viewportMask = Instantiate(Resources.FindObjectsOfTypeAll<UnityEngine.UI.Mask>().First(), _modsTableView.transform, false);

                    _modsTableView.GetComponentsInChildren<RectTransform>().Where(x => x.name == "Content").First().transform.SetParent(viewportMask.rectTransform, false);
                }
                catch (Exception e)
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
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "SongNameText").First().text = _mods[selectedMod].Name;
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "DurationText").First().text = "Version";
            modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "DurationValueText").First().text = _mods[selectedMod].Version;

            try
            {
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "BPMText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "BPMValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "NotesCountText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "NotesCountValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "ObstaclesCountText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "ObstaclesCountValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "Title").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "HighScoreText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "HighScoreValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "MaxComboText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "MaxComboValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "MaxRankText").First().gameObject);
                Destroy(modDetails.GetComponentsInChildren<TextMeshProUGUI>().Where(x => x.name == "MaxRankValueText").First().gameObject);

                Destroy(modDetails.GetComponentsInChildren<RectTransform>().Where(x => x.name == "YourStats").First().gameObject);

            }
            catch (Exception e)
            {
                Utils.Log(e.ToString(), Utils.Severity.Warning);
            }

            Button toggleButton = modDetails.GetComponentInChildren<Button>();

            _modMenuUi.SetButtonText(ref toggleButton, "Disable");



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
            SongListTableCell tableCell = Instantiate(_songListTableCellInstance);

            tableCell.songName = _mods.ElementAtOrDefault(row).Name;
            tableCell.author = _mods.ElementAtOrDefault(row).Version;

            return tableCell;
        }


    }
}
