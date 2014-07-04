using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.Configuration;
using TestStack.White.Recording;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;

namespace Core
{
    public class White_AthenaMH3Uv138bBridge
    {
        public string ExePath { get; set; }

        public AthenaMH3Uv138bBridge(string exePath)
        {
            ExePath = exePath;
        }

        protected bool isLaunched = false;
        protected Application App { get; set; }
        public void Launch()
        {
            if (!isLaunched)
            {
                var startInfo = new ProcessStartInfo(ExePath);
                var dir = Directory.GetParent(ExePath);
                startInfo.WorkingDirectory = dir.FullName;

                App = Application.Launch(startInfo);
                isLaunched = true;
            }
        }

        public void DoStuff()
        {
            var allWindows = App.GetWindows();
            if (allWindows != null && allWindows.Count > 0)
            {
                var firstWindow = allWindows[0];
                var searchButton = firstWindow.Get<Button>("btnSearch");

                var mapper = new MainFormControlMapper(firstWindow);
                mapper.Map();
                

            }
        }

        public class MainFormControlMapper
        {
            public Window MainWindow { get; set; }

            public MainFormControlMapper(Window mainWindow)
            {
                MainWindow = mainWindow;
            }

            public MainFormControlMap Map()
            {
                var results = new MainFormControlMap();
                var config = CoreAppXmlConfiguration.Instance;
                config.RawElementBasedSearch = true;
                config.MaxElementSearchDepth = 4;

                var tabControl = MainWindow.Get<Tab>();

                
                results.CharmTable = GetCombo("cmbCharmTable");
                
                results.Charms = GetCombo("cmbCharms");

                results.Female = MainWindow.Get<RadioButton>("rdoFemale");
                results.Male = MainWindow.Get<RadioButton>("rdoMale");

                results.FilterResultsByCharm = GetCombo("cmbCharmSelect");

                results.MaxWeaponSlots = GetNumericUpDown("nudWeaponSlots");
                results.MogaQuests = GetNumericUpDown("nudHR");
                results.TanziaQuests = GetNumericUpDown("nudElder");

                results.ProgressBar = MainWindow.Get<ProgressBar>("progressBar1");
                results.ProgressBar.HookEvents(new GenericListener());

                results.Results = MainWindow.Get<TextBox>("txtSolutions");

                results.SortResultsBy = GetCombo("cmbSort");

                results.btnAdvancedSearch = GetButton("btnAdvancedSearch");
                results.btnCancel = GetButton("btnCancel");
                results.btnImport = GetButton("btnImport");
                results.btnMyCharms = GetButton("btnCharms");
                results.btnSearch = GetButton("btnSearch");

                results.BlademasterSkills = GetCombosInGroupBox("grpBSkills");
                results.BlademasterSkillFilters = GetCombosInGroupBox("grpBSkillFilters");
                results.BlademasterTab = tabControl.Pages[0];

                results.GunnerTab = tabControl.Pages[1];
                results.GunnerTab.Focus();
                results.GunnerSkillFilters = GetCombosInGroupBox("grpGSkillFilters");
                results.GunnerSkills = GetCombosInGroupBox("grpGSkills");

                results.BlademasterTab.Focus();
                results.btnAdvancedSearch.Visit();
                return results;
            }


            public Button GetButton(string controlName)
            {
                return MainWindow.Get<Button>(controlName);
            }
            public IUIItem GetNumericUpDown(string controlName)
            {
                return MainWindow.Get(SearchCriteria.ByAutomationId(controlName));
            }

            public ComboBox GetCombo(string comboName)
            {
                return MainWindow.Get<ComboBox>(comboName);
            }

            public List<ComboBox> GetCombosInGroupBox(string groupBoxName)
            {
                List<ComboBox> results = null;
                try
                {
                    var gbCriteria =
                        SearchCriteria.ByAutomationId(groupBoxName);
                    //.ByControlType(typeof (GroupBox), WindowsFramework.WinForms)

                    var groupBox = MainWindow.Get(gbCriteria);

                    var comboCriteria = SearchCriteria.ByControlType(typeof (ComboBox), WindowsFramework.WinForms);
                    var combos = groupBox.GetMultiple(comboCriteria);
                    if (combos != null)
                    {
                        results = new List<ComboBox>();
                        foreach (IUIItem item in combos)
                        {
                            results.Add(item as ComboBox);
                        }
                    }
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Exception with GetCombosInGroupBox for: " + groupBoxName);
                }
                return results;
            } 
        }

        public class MainFormControlMap
        {
            public Button btnSearch { get; set; }

            public IUIItem TanziaQuests { get; set; }
            public IUIItem MogaQuests { get; set; }
            public IUIItem MaxWeaponSlots { get; set; }

            public ComboBox CharmTable { get; set; }

            public RadioButton Male { get; set; }
            public RadioButton Female { get; set; }

            public ComboBox SortResultsBy { get; set; }
            public ComboBox FilterResultsByCharm { get; set; }
            public ComboBox Charms { get; set; }

            public Button btnMyCharms { get; set; }
            public Button btnImport { get; set; }

            public List<ComboBox> BlademasterSkills { get; set; }
            public List<ComboBox> BlademasterSkillFilters { get; set; }

            public List<ComboBox> GunnerSkills { get; set; }
            public List<ComboBox> GunnerSkillFilters { get; set; }

            public Button btnAdvancedSearch { get; set; }
            public Button btnCancel { get; set; }

            public ProgressBar ProgressBar { get; set; }

            public TextBox Results { get; set; }

            // TODO: menu items
            // TODO: Labels (for language support)

            // TODO: right click on results


            public IUIItem BlademasterTab { get; set; }

            public IUIItem GunnerTab { get; set; }
        }


        public class GenericListener : UIItemEventListener
        {

            public void EventOccured(TestStack.White.UIItemEvents.UserEvent userEvent)
            {
                System.Diagnostics.Debug.WriteLine("Event occurred: " + userEvent.UIItemType);
            }
        }
    }
}
