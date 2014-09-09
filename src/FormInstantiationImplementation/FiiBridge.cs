using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Fasterflect;

namespace FormInstantiationImplementation
{
    public class FiiV138bBridge {
        public string ExePath { get; set; }

        public FiiV138bBridge(string exePath)
        {
            ExePath = exePath;
            var dir = Directory.GetParent(ExePath);
            Environment.CurrentDirectory = dir.FullName;
        }
        protected Assembly AthenaAssembly { get; set; }
        protected bool isLaunched = false;
        protected Form MainForm { get; set; }
        public void Launch()
        {
            if (!isLaunched)
            {
                AthenaAssembly = Assembly.LoadFile(ExePath);
                var formType = AthenaAssembly.GetType("MH3GASS.Form1");
                var instance = formType.TryCreateInstance(null);
                if (instance is Form)
                {
                    var form = instance as Form;
                    form.Show();
                    var mapper = new MainFormControlMapper(form);
                    var mapping = mapper.Map();
                }
                isLaunched = true;
            }
        }
        public void DoStuff()
        {
        }

        public class MainFormEventBinder
        {
            public Form MainForm { get; set; }
            public MainFormControlMap Mapping { get; set; }

            public MainFormEventBinder(Form mainForm, MainFormControlMap mapping)
            {
                MainForm = mainForm;
                Mapping = mapping;
            }

            public void BindEvents()
            {
                var backgroundWorker = MainForm.GetFieldValue("backgroundWorker") as BackgroundWorker;
                backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            }

            void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                // Signal solutions finished
            }
        }

        public class MainFormControlMapper
        {
            public Form MainWindow { get; set; }

            public MainFormControlMapper(Form mainWindow)
            {
                MainWindow = mainWindow;
            }

            public MainFormControlMap Map()
            {
                var results = new MainFormControlMap();
                
                results.CharmTable = GetCombo("cmbCharmTable");
                
                results.Charms = GetCombo("cmbCharms");

                results.Female = GetRadio("rdoFemale");
                results.Male = GetRadio("rdoMale");

                results.FilterResultsByCharm = GetCombo("cmbCharmSelect");

                results.MaxWeaponSlots = GetNumericUpDown("nudWeaponSlots");
                results.MogaQuests = GetNumericUpDown("nudHR");
                results.TanziaQuests = GetNumericUpDown("nudElder");

                results.ProgressBar = GetProgressBar("progressBar1");

                results.Results = GetRichTextBox("txtSolutions");

                results.SortResultsBy = GetCombo("cmbSort");

                results.btnAdvancedSearch = GetButton("btnAdvancedSearch");
                results.btnCancel = GetButton("btnCancel");
                results.btnImport = GetButton("btnImport");
                results.btnMyCharms = GetButton("btnCharms");
                results.btnSearch = GetButton("btnSearch");

                results.BlademasterSkills = GetCombosInGroupBox("grpBSkills");
                results.BlademasterSkillFilters = GetCombosInGroupBox("grpBSkillFilters");
                results.BlademasterTab = GetTabPage("tabBlademaster");

                results.GunnerTab = GetTabPage("tabGunner");
                results.GunnerSkillFilters = GetCombosInGroupBox("grpGSkillFilters");
                results.GunnerSkills = GetCombosInGroupBox("grpGSkills");

                return results;
            }

            public ProgressBar GetProgressBar(string controlName)
            {
                return GetSafeAs<ProgressBar>(controlName);
            }

            public RichTextBox GetRichTextBox(string controlName)
            {
                return GetSafeAs<RichTextBox>(controlName);
            }

            public RadioButton GetRadio(string controlName)
            {
                return GetSafeAs<RadioButton>(controlName);
            }

            public TabPage GetTabPage(string controlName)
            {
                return GetSafeAs<TabPage>(controlName);
            }
            public Button GetButton(string controlName)
            {
                return GetSafeAs<Button>(controlName);
            }
            public NumericUpDown GetNumericUpDown(string controlName)
            {
                return GetSafeAs<NumericUpDown>(controlName);
            }

            public T GetSafeAs<T>(string controlName) where T : Control
            {
                return MainWindow.GetFieldValue(controlName) as T;
            }

            public ComboBox GetCombo(string controlName)
            {
                return GetSafeAs<ComboBox>(controlName);
            }

            public List<ComboBox> GetCombosInGroupBox(string groupBoxName)
            {
                List<ComboBox> results = new List<ComboBox>();

                var groupBox = GetSafeAs<GroupBox>(groupBoxName);
                
                foreach (var item in groupBox.Controls)
                {
                    if (item is ComboBox)
                    {
                        results.Add(item as ComboBox);
                    }
                }

                return results;
            } 
        }

        public class MainFormControlMap : IMainFormControlMap
        {
            public Button btnSearch { get; set; }

            public NumericUpDown TanziaQuests { get; set; }
            public NumericUpDown MogaQuests { get; set; }
            public NumericUpDown MaxWeaponSlots { get; set; }

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

            public RichTextBox Results { get; set; }

            // TODO: menu items
            // TODO: Labels (for language support)

            // TODO: right click on results


            public TabPage BlademasterTab { get; set; }

            public TabPage GunnerTab { get; set; }
        }
    }

    public class LexiconBuilder
    {
        public Lexicon Build(IMainFormControlMap controlMap)
        {
            var results = new Lexicon();

            return results;
        }

        public KeyValuePair<string, object>? BuildButton(Button button)
        {
            if (button == null) return null;
            var results = new KeyValuePair<string, object>(button.Name, button.Text);
            return results;
        }

        public void SetPairInLexicon(Lexicon lexicon, KeyValuePair<string, object>? pair)
        {
            if (pair == null || !pair.HasValue) return;
            lexicon[pair.Value.Key] = pair.Value.Value;
        }
    }

    /*
     * 
     * 
     * So we need a few different sets of data.
     * First of all, we need to know the names of all the labels currently on the screen, or available to the screen.
     * This includes all the possible, unfiltered, values in any ComboBoxes or whatnot. 
     * These items will only be loaded once at the start, and perhaps any other time the language is changed.
     * We shall call this the Lexicon.
     * 
     * Second, we need to know the current values for any changeable or configurable field. 
     * This can change frequently on the client side. I can't really think of any reason why 
     * the server side would need to send these things back. This is more of a mapping to 
     * be sent TO the server in order to do its work calculating the armor set.
     * This is also really just a key/value collection.
     * 
     * The server needs to respond with these things:
     * - If there's a search currently happening
     * - The percentage of progress finished
     * - When finished, the text/objects in the Results section
     * I think that's it. 
     * */

    public class Lexicon : Dictionary<string, object>
    {
        public Lexicon() { }
    }

    public class FieldValues : Dictionary<string, object>
    {
        public FieldValues() { }
    }

    public class QueryResponse
    {
        public bool IsSearchInProgress { get; set; }
        public int PercentComplete { get; set; }
        public object Results { get; set; } // Don't know what exactly will be here
    }

    public class LayoutData
    {
        public LayoutData()
        {
            NamedResources = new Dictionary<string, object>();
        }

        public class LayoutItem
        {
            virtual public string ID { get; set; }
            virtual public string Type { get; set; }
            virtual public object CurrentValue { get; set; }
        }
        
        public class NumericUpDownLayoutItem : LayoutItem
        {
            virtual public int Minimum { get; set; }
            virtual public int Maximum { get; set; }
        }

        public class ComboBoxLayoutItem : LayoutItem
        {
            virtual public List<object> PossibleValues { get; set; } 
        }


        //public Button btnSearch { get; set; }

        public NumericUpDownLayoutItem TanziaQuests { get; set; }
        public NumericUpDownLayoutItem MogaQuests { get; set; }
        public NumericUpDownLayoutItem MaxWeaponSlots { get; set; }

        public ComboBoxLayoutItem CharmTable { get; set; }

        public LayoutItem Male { get; set; }
        public LayoutItem Female { get; set; }

        public ComboBoxLayoutItem SortResultsBy { get; set; }
        public ComboBoxLayoutItem FilterResultsByCharm { get; set; }
        public ComboBoxLayoutItem Charms { get; set; }

        //public Button btnMyCharms { get; set; }
        //public Button btnImport { get; set; }

        public ComboBoxLayoutItem BlademasterSkills { get; set; }
        public ComboBoxLayoutItem BlademasterSkillFilters { get; set; }

        public ComboBoxLayoutItem GunnerSkills { get; set; }
        public ComboBoxLayoutItem GunnerSkillFilters { get; set; }

        //public Button btnAdvancedSearch { get; set; }
        //public Button btnCancel { get; set; }

        //public ProgressBar ProgressBar { get; set; }

        public LayoutItem Results { get; set; }

        public Dictionary<string, object> NamedResources { get; set; } 

        // TODO: menu items
        // TODO: Labels (for language support)

        // TODO: right click on results


        //public TabPage BlademasterTab { get; set; }
        //public TabPage GunnerTab { get; set; }
    }
}
