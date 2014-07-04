// Type: MH3GASS.Form1
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MH3GASS
{
  public class Form1 : Form
  {
    private static int NumSkills = 6;
    private static DialogResult OK = DialogResult.OK;
    private static Mutex progress_mutex = new Mutex();
    private static Mutex results_mutex = new Mutex();
    private static Mutex charm_map_mutex = new Mutex();
    private static Mutex worker_mutex = new Mutex();
    private int MAX_LIMIT = 1000;
    private string CFG_FILE = "settings.cfg";
    private string endl = "\r\n";
    private bool construction_complete = false;
    private readonly Dictionary<string, List<Solution>> charm_solution_map = new Dictionary<string, List<Solution>>();
    private readonly Dictionary<string, Dictionary<long, bool>> existing_armor = new Dictionary<string, Dictionary<long, bool>>();
    private readonly List<Solution> final_solutions = new List<Solution>();
    private readonly List<Solution> no_charm_solutions = new List<Solution>();
    private readonly List<Solution> all_solutions = new List<Solution>();
    private readonly List<ComboBox> bSkills = new List<ComboBox>();
    private readonly List<ComboBox> gSkills = new List<ComboBox>();
    private readonly List<ComboBox> bSkillFilters = new List<ComboBox>();
    private readonly List<ComboBox> gSkillFilters = new List<ComboBox>();
    private readonly List<Dictionary<uint, uint>> bIndexMaps = new List<Dictionary<uint, uint>>();
    private readonly List<Dictionary<uint, uint>> gIndexMaps = new List<Dictionary<uint, uint>>();
    private readonly List<string> languages = new List<string>();
    private readonly List<Charm> charm_box_charms = new List<Charm>();
    private readonly List<BackgroundWorker> workers = new List<BackgroundWorker>();
    private readonly List<ThreadSearchData> worker_data = new List<ThreadSearchData>();
    private string last_result;
    private bool lock_skills;
    private bool sort_off;
    private bool can_save;
    private bool last_search_gunner;
    private bool updating_language;
    private bool lock_related;
    private bool search_cancelled;
    private LoadedData data;
    private Query query;
    private frmFind find_dialog;
    private List<List<uint>> blast_options;
    private List<List<uint>> glast_options;
    private int language;
    private int adv_x;
    private int adv_y;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem mnuFile;
    private ToolStripMenuItem mnuExit;
    private ToolStripMenuItem mnuHelp;
    private ToolStripMenuItem mnuAbout;
    private ToolStripMenuItem mnuLanguage;
    private GroupBox groupBox6;
    private RadioButton rdoFemale;
    private RadioButton rdoMale;
    private Button btnAdvancedSearch;
    private TabControl tabHunterType;
    private TabPage tabBlademaster;
    private TabPage tabGunner;
    private GroupBox grpGSkillFilters;
    private GroupBox grpGSkills;
    private NumericUpDown nudHR;
    private NumericUpDown nudWeaponSlots;
    private NumericUpDown nudElder;
    private GroupBox groupBox1;
    private Label lblHR;
    private Label lblElder;
    private Label lblSlots;
    private Button btnCancel;
    private GroupBox grpBSkills;
    private Button btnSearch;
    private ProgressBar progressBar1;
    private RichTextBox txtSolutions;
    private GroupBox groupBox4;
    private GroupBox grpResults;
    private GroupBox grpBSkillFilters;
    private GroupBox grpSort;
    private ComboBox cmbSort;
    private ContextMenuStrip cmsSolutions;
    private GroupBox grpCharmFilter;
    private ComboBox cmbCharms;
    private ToolStripMenuItem mnuOptions;
    private ToolStripMenuItem mnuAllowBadSkills;
    private ToolStripMenuItem mnuAllowPiercings;
    private ToolStripMenuItem mnuAllowEventArmor;
    private Button btnCharms;
    private GroupBox grpCharms;
    private ComboBox cmbCharmSelect;
    private ToolStripMenuItem mnuCheckForUpdates;
    private ToolStripMenuItem mnuClearSettings;
    private ToolStripSeparator toolStripSeparator1;
    private ContextMenuStrip cmsCharms;
    private ToolStripMenuItem mnuLoadData;
    private ToolStripMenuItem mnuSaveData;
    private ToolStripMenuItem mnuSortSkillsAlphabetically;
    private ToolStripMenuItem mnuPrintDecoNames;
    private ToolStripMenuItem mnuMaxResults;
    private ToolStripTextBox mnuNumResults;
    private Button btnImport;
    private ToolStripMenuItem mnuAllowLowerTierArmor;
    private Label lblCharmTable;
    private Label lblWhatIsCharmTable;
    private ComboBox cmbCharmTable;
    private uint finished_workers;
    private uint total_progress;
    private uint worker_start_index;
    private uint num_updates;
    private ToolStripMenuItem mnuAllowJapaneseOnlyDLC;
    private IContainer components;

    static Form1()
    {
    }

    public Form1()
    {
      // ISSUE: fault handler
      try
      {
        this.DoubleBuffered = true;
        this.language = -1;
        this.sort_off = false;
        this.updating_language = false;
        this.can_save = false;
        this.last_search_gunner = false;
        this.lock_related = false;
        this.InitializeComponent();
        this.InitializeComboBoxes();
        this.mnuNumResults.KeyPress += new KeyPressEventHandler(this.MaxResultsTextBoxKeyPress);
        this.mnuNumResults.TextChanged += new EventHandler(this.MaxResultsTextChanged);
        this.can_save = true;
        this.adv_x = 1031;
        this.adv_y = 587;
        this.data = new LoadedData();
        this.data.ImportTextFiles();
        CharmDatabase.GenerateCharmTable();
        this.LoadLanguages();
        this.InitFilters();
        this.InitSkills();
        this.lock_skills = false;
        this.btnCancel.Enabled = false;
        this.LoadConfig();
        CharmDatabase.LoadCustom();
        Form1 form1 = this;
        string str = form1.Text + " v" + "1.37b";
        form1.Text = str;
        this.construction_complete = true;
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    private void ClearFilters()
    {
      List<ComboBox>.Enumerator enumerator1 = this.bSkillFilters.GetEnumerator();
      while (enumerator1.MoveNext())
        enumerator1.Current.Items.Clear();
      List<ComboBox>.Enumerator enumerator2 = this.gSkillFilters.GetEnumerator();
      while (enumerator2.MoveNext())
        enumerator2.Current.Items.Clear();
    }

    private void AddFilters()
    {
      List<SkillTag>.Enumerator enumerator1 = SkillTag.tags.GetEnumerator();
label_1:
      while (enumerator1.MoveNext())
      {
        SkillTag current = enumerator1.Current;
        if (!current.disable_g)
        {
          List<ComboBox>.Enumerator enumerator2 = this.gSkillFilters.GetEnumerator();
          while (enumerator2.MoveNext())
            enumerator2.Current.Items.Add((object) current.name);
        }
        if (!current.disable_b)
        {
          List<ComboBox>.Enumerator enumerator2 = this.bSkillFilters.GetEnumerator();
          while (true)
          {
            if (enumerator2.MoveNext())
              enumerator2.Current.Items.Add((object) current.name);
            else
              goto label_1;
          }
        }
      }
    }

    private void InitFilters()
    {
      this.ClearFilters();
      this.AddFilters();
      List<ComboBox>.Enumerator enumerator1 = this.gSkillFilters.GetEnumerator();
      while (enumerator1.MoveNext())
        enumerator1.Current.SelectedIndex = 0;
      List<ComboBox>.Enumerator enumerator2 = this.bSkillFilters.GetEnumerator();
      while (enumerator2.MoveNext())
        enumerator2.Current.SelectedIndex = 0;
    }

    private void ResetSkill(ComboBox box, Dictionary<uint, uint> map, Skill skill)
    {
      if (skill == null)
        return;
      Dictionary<uint, uint>.Enumerator enumerator = map.GetEnumerator();
      while (enumerator.MoveNext())
      {
        KeyValuePair<uint, uint> current1 = enumerator.Current;
        if (Skill.static_skills[(int) current1.Value] == skill)
        {
          KeyValuePair<uint, uint> current2 = enumerator.Current;
          box.SelectedIndex = (int) current2.Key;
          break;
        }
      }
    }

    private void InitSkills2(ComboBox box, Dictionary<uint, uint> map, int filter, List<Ability> disallowed)
    {
      map.Clear();
      box.SelectedIndex = -1;
      box.Items.Clear();
      if (filter == -1 || StringTable.text == null)
        return;
      box.Items.Add((object) StringTable.text[75]);
      List<Skill>.Enumerator enumerator = (!this.mnuSortSkillsAlphabetically.Checked ? Skill.static_skills : Skill.ordered_skills).GetEnumerator();
      while (enumerator.MoveNext())
      {
        Skill current = enumerator.Current;
        if (current.points_required > 0 && !<Module>.Utility>Contains<struct\u0020Ability>(disallowed, current.ability) && (filter == 0 || filter == 1 && current.ability.tags.Count == 0 || (filter == 2 && current.ability.related || <Module>.Utility>FindByName<struct\u0020SkillTag>(current.ability.tags, SkillTag.tags[filter].name) != null)))
        {
          map[(uint) box.Items.Count] = (uint) current.static_index;
          box.Items.Add((object) current.name);
        }
      }
    }

    private void InitSkills()
    {
      for (uint index = 0U; index < 6U; ++index)
      {
        Form1 form1_1 = this;
        ComboBox box1 = form1_1.gSkills[(int) index];
        Dictionary<uint, uint> map1 = this.gIndexMaps[(int) index];
        int selectedIndex1 = this.gSkillFilters[(int) index].SelectedIndex;
        List<Ability> disallowed1 = new List<Ability>();
        int num1 = 0;
        form1_1.InitSkills(box1, map1, selectedIndex1, disallowed1, num1 != 0);
        Form1 form1_2 = this;
        ComboBox box2 = form1_2.bSkills[(int) index];
        Dictionary<uint, uint> map2 = this.bIndexMaps[(int) index];
        int selectedIndex2 = this.bSkillFilters[(int) index].SelectedIndex;
        List<Ability> disallowed2 = new List<Ability>();
        int num2 = 1;
        form1_2.InitSkills(box2, map2, selectedIndex2, disallowed2, num2 != 0);
      }
    }

    private void InitSkills(ComboBox box, Dictionary<uint, uint> map, int filter, List<Ability> disallowed, [MarshalAs(UnmanagedType.U1)] bool blade)
    {
      uint num = (uint) filter;
      if (blade)
      {
        for (int index = 0; index <= filter; ++index)
          num += (uint) SkillTag.tags[index].disable_b;
      }
      else
      {
        for (int index = 0; index <= filter; ++index)
          num += (uint) SkillTag.tags[index].disable_g;
      }
      this.InitSkills2(box, map, (int) num, disallowed);
    }

    private ComboBox GetNewComboBox(uint width, uint i)
    {
      ComboBox comboBox = new ComboBox();
      Point point = new Point(6, (int) i * 27 + 19);
      comboBox.Location = point;
      Size size = new Size((int) width, 21);
      comboBox.Size = size;
      comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      return comboBox;
    }

    private void InitializeComboBoxes()
    {
      for (uint i = 0U; i < 6U; ++i)
      {
        this.gSkillFilters.Add(this.GetNewComboBox(134U, i));
        this.bSkillFilters.Add(this.GetNewComboBox(134U, i));
        this.gSkillFilters[(int) i].SelectedIndexChanged += new EventHandler(this.cmbSkillFilter_SelectedIndexChanged);
        this.bSkillFilters[(int) i].SelectedIndexChanged += new EventHandler(this.cmbSkillFilter_SelectedIndexChanged);
        this.grpGSkillFilters.Controls.Add((Control) this.gSkillFilters[(int) i]);
        this.grpBSkillFilters.Controls.Add((Control) this.bSkillFilters[(int) i]);
        this.gSkills.Add(this.GetNewComboBox(171U, i));
        this.bSkills.Add(this.GetNewComboBox(171U, i));
        this.gSkills[(int) i].SelectedIndexChanged += new EventHandler(this.cmbSkill_SelectedIndexChanged);
        this.bSkills[(int) i].SelectedIndexChanged += new EventHandler(this.cmbSkill_SelectedIndexChanged);
        this.grpGSkills.Controls.Add((Control) this.gSkills[(int) i]);
        this.grpBSkills.Controls.Add((Control) this.bSkills[(int) i]);
        this.gIndexMaps.Add(new Dictionary<uint, uint>());
        this.bIndexMaps.Add(new Dictionary<uint, uint>());
      }
      this.charm_solution_map.Clear();
      this.cmbSort.SelectedIndex = 0;
      this.cmbCharms.SelectedIndex = 1;
      this.cmbCharmSelect.SelectedIndex = 3;
      this.cmbCharmTable.SelectedIndex = 0;
    }

    public void LoadLanguages()
    {
      foreach (string str in Directory.GetDirectories("Data/Languages"))
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @str;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem((^local).Substring((^local).LastIndexOf('\\') + 1));
        toolStripMenuItem.Click += new EventHandler(this.LanguageSelect_Click);
        this.mnuLanguage.DropDownItems.Add((ToolStripItem) toolStripMenuItem);
      }
    }

    private void AddSolution(string charm, Solution sol)
    {
      if (this.SolutionExists(charm, sol))
        return;
      if (!this.charm_solution_map.ContainsKey(charm))
        this.charm_solution_map.Add(charm, new List<Solution>());
      this.charm_solution_map[charm].Add(sol);
      this.all_solutions.Add(sol);
    }

    public void AddSolution(string line, uint version)
    {
      List<string> vec = new List<string>();
      <Module>.Utility>SplitString(vec, line, ' ');
      Solution sol = new Solution();
      for (uint index1 = 0U; index1 < 5U; ++index1)
      {
        int index2 = Convert.ToInt32(vec[(int) index1]);
        if (index2 >= 0)
          goto label_5;
        sol.armors.Add((Armor) null);
        continue;
label_5:
        sol.armors.Add(Armor.static_armors[(int) index1][index2]);
      }
      uint num1;
      if (version >= 4U)
      {
        uint num2 = Convert.ToUInt32(vec[9]);
        uint num3 = Convert.ToUInt32(vec[10]);
        num1 = (uint) ((int) num2 + (int) num3 + 1);
        for (uint index = 0U; index < num2; ++index)
          sol.body_decorations.Add(Decoration.static_decorations[(int) Convert.ToUInt32(vec[(int) index + 11])]);
        for (uint index = 0U; index < num3; ++index)
          sol.non_body_decorations.Add(Decoration.static_decorations[(int) Convert.ToUInt32(vec[(int) num2 + 11 + (int) index])]);
        sol.decorations.AddRange((IEnumerable<Decoration>) sol.body_decorations);
        sol.decorations.AddRange((IEnumerable<Decoration>) sol.non_body_decorations);
      }
      else
      {
        num1 = Convert.ToUInt32(vec[9]);
        for (uint index = 0U; index < num1; ++index)
          sol.decorations.Add(Decoration.static_decorations[(int) Convert.ToUInt32(vec[(int) index + 10])]);
      }
      uint num4 = Convert.ToUInt32(vec[(int) num1 + 10]);
      for (uint index = 0U; index < num4; ++index)
        sol.extra_skills.Add(Skill.static_skills[(int) Convert.ToUInt32(vec[(int) num1 + 11 + (int) index])]);
      uint num5 = num1 + 11U + num4;
      if (num5 < (uint) vec.Count)
      {
        sol.charm = new Charm();
        sol.charm.num_slots = Convert.ToUInt32(vec[(int) num5]);
        uint num2 = Convert.ToUInt32(vec[(int) num5 + 1]);
        for (uint index = 0U; index < num2; ++index)
        {
          Ability ab = Ability.static_abilities[(int) Convert.ToUInt32(vec[(int) num5 + 3 + (int) index * 2])];
          int am = Convert.ToInt32(vec[(int) num5 + 2 + (int) index * 2]);
          sol.charm.abilities.Add(new AbilityPair(ab, am));
        }
      }
      sol.slots_spare = new uint[4];
      Decimal num6 = this.nudElder.Value;
      Decimal num7 = this.nudHR.Value;
      sol.CalculateData((uint) (int) num7, (uint) (int) num6);
      sol.total_slots_spare = Convert.ToUInt32(vec[5]);
      for (uint index = 1U; index <= 3U; ++index)
        sol.slots_spare[(int) index] = Convert.ToUInt32(vec[(int) index + 5]);
      if (sol.charm != null)
      {
        this.AddSolution(sol.charm.GetName(), sol);
      }
      else
      {
        this.no_charm_solutions.Add(sol);
        this.all_solutions.Add(sol);
      }
    }

    public void LoadConfig()
    {
      Form1 form1 = this;
      string file = form1.CFG_FILE;
      form1.LoadConfig(file);
    }

    public void LoadConfig(string file)
    {
      this.can_save = false;
      if (File.Exists(file))
      {
        StreamReader streamReader1 = new StreamReader(file);
        StreamReader streamReader2;
        int num1;
        // ISSUE: fault handler
        try
        {
          streamReader2 = streamReader1;
          num1 = Convert.ToInt32(streamReader2.ReadLine());
          if (num1 >= 1)
          {
            if (num1 <= 8)
              goto label_7;
          }
          streamReader2.Close();
          this.can_save = true;
        }
        __fault
        {
          streamReader2.Dispose();
        }
        streamReader2.Dispose();
        return;
label_7:
        // ISSUE: fault handler
        try
        {
          this.language = -1;
          int index1 = (int) Convert.ToUInt32(streamReader2.ReadLine());
          Form1 form1_1 = this;
          ToolStripItem toolStripItem = form1_1.mnuLanguage.DropDownItems[index1];
          // ISSUE: variable of the null type
          __Null local = null;
          form1_1.LanguageSelect_Click((object) toolStripItem, (EventArgs) local);
          if (num1 >= 3)
          {
            this.MAX_LIMIT = Convert.ToInt32(streamReader2.ReadLine());
            this.mnuNumResults.Text = "" + (object) this.MAX_LIMIT;
          }
          this.last_search_gunner = Convert.ToBoolean(streamReader2.ReadLine());
          int num2 = Convert.ToInt32(streamReader2.ReadLine());
          this.rdoMale.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          this.rdoFemale.Checked = !this.rdoMale.Checked;
          this.mnuAllowBadSkills.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          this.mnuAllowPiercings.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          this.mnuAllowEventArmor.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          if (num1 >= 7)
            this.mnuAllowJapaneseOnlyDLC.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          this.mnuAllowLowerTierArmor.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          if (num1 >= 4)
            this.mnuPrintDecoNames.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          if (num1 >= 5)
            this.mnuSortSkillsAlphabetically.Checked = Convert.ToBoolean(streamReader2.ReadLine());
          this.cmbSort.SelectedIndex = Convert.ToInt32(streamReader2.ReadLine());
          this.cmbCharmSelect.SelectedIndex = Convert.ToInt32(streamReader2.ReadLine());
          if (num1 >= 2)
            this.cmbCharmTable.SelectedIndex = Convert.ToInt32(streamReader2.ReadLine());
          this.nudHR.Value = (Decimal) Convert.ToInt32(streamReader2.ReadLine());
          this.nudElder.Value = (Decimal) Convert.ToInt32(streamReader2.ReadLine());
          this.nudWeaponSlots.Value = (Decimal) Convert.ToInt32(streamReader2.ReadLine());
          if (num1 >= 6)
          {
            this.adv_x = Convert.ToInt32(streamReader2.ReadLine());
            this.adv_y = Convert.ToInt32(streamReader2.ReadLine());
          }
          this.tabHunterType.SuspendLayout();
          for (uint index2 = 0U; index2 < 6U; ++index2)
          {
            this.tabHunterType.SelectedIndex = 0;
            this.bSkillFilters[(int) index2].SelectedIndex = Convert.ToInt32(streamReader2.ReadLine());
            if (this.bSkillFilters[(int) index2].SelectedIndex == 2)
            {
              this.bSkillFilters[(int) index2].SelectedIndex = 0;
              ComboBox comboBox = this.bSkills[(int) index2];
              Form1 form1_2 = this;
              Dictionary<uint, uint> imap = form1_2.bIndexMaps[(int) index2];
              int skill_index = Convert.ToInt32(streamReader2.ReadLine());
              int num3 = form1_2.SearchIndexMap(imap, skill_index);
              comboBox.SelectedIndex = num3;
              this.bSkillFilters[(int) index2].SelectedIndex = 2;
            }
            else
            {
              ComboBox comboBox = this.bSkills[(int) index2];
              Form1 form1_2 = this;
              Dictionary<uint, uint> imap = form1_2.bIndexMaps[(int) index2];
              int skill_index = Convert.ToInt32(streamReader2.ReadLine());
              int num3 = form1_2.SearchIndexMap(imap, skill_index);
              comboBox.SelectedIndex = num3;
            }
            this.tabHunterType.SelectedIndex = 1;
            this.gSkillFilters[(int) index2].SelectedIndex = Convert.ToInt32(streamReader2.ReadLine());
            if (this.gSkillFilters[(int) index2].SelectedIndex == 2)
            {
              this.gSkillFilters[(int) index2].SelectedIndex = 0;
              ComboBox comboBox = this.gSkills[(int) index2];
              Form1 form1_2 = this;
              Dictionary<uint, uint> imap = form1_2.gIndexMaps[(int) index2];
              int skill_index = Convert.ToInt32(streamReader2.ReadLine());
              int num3 = form1_2.SearchIndexMap(imap, skill_index);
              comboBox.SelectedIndex = num3;
              this.gSkillFilters[(int) index2].SelectedIndex = 2;
            }
            ComboBox comboBox1 = this.gSkills[(int) index2];
            Form1 form1_3 = this;
            Dictionary<uint, uint> imap1 = form1_3.gIndexMaps[(int) index2];
            int skill_index1 = Convert.ToInt32(streamReader2.ReadLine());
            int num4 = form1_3.SearchIndexMap(imap1, skill_index1);
            comboBox1.SelectedIndex = num4;
          }
          this.tabHunterType.SelectedIndex = num2;
          this.tabHunterType.ResumeLayout();
          this.FormulateQuery(false, this.last_search_gunner);
          this.charm_solution_map.Clear();
          this.all_solutions.Clear();
          this.no_charm_solutions.Clear();
          while (!streamReader2.EndOfStream)
            this.AddSolution(streamReader2.ReadLine(), (uint) num1);
          this.last_result = (string) null;
          streamReader2.Close();
          this.UpdateCharmComboBox(1);
        }
        __fault
        {
          streamReader2.Dispose();
        }
        streamReader2.Dispose();
      }
      else if (this.mnuLanguage.HasDropDownItems)
      {
        this.language = -1;
        string str = CultureInfo.InstalledUICulture.Parent.NativeName.ToLower();
        int length = str.IndexOf(" ");
        if (length > 0)
          str = str.Substring(0, length);
        IEnumerator enumerator1 = this.mnuLanguage.DropDownItems.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            ToolStripItem toolStripItem = (ToolStripItem) enumerator1.Current;
            if (toolStripItem.ToString().ToLower().IndexOf(str) >= 0)
            {
              this.LanguageSelect_Click((object) toolStripItem, (EventArgs) null);
              break;
            }
          }
        }
        finally
        {
          IDisposable disposable = enumerator1 as IDisposable;
          int num;
          if (disposable != null)
          {
            disposable.Dispose();
            num = 0;
          }
          else
            num = 0;
        }
        if (this.language == -1)
        {
          IEnumerator enumerator2 = this.mnuLanguage.DropDownItems.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              ToolStripItem toolStripItem = (ToolStripItem) enumerator2.Current;
              if (toolStripItem.ToString().IndexOf("English") >= 0)
              {
                this.LanguageSelect_Click((object) toolStripItem, (EventArgs) null);
                break;
              }
            }
          }
          finally
          {
            IDisposable disposable = enumerator2 as IDisposable;
            int num;
            if (disposable != null)
            {
              disposable.Dispose();
              num = 0;
            }
            else
              num = 0;
          }
        }
        if (this.language == -1)
        {
          Form1 form1 = this;
          ToolStripItem toolStripItem = form1.mnuLanguage.DropDownItems[0];
          // ISSUE: variable of the null type
          __Null local = null;
          form1.LanguageSelect_Click((object) toolStripItem, (EventArgs) local);
        }
      }
      this.can_save = true;
      this.SaveConfig();
    }

    public void SaveConfig()
    {
      Form1 form1 = this;
      string file = form1.CFG_FILE;
      form1.SaveConfig(file);
    }

    public void SaveConfig(string file)
    {
      if (!this.can_save)
        return;
      StreamWriter streamWriter1 = new StreamWriter(file);
      StreamWriter streamWriter2;
      // ISSUE: fault handler
      try
      {
        streamWriter2 = streamWriter1;
        streamWriter2.WriteLine("8");
        streamWriter2.WriteLine(this.language);
        streamWriter2.WriteLine(this.MAX_LIMIT);
        streamWriter2.WriteLine(this.last_search_gunner);
        streamWriter2.WriteLine(this.tabHunterType.SelectedIndex);
        streamWriter2.WriteLine(this.rdoMale.Checked);
        streamWriter2.WriteLine(this.mnuAllowBadSkills.Checked);
        streamWriter2.WriteLine(this.mnuAllowPiercings.Checked);
        streamWriter2.WriteLine(this.mnuAllowEventArmor.Checked);
        streamWriter2.WriteLine(this.mnuAllowJapaneseOnlyDLC.Checked);
        streamWriter2.WriteLine(this.mnuAllowLowerTierArmor.Checked);
        streamWriter2.WriteLine(this.mnuPrintDecoNames.Checked);
        streamWriter2.WriteLine(this.mnuSortSkillsAlphabetically.Checked);
        streamWriter2.WriteLine(this.cmbSort.SelectedIndex);
        streamWriter2.WriteLine(this.cmbCharmSelect.SelectedIndex);
        streamWriter2.WriteLine(this.cmbCharmTable.SelectedIndex);
        Decimal num1 = this.nudHR.Value;
        streamWriter2.WriteLine(num1);
        Decimal num2 = this.nudElder.Value;
        streamWriter2.WriteLine(num2);
        Decimal num3 = this.nudWeaponSlots.Value;
        streamWriter2.WriteLine(num3);
        streamWriter2.WriteLine(this.adv_x);
        streamWriter2.WriteLine(this.adv_y);
        for (uint index = 0U; index < 6U; ++index)
        {
          streamWriter2.WriteLine(this.bSkillFilters[(int) index].SelectedIndex);
          if (this.bSkills[(int) index].SelectedIndex == -1)
            streamWriter2.WriteLine(-1);
          else
            streamWriter2.WriteLine(this.bIndexMaps[(int) index][(uint) this.bSkills[(int) index].SelectedIndex]);
          streamWriter2.WriteLine(this.gSkillFilters[(int) index].SelectedIndex);
          if (this.gSkills[(int) index].SelectedIndex != -1)
            goto label_11;
          streamWriter2.WriteLine(-1);
          continue;
label_11:
          streamWriter2.WriteLine(this.gIndexMaps[(int) index][(uint) this.gSkills[(int) index].SelectedIndex]);
        }
        List<Solution>.Enumerator enumerator1 = this.all_solutions.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          Solution current1 = enumerator1.Current;
          for (uint index = 0U; index < 5U; ++index)
          {
            streamWriter2.Write(Convert.ToString(<Module>.Utility>GetIndexOf<struct\u0020Armor>(Armor.static_armors[(int) index], current1.armors[(int) index])));
            streamWriter2.Write(" ");
          }
          streamWriter2.Write(Convert.ToString(current1.total_slots_spare));
          streamWriter2.Write(" ");
          for (uint index = 1U; index <= 3U; ++index)
          {
            streamWriter2.Write(Convert.ToString(current1.slots_spare[(int) index]));
            streamWriter2.Write(" ");
          }
          streamWriter2.Write(Convert.ToString(current1.body_decorations.Count));
          streamWriter2.Write(" ");
          streamWriter2.Write(Convert.ToString(current1.non_body_decorations.Count));
          streamWriter2.Write(" ");
          List<Decoration>.Enumerator enumerator2 = current1.body_decorations.GetEnumerator();
          while (enumerator2.MoveNext())
          {
            Decoration current2 = enumerator2.Current;
            streamWriter2.Write(Convert.ToString(<Module>.Utility>GetIndexOf<struct\u0020Decoration>(Decoration.static_decorations, current2)));
            streamWriter2.Write(" ");
          }
          List<Decoration>.Enumerator enumerator3 = current1.non_body_decorations.GetEnumerator();
          while (enumerator3.MoveNext())
          {
            Decoration current2 = enumerator3.Current;
            streamWriter2.Write(Convert.ToString(<Module>.Utility>GetIndexOf<struct\u0020Decoration>(Decoration.static_decorations, current2)));
            streamWriter2.Write(" ");
          }
          streamWriter2.Write(Convert.ToString(current1.extra_skills.Count));
          streamWriter2.Write(" ");
          List<Skill>.Enumerator enumerator4 = current1.extra_skills.GetEnumerator();
          while (enumerator4.MoveNext())
          {
            Skill current2 = enumerator4.Current;
            streamWriter2.Write(Convert.ToString(<Module>.Utility>GetIndexOf<struct\u0020Skill>(Skill.static_skills, current2)));
            streamWriter2.Write(" ");
          }
          if (current1.charm != null)
          {
            streamWriter2.Write(Convert.ToString(current1.charm.num_slots));
            streamWriter2.Write(" ");
            streamWriter2.Write(Convert.ToString(current1.charm.abilities.Count));
            streamWriter2.Write(" ");
            List<AbilityPair>.Enumerator enumerator5 = current1.charm.abilities.GetEnumerator();
            while (enumerator5.MoveNext())
            {
              AbilityPair current2 = enumerator5.Current;
              streamWriter2.Write(Convert.ToString(current2.amount));
              streamWriter2.Write(" ");
              streamWriter2.Write(Convert.ToString(<Module>.Utility>GetIndexOf<struct\u0020Ability>(Ability.static_abilities, current2.ability)));
              streamWriter2.Write(" ");
            }
          }
          streamWriter2.WriteLine();
        }
        streamWriter2.Close();
      }
      __fault
      {
        streamWriter2.Dispose();
      }
      streamWriter2.Dispose();
    }

    public int SearchIndexMap(Dictionary<uint, uint> imap, int skill_index)
    {
      Dictionary<uint, uint>.Enumerator enumerator = imap.GetEnumerator();
      int num;
      while (enumerator.MoveNext())
      {
        KeyValuePair<uint, uint> current = enumerator.Current;
        if ((int) current.Value == skill_index)
        {
          num = (int) current.Key;
          goto label_5;
        }
      }
      num = -1;
label_5:
      return num;
    }

    private void \u007EForm1()
    {
      this.SaveConfig();
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.nudHR = new NumericUpDown();
      this.groupBox1 = new GroupBox();
      this.lblWhatIsCharmTable = new Label();
      this.lblCharmTable = new Label();
      this.cmbCharmTable = new ComboBox();
      this.nudWeaponSlots = new NumericUpDown();
      this.lblElder = new Label();
      this.lblSlots = new Label();
      this.nudElder = new NumericUpDown();
      this.lblHR = new Label();
      this.grpBSkills = new GroupBox();
      this.btnSearch = new Button();
      this.progressBar1 = new ProgressBar();
      this.txtSolutions = new RichTextBox();
      Form1 form1_1 = this;
      ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip(form1_1.components);
      form1_1.cmsSolutions = contextMenuStrip1;
      this.groupBox4 = new GroupBox();
      this.btnAdvancedSearch = new Button();
      this.btnCancel = new Button();
      this.grpResults = new GroupBox();
      this.btnCharms = new Button();
      this.grpBSkillFilters = new GroupBox();
      this.menuStrip1 = new MenuStrip();
      this.mnuFile = new ToolStripMenuItem();
      this.mnuLoadData = new ToolStripMenuItem();
      this.mnuSaveData = new ToolStripMenuItem();
      this.mnuExit = new ToolStripMenuItem();
      this.mnuOptions = new ToolStripMenuItem();
      this.mnuClearSettings = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.mnuMaxResults = new ToolStripMenuItem();
      this.mnuNumResults = new ToolStripTextBox();
      this.mnuAllowBadSkills = new ToolStripMenuItem();
      this.mnuAllowPiercings = new ToolStripMenuItem();
      this.mnuAllowEventArmor = new ToolStripMenuItem();
      this.mnuAllowJapaneseOnlyDLC = new ToolStripMenuItem();
      this.mnuAllowLowerTierArmor = new ToolStripMenuItem();
      this.mnuPrintDecoNames = new ToolStripMenuItem();
      this.mnuSortSkillsAlphabetically = new ToolStripMenuItem();
      this.mnuLanguage = new ToolStripMenuItem();
      this.mnuHelp = new ToolStripMenuItem();
      this.mnuCheckForUpdates = new ToolStripMenuItem();
      this.mnuAbout = new ToolStripMenuItem();
      this.groupBox6 = new GroupBox();
      this.rdoFemale = new RadioButton();
      this.rdoMale = new RadioButton();
      this.tabHunterType = new TabControl();
      this.tabBlademaster = new TabPage();
      this.tabGunner = new TabPage();
      this.grpGSkillFilters = new GroupBox();
      this.grpGSkills = new GroupBox();
      this.grpSort = new GroupBox();
      this.cmbSort = new ComboBox();
      this.grpCharmFilter = new GroupBox();
      this.cmbCharms = new ComboBox();
      this.grpCharms = new GroupBox();
      this.btnImport = new Button();
      this.cmbCharmSelect = new ComboBox();
      Form1 form1_2 = this;
      ContextMenuStrip contextMenuStrip2 = new ContextMenuStrip(form1_2.components);
      form1_2.cmsCharms = contextMenuStrip2;
      this.nudHR.BeginInit();
      this.groupBox1.SuspendLayout();
      this.nudWeaponSlots.BeginInit();
      this.nudElder.BeginInit();
      this.groupBox4.SuspendLayout();
      this.grpResults.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.tabHunterType.SuspendLayout();
      this.tabBlademaster.SuspendLayout();
      this.tabGunner.SuspendLayout();
      this.grpSort.SuspendLayout();
      this.grpCharmFilter.SuspendLayout();
      this.grpCharms.SuspendLayout();
      this.SuspendLayout();
      this.nudHR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.nudHR.Location = new Point(109, 20);
      this.nudHR.Maximum = new Decimal(new int[4]
      {
        8,
        0,
        0,
        0
      });
      this.nudHR.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.nudHR.Name = "nudHR";
      this.nudHR.Size = new Size(35, 20);
      this.nudHR.TabIndex = 2;
      this.nudHR.Value = new Decimal(new int[4]
      {
        8,
        0,
        0,
        0
      });
      this.nudHR.ValueChanged += new EventHandler(this.HRChanged);
      this.groupBox1.Controls.Add((Control) this.lblWhatIsCharmTable);
      this.groupBox1.Controls.Add((Control) this.lblCharmTable);
      this.groupBox1.Controls.Add((Control) this.cmbCharmTable);
      this.groupBox1.Controls.Add((Control) this.nudWeaponSlots);
      this.groupBox1.Controls.Add((Control) this.nudHR);
      this.groupBox1.Controls.Add((Control) this.lblElder);
      this.groupBox1.Controls.Add((Control) this.lblSlots);
      this.groupBox1.Controls.Add((Control) this.nudElder);
      this.groupBox1.Controls.Add((Control) this.lblHR);
      this.groupBox1.Location = new Point(12, 27);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(154, 140);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.lblWhatIsCharmTable.AutoSize = true;
      this.lblWhatIsCharmTable.BackColor = SystemColors.Control;
      this.lblWhatIsCharmTable.Cursor = Cursors.Hand;
      this.lblWhatIsCharmTable.ForeColor = SystemColors.HotTrack;
      this.lblWhatIsCharmTable.Location = new Point(75, 111);
      this.lblWhatIsCharmTable.Name = "lblWhatIsCharmTable";
      this.lblWhatIsCharmTable.Size = new Size(13, 13);
      this.lblWhatIsCharmTable.TabIndex = 9;
      this.lblWhatIsCharmTable.Text = "?";
      this.lblWhatIsCharmTable.Click += new EventHandler(this.lblWhatIsCharmTable_Click);
      this.lblCharmTable.AutoSize = true;
      this.lblCharmTable.Location = new Point(11, 111);
      this.lblCharmTable.Name = "lblCharmTable";
      this.lblCharmTable.Size = new Size(67, 13);
      this.lblCharmTable.TabIndex = 8;
      this.lblCharmTable.Text = "Charm Table";
      this.cmbCharmTable.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbCharmTable.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharmTable.FormattingEnabled = true;
      this.cmbCharmTable.Items.AddRange(new object[19]
      {
        (object) "???",
        (object) "   1",
        (object) "   2",
        (object) "   3",
        (object) "   4",
        (object) "   5",
        (object) "   6",
        (object) "   7",
        (object) "   8",
        (object) "   9",
        (object) "  10",
        (object) "  11",
        (object) "  12",
        (object) "  13",
        (object) "  14",
        (object) "  15",
        (object) "  16",
        (object) "  17",
        (object) "All"
      });
      this.cmbCharmTable.Location = new Point(101, 108);
      this.cmbCharmTable.Name = "cmbCharmTable";
      this.cmbCharmTable.Size = new Size(43, 21);
      this.cmbCharmTable.TabIndex = 1;
      this.cmbCharmTable.SelectedIndexChanged += new EventHandler(this.cmbCharmTable_SelectedIndexChanged);
      this.nudWeaponSlots.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.nudWeaponSlots.Location = new Point(109, 80);
      this.nudWeaponSlots.Maximum = new Decimal(new int[4]
      {
        3,
        0,
        0,
        0
      });
      this.nudWeaponSlots.Name = "nudWeaponSlots";
      this.nudWeaponSlots.Size = new Size(35, 20);
      this.nudWeaponSlots.TabIndex = 7;
      this.lblElder.AutoSize = true;
      this.lblElder.Location = new Point(11, 52);
      this.lblElder.Name = "lblElder";
      this.lblElder.Size = new Size(74, 13);
      this.lblElder.TabIndex = 4;
      this.lblElder.Text = "Village Quests";
      this.lblSlots.AutoSize = true;
      this.lblSlots.Location = new Point(11, 82);
      this.lblSlots.Name = "lblSlots";
      this.lblSlots.Size = new Size(97, 13);
      this.lblSlots.TabIndex = 1;
      this.lblSlots.Text = "Max Weapon Slots";
      this.nudElder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.nudElder.Location = new Point(109, 50);
      this.nudElder.Maximum = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.nudElder.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.nudElder.Name = "nudElder";
      this.nudElder.Size = new Size(35, 20);
      this.nudElder.TabIndex = 5;
      this.nudElder.Value = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.nudElder.ValueChanged += new EventHandler(this.DeleteOptions);
      this.lblHR.AutoSize = true;
      this.lblHR.Location = new Point(11, 22);
      this.lblHR.Name = "lblHR";
      this.lblHR.Size = new Size(75, 13);
      this.lblHR.TabIndex = 6;
      this.lblHR.Text = "Harbor Quests";
      this.grpBSkills.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpBSkills.Location = new Point(6, 8);
      this.grpBSkills.Name = "grpBSkills";
      this.grpBSkills.Size = new Size(183, 182);
      this.grpBSkills.TabIndex = 8;
      this.grpBSkills.TabStop = false;
      this.grpBSkills.Text = "Skills";
      this.btnSearch.Location = new Point(6, 12);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(107, 27);
      this.btnSearch.TabIndex = 9;
      this.btnSearch.Text = "&Quick Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.progressBar1.Location = new Point(14, 492);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(353, 10);
      this.progressBar1.Step = 1;
      this.progressBar1.TabIndex = 10;
      this.txtSolutions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtSolutions.ContextMenuStrip = this.cmsSolutions;
      this.txtSolutions.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtSolutions.Location = new Point(6, 16);
      this.txtSolutions.Name = "txtSolutions";
      this.txtSolutions.ReadOnly = true;
      this.txtSolutions.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.txtSolutions.Size = new Size(332, 451);
      this.txtSolutions.TabIndex = 11;
      this.txtSolutions.Text = "";
      this.txtSolutions.KeyDown += new KeyEventHandler(this.KeyDown);
      this.cmsSolutions.Name = "contextMenuStrip1";
      this.cmsSolutions.Size = new Size(61, 4);
      this.cmsSolutions.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
      this.groupBox4.Controls.Add((Control) this.btnAdvancedSearch);
      this.groupBox4.Controls.Add((Control) this.btnCancel);
      this.groupBox4.Controls.Add((Control) this.btnSearch);
      this.groupBox4.Location = new Point(14, 440);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(353, 46);
      this.groupBox4.TabIndex = 14;
      this.groupBox4.TabStop = false;
      this.btnAdvancedSearch.Location = new Point(119, 12);
      this.btnAdvancedSearch.Name = "btnAdvancedSearch";
      this.btnAdvancedSearch.Size = new Size(137, 27);
      this.btnAdvancedSearch.TabIndex = 11;
      this.btnAdvancedSearch.Text = "&Advanced Search";
      this.btnAdvancedSearch.UseVisualStyleBackColor = true;
      this.btnAdvancedSearch.Click += new EventHandler(this.btnAdvancedSearch_Click);
      this.btnCancel.Location = new Point(262, 12);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(85, 27);
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.grpResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpResults.Controls.Add((Control) this.txtSolutions);
      this.grpResults.Location = new Point(373, 27);
      this.grpResults.Name = "grpResults";
      this.grpResults.Size = new Size(344, 477);
      this.grpResults.TabIndex = 15;
      this.grpResults.TabStop = false;
      this.grpResults.Text = "Results";
      this.btnCharms.Location = new Point(5, 46);
      this.btnCharms.Name = "btnCharms";
      this.btnCharms.Size = new Size(89, 23);
      this.btnCharms.TabIndex = 25;
      this.btnCharms.Text = "&My Charms";
      this.btnCharms.UseVisualStyleBackColor = true;
      this.btnCharms.Click += new EventHandler(this.btnCharms_Click);
      this.grpBSkillFilters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpBSkillFilters.Location = new Point(195, 8);
      this.grpBSkillFilters.Name = "grpBSkillFilters";
      this.grpBSkillFilters.Size = new Size(146, 182);
      this.grpBSkillFilters.TabIndex = 9;
      this.grpBSkillFilters.TabStop = false;
      this.grpBSkillFilters.Text = "Skill Filters";
      this.menuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.mnuFile,
        (ToolStripItem) this.mnuOptions,
        (ToolStripItem) this.mnuLanguage,
        (ToolStripItem) this.mnuHelp
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(729, 24);
      this.menuStrip1.TabIndex = 16;
      this.menuStrip1.Text = "menuStrip1";
      this.mnuFile.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.mnuLoadData,
        (ToolStripItem) this.mnuSaveData,
        (ToolStripItem) this.mnuExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new Size(37, 20);
      this.mnuFile.Text = "&File";
      this.mnuLoadData.Name = "mnuLoadData";
      this.mnuLoadData.Size = new Size(100, 22);
      this.mnuLoadData.Text = "&Load";
      this.mnuLoadData.Click += new EventHandler(this.mnuLoad_Click);
      this.mnuSaveData.Name = "mnuSaveData";
      this.mnuSaveData.Size = new Size(100, 22);
      this.mnuSaveData.Text = "&Save";
      this.mnuSaveData.Click += new EventHandler(this.mnuSave_Click);
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new Size(100, 22);
      this.mnuExit.Text = "E&xit";
      this.mnuExit.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.mnuOptions.DropDownItems.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.mnuClearSettings,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.mnuMaxResults,
        (ToolStripItem) this.mnuAllowBadSkills,
        (ToolStripItem) this.mnuAllowPiercings,
        (ToolStripItem) this.mnuAllowEventArmor,
        (ToolStripItem) this.mnuAllowJapaneseOnlyDLC,
        (ToolStripItem) this.mnuAllowLowerTierArmor,
        (ToolStripItem) this.mnuPrintDecoNames,
        (ToolStripItem) this.mnuSortSkillsAlphabetically
      });
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new Size(61, 20);
      this.mnuOptions.Text = "&Options";
      this.mnuClearSettings.Name = "mnuClearSettings";
      this.mnuClearSettings.Size = new Size(209, 22);
      this.mnuClearSettings.Text = "&Clear Settings";
      this.mnuClearSettings.Click += new EventHandler(this.toolStripMenuItem1_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(206, 6);
      this.mnuMaxResults.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.mnuNumResults
      });
      this.mnuMaxResults.Name = "mnuMaxResults";
      this.mnuMaxResults.Size = new Size(209, 22);
      this.mnuMaxResults.Text = "&Max Results";
      this.mnuNumResults.Name = "mnuNumResults";
      this.mnuNumResults.Size = new Size(100, 23);
      this.mnuNumResults.Text = "1000";
      this.mnuAllowBadSkills.CheckOnClick = true;
      this.mnuAllowBadSkills.Name = "mnuAllowBadSkills";
      this.mnuAllowBadSkills.Size = new Size(209, 22);
      this.mnuAllowBadSkills.Text = "Allow &Bad Skills";
      this.mnuAllowBadSkills.Click += new EventHandler(this.OptionsChanged);
      this.mnuAllowPiercings.CheckOnClick = true;
      this.mnuAllowPiercings.Name = "mnuAllowPiercings";
      this.mnuAllowPiercings.Size = new Size(209, 22);
      this.mnuAllowPiercings.Text = "Allow &Piercings";
      this.mnuAllowPiercings.Click += new EventHandler(this.OptionsChanged);
      this.mnuAllowEventArmor.Checked = true;
      this.mnuAllowEventArmor.CheckOnClick = true;
      this.mnuAllowEventArmor.CheckState = CheckState.Checked;
      this.mnuAllowEventArmor.Name = "mnuAllowEventArmor";
      this.mnuAllowEventArmor.Size = new Size(209, 22);
      this.mnuAllowEventArmor.Text = "Allow &Event Armor";
      this.mnuAllowEventArmor.Click += new EventHandler(this.OptionsChanged);
      this.mnuAllowJapaneseOnlyDLC.CheckOnClick = true;
      this.mnuAllowJapaneseOnlyDLC.Name = "mnuAllowJapaneseOnlyDLC";
      this.mnuAllowJapaneseOnlyDLC.Size = new Size(209, 22);
      this.mnuAllowJapaneseOnlyDLC.Text = "Allow &Japanese-Only DLC";
      this.mnuAllowLowerTierArmor.Checked = true;
      this.mnuAllowLowerTierArmor.CheckOnClick = true;
      this.mnuAllowLowerTierArmor.CheckState = CheckState.Checked;
      this.mnuAllowLowerTierArmor.Name = "mnuAllowLowerTierArmor";
      this.mnuAllowLowerTierArmor.Size = new Size(209, 22);
      this.mnuAllowLowerTierArmor.Text = "Allow &Lower Tier Armor";
      this.mnuAllowLowerTierArmor.Click += new EventHandler(this.OptionsChanged);
      this.mnuPrintDecoNames.Checked = true;
      this.mnuPrintDecoNames.CheckOnClick = true;
      this.mnuPrintDecoNames.CheckState = CheckState.Checked;
      this.mnuPrintDecoNames.Name = "mnuPrintDecoNames";
      this.mnuPrintDecoNames.Size = new Size(209, 22);
      this.mnuPrintDecoNames.Text = "Print &Decoration Names";
      this.mnuPrintDecoNames.Click += new EventHandler(this.OptionsChanged);
      this.mnuSortSkillsAlphabetically.Checked = true;
      this.mnuSortSkillsAlphabetically.CheckOnClick = true;
      this.mnuSortSkillsAlphabetically.CheckState = CheckState.Checked;
      this.mnuSortSkillsAlphabetically.Name = "mnuSortSkillsAlphabetically";
      this.mnuSortSkillsAlphabetically.Size = new Size(209, 22);
      this.mnuSortSkillsAlphabetically.Text = "&Sort Skills Alphabetically";
      this.mnuSortSkillsAlphabetically.Click += new EventHandler(this.OptionsChanged);
      this.mnuLanguage.Name = "mnuLanguage";
      this.mnuLanguage.Size = new Size(71, 20);
      this.mnuLanguage.Text = "&Language";
      this.mnuHelp.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.mnuCheckForUpdates,
        (ToolStripItem) this.mnuAbout
      });
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new Size(44, 20);
      this.mnuHelp.Text = "&Help";
      this.mnuCheckForUpdates.Name = "mnuCheckForUpdates";
      this.mnuCheckForUpdates.Size = new Size(171, 22);
      this.mnuCheckForUpdates.Text = "Check for &Updates";
      this.mnuCheckForUpdates.Click += new EventHandler(this.UpdateMenuItem_Click);
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new Size(171, 22);
      this.mnuAbout.Text = "&About";
      this.mnuAbout.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.groupBox6.Controls.Add((Control) this.rdoFemale);
      this.groupBox6.Controls.Add((Control) this.rdoMale);
      this.groupBox6.Location = new Point(12, 166);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(154, 40);
      this.groupBox6.TabIndex = 19;
      this.groupBox6.TabStop = false;
      this.rdoFemale.AutoSize = true;
      this.rdoFemale.Location = new Point(78, 15);
      this.rdoFemale.Name = "rdoFemale";
      this.rdoFemale.Size = new Size(59, 17);
      this.rdoFemale.TabIndex = 18;
      this.rdoFemale.TabStop = true;
      this.rdoFemale.Text = "Female";
      this.rdoFemale.UseVisualStyleBackColor = true;
      this.rdoFemale.CheckedChanged += new EventHandler(this.DeleteOptions);
      this.rdoMale.AutoSize = true;
      this.rdoMale.Checked = true;
      this.rdoMale.Location = new Point(15, 15);
      this.rdoMale.Name = "rdoMale";
      this.rdoMale.Size = new Size(48, 17);
      this.rdoMale.TabIndex = 0;
      this.rdoMale.TabStop = true;
      this.rdoMale.Text = "Male";
      this.rdoMale.UseVisualStyleBackColor = true;
      this.rdoMale.CheckedChanged += new EventHandler(this.DeleteOptions);
      this.tabHunterType.Controls.Add((Control) this.tabBlademaster);
      this.tabHunterType.Controls.Add((Control) this.tabGunner);
      this.tabHunterType.Location = new Point(12, 212);
      this.tabHunterType.Name = "tabHunterType";
      this.tabHunterType.SelectedIndex = 0;
      this.tabHunterType.Size = new Size(355, 222);
      this.tabHunterType.TabIndex = 24;
      this.tabBlademaster.BackColor = SystemColors.Control;
      this.tabBlademaster.Controls.Add((Control) this.grpBSkills);
      this.tabBlademaster.Controls.Add((Control) this.grpBSkillFilters);
      this.tabBlademaster.Location = new Point(4, 22);
      this.tabBlademaster.Name = "tabBlademaster";
      this.tabBlademaster.Padding = new Padding(3);
      this.tabBlademaster.Size = new Size(347, 196);
      this.tabBlademaster.TabIndex = 0;
      this.tabBlademaster.Text = "Blademaster";
      this.tabGunner.BackColor = SystemColors.Control;
      this.tabGunner.Controls.Add((Control) this.grpGSkillFilters);
      this.tabGunner.Controls.Add((Control) this.grpGSkills);
      this.tabGunner.Location = new Point(4, 22);
      this.tabGunner.Name = "tabGunner";
      this.tabGunner.Padding = new Padding(3);
      this.tabGunner.Size = new Size(347, 196);
      this.tabGunner.TabIndex = 1;
      this.tabGunner.Text = "Gunner";
      this.grpGSkillFilters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpGSkillFilters.Location = new Point(195, 8);
      this.grpGSkillFilters.Name = "grpGSkillFilters";
      this.grpGSkillFilters.Size = new Size(146, 182);
      this.grpGSkillFilters.TabIndex = 10;
      this.grpGSkillFilters.TabStop = false;
      this.grpGSkillFilters.Text = "Skill Filters";
      this.grpGSkills.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpGSkills.Location = new Point(6, 8);
      this.grpGSkills.Name = "grpGSkills";
      this.grpGSkills.Size = new Size(183, 182);
      this.grpGSkills.TabIndex = 9;
      this.grpGSkills.TabStop = false;
      this.grpGSkills.Text = "Skills";
      this.grpSort.Controls.Add((Control) this.cmbSort);
      this.grpSort.Location = new Point(172, 27);
      this.grpSort.Name = "grpSort";
      this.grpSort.Size = new Size(193, 45);
      this.grpSort.TabIndex = 0;
      this.grpSort.TabStop = false;
      this.grpSort.Text = "Sort Results By";
      this.cmbSort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSort.FormattingEnabled = true;
      this.cmbSort.Items.AddRange(new object[13]
      {
        (object) "None",
        (object) "Dragon res",
        (object) "Fire res",
        (object) "Ice res",
        (object) "Thunder res",
        (object) "Water res",
        (object) "Base defence",
        (object) "Max defence",
        (object) "Difficulty",
        (object) "Rarity",
        (object) "Slots spare",
        (object) "Family",
        (object) "Extra Skills"
      });
      this.cmbSort.Location = new Point(6, 16);
      this.cmbSort.Name = "cmbSort";
      this.cmbSort.Size = new Size(181, 21);
      this.cmbSort.TabIndex = 0;
      this.cmbSort.SelectedIndexChanged += new EventHandler(this.cmbSort_SelectedIndexChanged);
      this.grpCharmFilter.Controls.Add((Control) this.cmbCharms);
      this.grpCharmFilter.Location = new Point(172, 78);
      this.grpCharmFilter.Name = "grpCharmFilter";
      this.grpCharmFilter.Size = new Size(193, 45);
      this.grpCharmFilter.TabIndex = 1;
      this.grpCharmFilter.TabStop = false;
      this.grpCharmFilter.Text = "Filter Results by Charm";
      this.cmbCharms.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbCharms.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharms.FormattingEnabled = true;
      this.cmbCharms.Items.AddRange(new object[2]
      {
        (object) "None",
        (object) "All"
      });
      this.cmbCharms.Location = new Point(6, 16);
      this.cmbCharms.Name = "cmbCharms";
      this.cmbCharms.Size = new Size(181, 21);
      this.cmbCharms.TabIndex = 0;
      this.cmbCharms.SelectedIndexChanged += new EventHandler(this.cmbCharms_SelectedIndexChanged);
      this.grpCharms.Controls.Add((Control) this.btnImport);
      this.grpCharms.Controls.Add((Control) this.cmbCharmSelect);
      this.grpCharms.Controls.Add((Control) this.btnCharms);
      this.grpCharms.Location = new Point(172, 129);
      this.grpCharms.Name = "grpCharms";
      this.grpCharms.Size = new Size(193, 77);
      this.grpCharms.TabIndex = 25;
      this.grpCharms.TabStop = false;
      this.grpCharms.Text = "Charms";
      this.btnImport.Location = new Point(99, 46);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new Size(89, 23);
      this.btnImport.TabIndex = 26;
      this.btnImport.Text = "&Import";
      this.btnImport.UseVisualStyleBackColor = true;
      this.btnImport.Click += new EventHandler(this.btnImport_Click);
      this.cmbCharmSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cmbCharmSelect.ContextMenuStrip = this.cmsCharms;
      this.cmbCharmSelect.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharmSelect.FormattingEnabled = true;
      this.cmbCharmSelect.Items.AddRange(new object[5]
      {
        (object) "Use no charms",
        (object) "Use my charms",
        (object) "Use only slotted charms",
        (object) "Use up to one skill charms",
        (object) "Use up to two skill charms"
      });
      this.cmbCharmSelect.Location = new Point(6, 19);
      this.cmbCharmSelect.Name = "cmbCharmSelect";
      this.cmbCharmSelect.Size = new Size(181, 21);
      this.cmbCharmSelect.TabIndex = 1;
      this.cmsCharms.Name = "cmsCharms";
      this.cmsCharms.Size = new Size(61, 4);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(729, 509);
      this.Controls.Add((Control) this.grpCharmFilter);
      this.Controls.Add((Control) this.grpSort);
      this.Controls.Add((Control) this.grpResults);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Controls.Add((Control) this.grpCharms);
      this.Controls.Add((Control) this.groupBox6);
      this.Controls.Add((Control) this.tabHunterType);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.progressBar1);
      this.Name = "Form1";
      this.Text = "Athena's ASS for MH3G and MH3U";
      this.nudHR.EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.nudWeaponSlots.EndInit();
      this.nudElder.EndInit();
      this.groupBox4.ResumeLayout(false);
      this.grpResults.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.tabHunterType.ResumeLayout(false);
      this.tabBlademaster.ResumeLayout(false);
      this.tabGunner.ResumeLayout(false);
      this.grpSort.ResumeLayout(false);
      this.grpCharmFilter.ResumeLayout(false);
      this.grpCharms.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void FormulateQuery([MarshalAs(UnmanagedType.U1)] bool danger, [MarshalAs(UnmanagedType.U1)] bool use_gunner_skills)
    {
      this.query = new Query();
      for (int index = 0; index < 5; ++index)
      {
        this.query.rel_armor.Add(new List<Armor>());
        this.query.inf_armor.Add(new List<Armor>());
      }
      this.query.weapon_slots_allowed = (uint) (int) this.nudWeaponSlots.Value;
      this.query.elder_star = (uint) (int) this.nudElder.Value;
      this.query.hr = (uint) (int) this.nudHR.Value;
      this.query.charm_table = (uint) this.cmbCharmTable.SelectedIndex;
      this.query.gender = !this.rdoMale.Checked ? Gender.FEMALE : Gender.MALE;
      this.query.hunter_type = use_gunner_skills ? HunterType.GUNNER : HunterType.BLADEMASTER;
      this.query.include_piercings = this.mnuAllowPiercings.Checked;
      this.query.allow_bad = this.mnuAllowBadSkills.Checked;
      this.query.allow_event = this.mnuAllowEventArmor.Checked;
      this.query.allow_japs = this.mnuAllowJapaneseOnlyDLC.Checked;
      this.query.allow_lower_tier = this.mnuAllowLowerTierArmor.Checked;
      if (!use_gunner_skills)
      {
        for (uint index = 0U; index < 6U; ++index)
        {
          if (this.bSkills[(int) index].SelectedIndex < 0)
            continue;
          this.query.skills.Add(this.data.FindSkill(this.bIndexMaps[(int) index][(uint) this.bSkills[(int) index].SelectedIndex]));
        }
      }
      else
      {
        for (uint index = 0U; index < 6U; ++index)
        {
          if (this.gSkills[(int) index].SelectedIndex < 0)
            continue;
          this.query.skills.Add(this.data.FindSkill(this.gIndexMaps[(int) index][(uint) this.gSkills[(int) index].SelectedIndex]));
        }
      }
      this.data.GetRelevantData(this.query);
    }

    private void MaxResultsTextBoxKeyPress(object sender, KeyPressEventArgs e)
    {
      if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        return;
      e.Handled = true;
    }

    private void MaxResultsTextChanged(object sender, EventArgs e)
    {
      try
      {
        Form1 form1 = this;
        int num = (int) Convert.ToUInt32(form1.mnuNumResults.Text);
        form1.MAX_LIMIT = num;
      }
      catch (Exception ex)
      {
        return;
      }
      this.SaveConfig();
    }

    private void OptionsChanged(object sender, EventArgs e)
    {
      this.SaveConfig();
      if (sender != this.mnuAllowBadSkills)
        this.DeleteOptions(sender, e);
      if (sender == this.mnuPrintDecoNames)
        this.cmbCharms_SelectedIndexChanged((object) null, (EventArgs) null);
      if (sender != this.mnuSortSkillsAlphabetically)
        return;
      List<ComboBox>.Enumerator enumerator1 = this.bSkillFilters.GetEnumerator();
      while (enumerator1.MoveNext())
        this.cmbSkillFilter_SelectedIndexChanged((object) enumerator1.Current, (EventArgs) null);
      List<ComboBox>.Enumerator enumerator2 = this.gSkillFilters.GetEnumerator();
      while (enumerator2.MoveNext())
        this.cmbSkillFilter_SelectedIndexChanged((object) enumerator2.Current, (EventArgs) null);
    }

    private void QueueTask(Query query, Charm ct)
    {
      BackgroundWorker backgroundWorker = new BackgroundWorker();
      backgroundWorker.WorkerSupportsCancellation = true;
      backgroundWorker.WorkerReportsProgress = true;
      backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.worker_data.Add(new ThreadSearchData()
      {
        charm_template = ct,
        query = query
      });
      this.workers.Add(backgroundWorker);
    }

    private void EnableSearchControls([MarshalAs(UnmanagedType.U1)] bool enabled)
    {
      this.btnSearch.Enabled = enabled;
      this.btnAdvancedSearch.Enabled = enabled;
      this.btnCancel.Enabled = !enabled;
      this.mnuMaxResults.Enabled = enabled;
      this.mnuNumResults.Enabled = enabled;
      this.cmbSort.Enabled = enabled;
      this.cmbCharms.Enabled = enabled;
    }

    private void StartTasks()
    {
      Form1.worker_mutex.WaitOne();
      if (this.workers.Count > 0)
      {
        this.worker_start_index = (uint) Math.Max(1, Math.Min(Environment.ProcessorCount, this.workers.Count));
        for (uint index = 0U; index < this.worker_start_index; ++index)
          this.workers[(int) index].RunWorkerAsync((object) this.worker_data[(int) index]);
      }
      Form1.worker_mutex.ReleaseMutex();
    }

    private void StartSearch()
    {
      this.progressBar1.Value = 0;
      this.total_progress = 0U;
      this.num_updates = 0U;
      this.search_cancelled = false;
      if (this.query.skills.Count <= 0)
        return;
      this.existing_armor.Clear();
      this.EnableSearchControls(false);
      this.charm_solution_map.Clear();
      this.cmbCharms.SelectedIndex = 1;
      this.txtSolutions.Text = StringTable.text[81] + " " + "0";
      this.final_solutions.Clear();
      this.all_solutions.Clear();
      this.no_charm_solutions.Clear();
      this.workers.Clear();
      this.worker_data.Clear();
      this.last_result = (string) null;
      this.last_search_gunner = this.tabHunterType.SelectedIndex == 1;
      this.finished_workers = 0U;
      if (this.cmbCharmSelect.SelectedIndex > 1)
      {
        List<Ability>.Enumerator enumerator1 = this.query.rel_abilities.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          Ability current1 = enumerator1.Current;
          if (current1.auto_guard)
          {
            List<Charm>.Enumerator enumerator2 = CharmDatabase.mycharms.GetEnumerator();
            while (enumerator2.MoveNext())
            {
              Charm current2 = enumerator2.Current;
              if (current2.abilities.Count == 1 && current2.abilities[0].ability == current1)
              {
                Form1 form1 = this;
                Query query = form1.query;
                Charm ct = current2;
                form1.QueueTask(query, ct);
                this.StartTasks();
                break;
              }
            }
            return;
          }
        }
      }
      switch (this.cmbCharmSelect.SelectedIndex)
      {
        case 0:
          Form1 form1_1 = this;
          Query query1 = form1_1.query;
          // ISSUE: variable of the null type
          __Null local1 = null;
          form1_1.QueueTask(query1, (Charm) local1);
          break;
        case 1:
          List<Charm> lst = new List<Charm>();
          List<Charm>.Enumerator enumerator3 = CharmDatabase.mycharms.GetEnumerator();
          while (enumerator3.MoveNext())
          {
            Charm current = enumerator3.Current;
            Charm.AddToOptimalList(lst, current);
          }
          List<Charm>.Enumerator enumerator4 = lst.GetEnumerator();
          while (enumerator4.MoveNext())
          {
            Charm current = enumerator4.Current;
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = current;
            form1_2.QueueTask(query2, ct);
          }
          break;
        case 2:
          Form1 form1_3 = this;
          Query query3 = form1_3.query;
          // ISSUE: variable of the null type
          __Null local2 = null;
          form1_3.QueueTask(query3, (Charm) local2);
          if (((int) CharmDatabase.have_slots[this.cmbCharmTable.SelectedIndex, 1] & 2) != 0)
          {
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = new Charm(1U);
            form1_2.QueueTask(query2, ct);
          }
          if ((this.query.elder_star > 6U || this.query.hr >= 3U) && ((int) CharmDatabase.have_slots[this.cmbCharmTable.SelectedIndex, 2] & 4) != 0)
          {
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = new Charm(2U);
            form1_2.QueueTask(query2, ct);
          }
          if ((this.query.elder_star > 8U || this.query.hr >= 4U) && ((int) CharmDatabase.have_slots[this.cmbCharmTable.SelectedIndex, 3] & 8) != 0)
          {
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = new Charm(3U);
            form1_2.QueueTask(query2, ct);
            break;
          }
          else
            break;
        case 3:
          List<Charm>.Enumerator enumerator5 = CharmDatabase.GetCharms(this.query, false).GetEnumerator();
          while (enumerator5.MoveNext())
          {
            Charm current = enumerator5.Current;
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = current;
            form1_2.QueueTask(query2, ct);
          }
          break;
        case 4:
          List<Charm>.Enumerator enumerator6 = CharmDatabase.GetCharms(this.query, true).GetEnumerator();
          while (enumerator6.MoveNext())
          {
            Charm current = enumerator6.Current;
            Form1 form1_2 = this;
            Query query2 = form1_2.query;
            Charm ct = current;
            form1_2.QueueTask(query2, ct);
          }
          break;
      }
      if (this.workers.Count == 0)
      {
        Form1 form1_2 = this;
        Query query2 = form1_2.query;
        // ISSUE: variable of the null type
        __Null local3 = null;
        form1_2.QueueTask(query2, (Charm) local3);
      }
      this.StartTasks();
    }

    private void btnAdvancedSearch_Click(object sender, EventArgs e)
    {
      this.FormulateQuery(true, this.tabHunterType.SelectedIndex == 1);
      frmAdvanced frmAdvanced1 = new frmAdvanced(this.query);
      frmAdvanced frmAdvanced2;
      // ISSUE: fault handler
      try
      {
        frmAdvanced2 = frmAdvanced1;
        frmAdvanced2.Width = this.adv_x;
        frmAdvanced2.Height = this.adv_y;
        if (this.tabHunterType.SelectedIndex == 0)
        {
          frmAdvanced2.CheckResult(this.blast_options);
          int num = (int) frmAdvanced2.ShowDialog((IWin32Window) this);
          this.blast_options = frmAdvanced2.result;
          goto label_7;
        }
        else if (this.tabHunterType.SelectedIndex == 1)
        {
          frmAdvanced2.CheckResult(this.glast_options);
          int num = (int) frmAdvanced2.ShowDialog((IWin32Window) this);
          this.glast_options = frmAdvanced2.result;
          goto label_7;
        }
      }
      __fault
      {
        frmAdvanced2.Dispose();
      }
      frmAdvanced2.Dispose();
      return;
label_7:
      // ISSUE: fault handler
      try
      {
        this.adv_x = frmAdvanced2.Width;
        this.adv_y = frmAdvanced2.Height;
        this.SaveConfig();
        if (frmAdvanced2.DialogResult == DialogResult.OK)
          goto label_11;
      }
      __fault
      {
        frmAdvanced2.Dispose();
      }
      frmAdvanced2.Dispose();
      return;
label_11:
      // ISSUE: fault handler
      try
      {
        for (int index1 = 0; index1 < 5; ++index1)
        {
          this.query.rel_armor[index1].Clear();
          for (int index2 = 0; index2 < this.query.inf_armor[index1].Count; ++index2)
          {
            if (!frmAdvanced2.boxes[index1].Items[index2].Checked)
              continue;
            this.query.rel_armor[index1].Add(this.query.inf_armor[index1][index2]);
          }
        }
        this.query.rel_decorations.Clear();
        for (int index = 0; index < this.query.inf_decorations.Count; ++index)
        {
          if (!frmAdvanced2.boxes[5].Items[index].Checked)
            continue;
          this.query.rel_decorations.Add(this.query.inf_decorations[index]);
        }
        this.StartSearch();
      }
      __fault
      {
        frmAdvanced2.Dispose();
      }
      frmAdvanced2.Dispose();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.FormulateQuery(false, this.tabHunterType.SelectedIndex == 1);
      this.StartSearch();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.search_cancelled = true;
      List<BackgroundWorker>.Enumerator enumerator = this.workers.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.CancelAsync();
      this.EnableSearchControls(true);
      this.progressBar1.Value = 0;
    }

    private void cmbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.tabHunterType.SelectedIndex == 0)
      {
        this.blast_options = (List<List<uint>>) null;
        this.cmbSkillFilter_SelectedIndexChanged(sender, this.bSkills, this.bSkillFilters, this.bIndexMaps);
      }
      else
      {
        if (this.tabHunterType.SelectedIndex != 1)
          return;
        this.glast_options = (List<List<uint>>) null;
        this.cmbSkillFilter_SelectedIndexChanged(sender, this.gSkills, this.gSkillFilters, this.gIndexMaps);
      }
    }

    private void cmbSkillFilter_SelectedIndexChanged(object sender, List<ComboBox> skills, List<ComboBox> skill_filters, List<Dictionary<uint, uint>> index_maps)
    {
      List<Ability> disallowed = new List<Ability>();
      int index1 = -1;
      Skill skill1 = (Skill) null;
      for (uint index2 = 0U; index2 < 6U; ++index2)
      {
        if (sender == skill_filters[(int) index2])
          index1 = (int) index2;
        if (skills[(int) index2].SelectedIndex < 1)
          continue;
        Skill skill2 = Skill.static_skills[(int) index_maps[(int) index2][(uint) skills[(int) index2].SelectedIndex]];
        if (sender == skill_filters[(int) index2])
          skill1 = skill2;
        else
          disallowed.Add(skill2.ability);
      }
      if (index1 == -1)
        return;
      skills[index1].BeginUpdate();
      this.lock_related = true;
      byte num = skill_filters != this.bSkillFilters ? (byte) 0 : (byte) 1;
      this.InitSkills(skills[index1], index_maps[index1], skill_filters[index1].SelectedIndex, disallowed, (bool) num);
      this.ResetSkill(skills[index1], index_maps[index1], skill1);
      this.lock_related = false;
      skills[index1].EndUpdate();
    }

    private void cmbSkill_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lock_skills)
        return;
      if (this.tabHunterType.SelectedIndex == 0)
      {
        this.blast_options = (List<List<uint>>) null;
        this.cmbSkill_SelectedIndexChanged(sender, this.bSkills, this.bSkillFilters, this.bIndexMaps);
      }
      else
      {
        if (this.tabHunterType.SelectedIndex != 1)
          return;
        this.glast_options = (List<List<uint>>) null;
        this.cmbSkill_SelectedIndexChanged(sender, this.gSkills, this.gSkillFilters, this.gIndexMaps);
      }
    }

    private void cmbSkill_SelectedIndexChanged(object sender, List<ComboBox> skills, List<ComboBox> skill_filters, List<Dictionary<uint, uint>> index_maps)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < 6; ++index2)
      {
        if (sender == skills[index2])
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        return;
      this.lock_skills = true;
      if (skills[index1].SelectedIndex == 0)
        skills[index1].SelectedIndex = -1;
      if (!this.lock_related)
        <Module>.FindRelatedSkills(skills, index_maps);
      for (int index2 = 0; index2 < 6; ++index2)
      {
        if ((index2 == index1 || skills[index1].SelectedIndex == -1) && skill_filters[index2].SelectedIndex != 2)
          continue;
        Skill skill = skills[index2].SelectedIndex != -1 ? Skill.static_skills[(int) index_maps[index2][(uint) skills[index2].SelectedIndex]] : (Skill) null;
        List<Ability> disallowed = new List<Ability>();
        for (int index3 = 0; index3 < 6; ++index3)
        {
          if (index3 == index2 || skills[index3].SelectedIndex == -1)
            continue;
          disallowed.Add(Skill.static_skills[(int) index_maps[index3][(uint) skills[index3].SelectedIndex]].ability);
        }
        skills[index2].BeginUpdate();
        byte num = skill_filters != this.bSkillFilters ? (byte) 0 : (byte) 1;
        this.InitSkills(skills[index2], index_maps[index2], skill_filters[index2].SelectedIndex, disallowed, (bool) num);
        this.ResetSkill(skills[index2], index_maps[index2], skill);
        skills[index2].EndUpdate();
      }
      this.lock_skills = false;
    }

    private void UpdateResultString(List<Solution> solutions)
    {
      if (StringTable.text == null)
        return;
      StringBuilder stringBuilder1 = new StringBuilder(solutions.Count * 1024);
      int num1 = 0;
      if (this.last_result != null)
        stringBuilder1.Append(this.last_result);
      string str1 = "-----------------";
      List<Solution>.Enumerator enumerator1 = solutions.GetEnumerator();
label_4:
      while (enumerator1.MoveNext())
      {
        Solution current1 = enumerator1.Current;
        ++num1;
        if (num1 <= this.MAX_LIMIT)
        {
          stringBuilder1.Append(this.endl);
          List<Armor>.Enumerator enumerator2 = current1.armors.GetEnumerator();
          while (enumerator2.MoveNext())
          {
            Armor current2 = enumerator2.Current;
            if (current2 != null)
            {
              stringBuilder1.Append(current2.name);
              if (current2.no_skills)
              {
                if ((int) current2.num_slots == 1)
                  stringBuilder1.Append(StringTable.text[78]);
                else
                  stringBuilder1.Append(StringTable.text[79].Replace("%1", Convert.ToString(current2.num_slots)));
              }
              else if (current2.torso_inc)
                stringBuilder1.Append(StringTable.text[80]);
              stringBuilder1.Append(this.endl);
            }
          }
          if (current1.charm != null)
          {
            stringBuilder1.AppendLine(str1);
            stringBuilder1.AppendLine(current1.charm.GetName());
          }
          if (current1.decorations.Count > 0)
          {
            stringBuilder1.AppendLine(str1);
            Dictionary<Decoration, uint> dictionary = new Dictionary<Decoration, uint>();
            List<Decoration>.Enumerator enumerator3 = current1.decorations.GetEnumerator();
            while (enumerator3.MoveNext())
            {
              Decoration current2 = enumerator3.Current;
              if (!dictionary.ContainsKey(current2))
              {
                dictionary.Add(current2, 1U);
              }
              else
              {
                uint num2 = dictionary[current2] + 1U;
                dictionary[current2] = num2;
              }
            }
            Dictionary<Decoration, uint>.Enumerator enumerator4 = dictionary.GetEnumerator();
            while (enumerator4.MoveNext())
            {
              KeyValuePair<Decoration, uint> current2 = enumerator4.Current;
              stringBuilder1.Append(Convert.ToString(current2.Value)).Append("x ");
              if (this.mnuPrintDecoNames.Checked)
              {
                KeyValuePair<Decoration, uint> current3 = enumerator4.Current;
                stringBuilder1.AppendLine(current3.Key.name);
              }
              else
              {
                KeyValuePair<Decoration, uint> current3 = enumerator4.Current;
                stringBuilder1.Append(current3.Key.abilities[0].ability.name).Append(" +");
                KeyValuePair<Decoration, uint> current4 = enumerator4.Current;
                stringBuilder1.Append(current4.Key.abilities[0].amount).Append(" ").AppendLine(StringTable.text[107]);
              }
            }
          }
          if (current1.total_slots_spare > 0U || this.cmbSort.SelectedIndex == 10)
          {
            if ((int) current1.total_slots_spare == 1)
              stringBuilder1.AppendLine(StringTable.text[76]);
            else if ((int) current1.total_slots_spare == 0)
              stringBuilder1.AppendLine(StringTable.text[77].Replace("%1", "0"));
            else if (current1.slots_spare == null || current1.total_slots_spare <= 3U && (int) current1.slots_spare[(int) current1.total_slots_spare] == 1)
            {
              stringBuilder1.AppendLine(StringTable.text[77].Replace("%1", Convert.ToString(current1.total_slots_spare)));
            }
            else
            {
              stringBuilder1.Append(StringTable.text[77].Replace("%1", Convert.ToString(current1.total_slots_spare)));
              stringBuilder1.Append(" (");
              bool flag = true;
              for (uint index1 = 1U; index1 <= 3U; ++index1)
              {
                for (uint index2 = 0U; index2 < current1.slots_spare[(int) index1]; ++index2)
                {
                  string str2 = !flag ? "+" : "";
                  stringBuilder1.Append(str2 + Convert.ToString(index1));
                  flag = false;
                }
              }
              stringBuilder1.AppendLine(")");
            }
          }
          if (this.cmbSort.SelectedIndex > 0 && this.cmbSort.SelectedIndex < 10)
          {
            if (this.cmbSort.SelectedIndex < 8)
            {
              stringBuilder1.AppendLine(str1);
              stringBuilder1.AppendLine(StringTable.text[106].Replace("%1", Convert.ToString(current1.defence)).Replace("%2", Convert.ToString(current1.max_defence)).Replace("%3", Convert.ToString(current1.fire_res)).Replace("%4", Convert.ToString(current1.water_res)).Replace("%5", Convert.ToString(current1.ice_res)).Replace("%6", Convert.ToString(current1.thunder_res)).Replace("%7", Convert.ToString(current1.dragon_res)));
            }
            else
            {
              if (this.cmbSort.SelectedIndex == 8)
                stringBuilder1.Append(current1.difficulty);
              else if (this.cmbSort.SelectedIndex == 9)
                stringBuilder1.Append(current1.rarity);
              stringBuilder1.Append(" ").AppendLine((string) this.cmbSort.SelectedItem);
            }
          }
          if (current1.extra_skills.Count > 0)
          {
            stringBuilder1.AppendLine(str1);
            List<Skill>.Enumerator enumerator3 = current1.extra_skills.GetEnumerator();
            while (enumerator3.MoveNext())
            {
              Skill current2 = enumerator3.Current;
              if (!<Module>.Utility>Contains<struct\u0020Skill>(this.query.skills, current2))
                stringBuilder1.AppendLine(current2.name);
            }
          }
          if (this.cmbSort.SelectedIndex == 12)
          {
            stringBuilder1.AppendLine(str1);
            Dictionary<Ability, int>.Enumerator enumerator3 = current1.abilities.GetEnumerator();
            while (true)
            {
              do
              {
                if (!enumerator3.MoveNext())
                  goto label_4;
              }
              while (enumerator3.Current.Value >= 10 || enumerator3.Current.Value <= 3);
              KeyValuePair<Ability, int> current2 = enumerator3.Current;
              KeyValuePair<Ability, int> current3 = enumerator3.Current;
              stringBuilder1.Append("+").Append(current3.Value).Append(" ").AppendLine(current2.Key.name);
            }
          }
        }
        else
          break;
      }
      if (this.final_solutions != solutions)
        this.final_solutions.AddRange((IEnumerable<Solution>) solutions);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append(StringTable.text[81] + " ").AppendLine(Convert.ToString(this.final_solutions.Count));
      if (solutions.Count > this.MAX_LIMIT)
        stringBuilder2.AppendLine(StringTable.text[82].Replace("%1", Convert.ToString(this.MAX_LIMIT)));
      this.last_result = stringBuilder1.ToString();
      stringBuilder2.Append(this.last_result);
      Form1.results_mutex.WaitOne();
      this.txtSolutions.SuspendLayout();
      this.txtSolutions.Enabled = false;
      this.txtSolutions.Text = stringBuilder2.ToString();
      this.txtSolutions.SelectionStart = 0;
      this.txtSolutions.SelectionLength = this.txtSolutions.Text.Length;
      this.txtSolutions.SelectionFont = new Font("Microsoft Sans Serif", this.txtSolutions.Font.Size);
      this.txtSolutions.SelectionLength = 0;
      this.txtSolutions.Enabled = true;
      this.txtSolutions.ResumeLayout();
      Form1.results_mutex.ReleaseMutex();
    }

    private long HashArmor(List<Armor> armors)
    {
      long num = 0L;
      for (int index = 0; index < armors.Count; ++index)
        num += (long) (armors[index].index << index * 3);
      return num;
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool SolutionExists(string charm, Solution sol)
    {
      bool flag;
      if (!this.existing_armor.ContainsKey(charm))
      {
        this.existing_armor.Add(charm, new Dictionary<long, bool>());
        this.existing_armor[charm].Add(this.HashArmor(sol.armors), true);
        flag = false;
      }
      else
      {
        Dictionary<long, bool> dictionary = this.existing_armor[charm];
        long key = this.HashArmor(sol.armors);
        if (dictionary.ContainsKey(key))
        {
          flag = true;
        }
        else
        {
          dictionary.Add(key, true);
          flag = false;
        }
      }
      return flag;
    }

    private void AddSolutions(List<Solution> solutions)
    {
      Form1.charm_map_mutex.WaitOne();
      List<Solution>.Enumerator enumerator = solutions.GetEnumerator();
      while (enumerator.MoveNext())
      {
        Solution current = enumerator.Current;
        if (current.charm != null)
        {
          this.AddSolution(current.charm.GetName(), current);
        }
        else
        {
          this.all_solutions.Add(current);
          this.no_charm_solutions.Add(current);
        }
      }
      Form1.charm_map_mutex.ReleaseMutex();
    }

    private void UpdateCharmComboBox(int new_index)
    {
      Charm charm = this.cmbCharms.SelectedIndex <= 1 ? (Charm) null : this.charm_box_charms[this.cmbCharms.SelectedIndex - 2];
      int selectedIndex = this.cmbCharms.SelectedIndex;
      this.charm_box_charms.Clear();
      List<string> list = new List<string>();
      Dictionary<string, Charm> dictionary = new Dictionary<string, Charm>();
      List<Solution>.Enumerator enumerator1 = this.all_solutions.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        Solution current = enumerator1.Current;
        if (current.charm != null)
        {
          string name = current.charm.GetName();
          if (!dictionary.ContainsKey(name))
          {
            list.Add(name);
            dictionary.Add(name, current.charm);
          }
        }
      }
      list.Sort();
      this.cmbCharms.BeginUpdate();
      this.cmbCharms.Items.Clear();
      this.cmbCharms.Items.Add((object) <Module>.StripAmpersands(StringTable.text[25]));
      this.cmbCharms.Items.Add((object) StringTable.text[45]);
      List<string>.Enumerator enumerator2 = list.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        string current = enumerator2.Current;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @current;
        // ISSUE: explicit reference operation
        this.charm_box_charms.Add(dictionary[^local]);
        // ISSUE: explicit reference operation
        this.cmbCharms.Items.Add((object) ^local);
      }
      if (new_index == -1)
      {
        if (selectedIndex == -1)
          this.cmbCharms.SelectedIndex = 1;
        else if (selectedIndex < 2)
        {
          this.cmbCharms.SelectedIndex = selectedIndex;
        }
        else
        {
          for (int index = 2; index < this.cmbCharms.Items.Count; ++index)
          {
            if (this.cmbCharms.Items[index].ToString() == charm.GetName())
            {
              this.cmbCharms.SelectedIndex = index;
              break;
            }
          }
        }
      }
      else
        this.cmbCharms.SelectedIndex = new_index;
      this.cmbCharms.EndUpdate();
    }

    private void UpdateCharmComboBox()
    {
      this.UpdateCharmComboBox(-1);
    }

    private void backgroundWorker1_RunWorkerCompleted(object __unnamed000, RunWorkerCompletedEventArgs e)
    {
      if (e.Error != null)
      {
        int num = (int) MessageBox.Show(e.Error.Message);
        this.progressBar1.Value = 0;
      }
      else if (e.Cancelled)
      {
        this.progressBar1.Value = 0;
      }
      else
      {
        if (e.Result == null)
          return;
        if (!this.search_cancelled)
        {
          Form1.worker_mutex.WaitOne();
          if (this.worker_start_index < (uint) this.workers.Count)
          {
            this.workers[(int) this.worker_start_index].RunWorkerAsync((object) this.worker_data[(int) this.worker_start_index]);
            ++this.worker_start_index;
          }
          Form1.worker_mutex.ReleaseMutex();
        }
        this.AddSolutions((List<Solution>) e.Result);
        Form1.progress_mutex.WaitOne();
        ++this.finished_workers;
        if (this.finished_workers >= (uint) this.workers.Count)
        {
          this.EnableSearchControls(true);
          this.progressBar1.Value = 100;
          this.SaveConfig();
          this.UpdateCharmComboBox(1);
        }
        else
          this.txtSolutions.Text = StringTable.text[81] + " " + Convert.ToString(this.all_solutions.Count);
        Form1.progress_mutex.ReleaseMutex();
      }
    }

    private void backgroundWorker1_ProgressChanged(object __unnamed000, ProgressChangedEventArgs e)
    {
      Form1.progress_mutex.WaitOne();
      ++this.num_updates;
      this.total_progress += (uint) e.ProgressPercentage;
      this.progressBar1.Value = (int) (this.total_progress / (uint) this.workers.Count);
      Form1.progress_mutex.ReleaseMutex();
      if (e.UserState == null)
        return;
      this.AddSolutions((List<Solution>) e.UserState);
      this.txtSolutions.Text = StringTable.text[81] + " " + Convert.ToString(this.all_solutions.Count);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker backgroundWorker = (BackgroundWorker) sender;
      ThreadSearchData threadSearchData = (ThreadSearchData) e.Argument;
      Query query = threadSearchData.query;
      List<Solution> list1 = new List<Solution>();
      List<Armor> list2 = query.rel_armor[0];
      List<Armor> list3 = query.rel_armor[1];
      List<Armor> list4 = query.rel_armor[2];
      List<Armor> list5 = query.rel_armor[3];
      List<Armor> list6 = query.rel_armor[4];
      if (list2.Count == 0)
        list2.Add((Armor) null);
      if (list3.Count == 0)
        list3.Add((Armor) null);
      if (list4.Count == 0)
        list4.Add((Armor) null);
      if (list5.Count == 0)
        list5.Add((Armor) null);
      if (list6.Count == 0)
        list6.Add((Armor) null);
      uint num1 = 0U;
      bool flag = false;
      for (int index1 = 0; index1 < list2.Count; ++index1)
      {
        for (int index2 = 0; index2 < list3.Count; ++index2)
        {
          int num2 = (index1 * list3.Count + index2) * 100 / (list2.Count * list3.Count);
          if (!flag)
          {
            backgroundWorker.ReportProgress(num2 - (int) num1);
            num1 = (uint) num2;
          }
          else
          {
            List<Solution> list7 = list1;
            list1 = new List<Solution>();
            backgroundWorker.ReportProgress(num2 - (int) num1, (object) list7);
            num1 = (uint) num2;
            flag = false;
          }
          for (int index3 = 0; index3 < list4.Count; ++index3)
          {
            for (int index4 = 0; index4 < list5.Count; ++index4)
            {
              for (int index5 = 0; index5 < list6.Count; ++index5)
              {
                if (backgroundWorker.CancellationPending)
                {
                  e.Result = (object) list1;
                  return;
                }
                else
                {
                  Solution solution = new Solution();
                  solution.armors.Add(list2[index1]);
                  solution.armors.Add(list3[index2]);
                  solution.armors.Add(list4[index3]);
                  solution.armors.Add(list5[index4]);
                  solution.armors.Add(list6[index5]);
                  Charm charm = threadSearchData.charm_template == null ? (Charm) null : new Charm(threadSearchData.charm_template);
                  solution.charm = charm;
                  if (!solution.MatchesQuery(query))
                    continue;
                  list1.Add(solution);
                  flag = true;
                }
              }
            }
          }
        }
      }
      backgroundWorker.ReportProgress(100 - (int) num1);
      e.Result = (object) list1;
    }

    private void FindDialogClosed(object sender, EventArgs e)
    {
      this.find_dialog = (frmFind) null;
    }

    private void FindDialogFoundText(object sender, EventArgs e)
    {
      frmFind frmFind = (frmFind) sender;
      if (frmFind.index == -1)
      {
        this.txtSolutions.SelectionStart = this.txtSolutions.Text.Length;
        this.txtSolutions.SelectionLength = 0;
      }
      else
      {
        this.txtSolutions.SelectionStart = frmFind.index;
        this.txtSolutions.SelectionLength = frmFind.length;
      }
      this.txtSolutions.ScrollToCaret();
      this.txtSolutions.Focus();
    }

    private void KeyDown(object sender, KeyEventArgs e)
    {
      if (sender != this.txtSolutions || !e.Control)
        return;
      if (e.KeyValue == 65)
      {
        this.txtSolutions.SelectAll();
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.KeyValue == 70 && this.find_dialog == null)
      {
        Form1 form1 = this;
        frmFind frmFind = new frmFind(form1.txtSolutions);
        form1.find_dialog = frmFind;
        this.find_dialog.DialogClosing += new EventHandler(this.FindDialogClosed);
        this.find_dialog.TextFound += new EventHandler(this.FindDialogFoundText);
        this.find_dialog.Show((IWin32Window) this);
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        int keyValue = e.KeyValue;
      }
    }

    private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((int) <Module>.\u003F\u003F_B\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00404\u0040\u0040Z\u0040$$Q51 & 1) == 0)
      {
        <Module>.\u003F\u003F_B\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00404\u0040\u0040Z\u0040$$Q51 |= 1U;
        // ISSUE: fault handler
        try
        {
          <Module>.\u003Flast_index\u0040\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00405\u0040\u0040Z\u0040$$Q4HA = -1;
        }
        __fault
        {
          <Module>.\u003F\u003F_B\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00404\u0040\u0040Z\u0040$$Q51 &= 4294967294U;
        }
      }
      if (this.cmbSort.SelectedIndex == <Module>.\u003Flast_index\u0040\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00405\u0040\u0040Z\u0040$$Q4HA)
        return;
      <Module>.\u003Flast_index\u0040\u003F1\u003F\u003FcmbSort_SelectedIndexChanged\u0040Form1\u0040MH3GASS\u0040\u0040A$AAMXP$AAVObject\u0040System\u0040\u0040P$AAVEventArgs\u00405\u0040\u0040Z\u0040$$Q4HA = this.cmbSort.SelectedIndex;
      if (this.data == null)
        return;
      this.SaveConfig();
      if (this.final_solutions.Count <= 0)
        return;
      this.SortResults();
      this.last_result = (string) null;
      Form1 form1 = this;
      List<Solution> solutions = form1.final_solutions;
      form1.UpdateResultString(solutions);
    }

    private void UpdateComboBoxLanguage(ComboBox cb)
    {
      cb.BeginUpdate();
      for (int index = 1; index < cb.Items.Count; ++index)
      {
        Skill skill = Skill.FindSkill((string) cb.Items[index]);
        cb.Items[index] = (object) skill.name;
      }
      cb.EndUpdate();
    }

    private void LanguageSelect_Click(object sender, EventArgs e)
    {
      int num1 = this.language;
      for (int index = 0; index < this.mnuLanguage.DropDownItems.Count; ++index)
      {
        ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem) this.mnuLanguage.DropDownItems[index];
        if (this.mnuLanguage.DropDownItems[index] != sender)
          goto label_6;
        if (index == this.language)
          return;
        this.language = index;
        toolStripMenuItem.Checked = true;
        continue;
label_6:
        toolStripMenuItem.Checked = false;
      }
      if (num1 == this.language)
        return;
      this.updating_language = true;
      StringTable.LoadLanguage(((ToolStripItem) sender).ToString());
      List<ComboBox>.Enumerator enumerator1 = this.bSkillFilters.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        ComboBox current = enumerator1.Current;
        current.BeginUpdate();
        int index1 = 0;
        int num2 = 0;
        for (; index1 < SkillTag.tags.Count; ++index1)
        {
          if (SkillTag.tags[index1].disable_b)
            continue;
          int index2 = num2;
          current.Items[index2] = (object) SkillTag.tags[index1].name;
          ++num2;
        }
        current.EndUpdate();
      }
      List<ComboBox>.Enumerator enumerator2 = this.gSkillFilters.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        ComboBox current = enumerator2.Current;
        current.BeginUpdate();
        int index1 = 0;
        int num2 = 0;
        for (; index1 < SkillTag.tags.Count; ++index1)
        {
          if (SkillTag.tags[index1].disable_g)
            continue;
          int index2 = num2;
          current.Items[index2] = (object) SkillTag.tags[index1].name;
          ++num2;
        }
        current.EndUpdate();
      }
      this.mnuFile.Text = StringTable.text[1];
      this.mnuOptions.Text = StringTable.text[2];
      this.mnuLanguage.Text = StringTable.text[3];
      this.mnuHelp.Text = StringTable.text[4];
      this.mnuLoadData.Text = StringTable.text[5];
      this.mnuSaveData.Text = StringTable.text[6];
      this.mnuExit.Text = StringTable.text[8];
      this.mnuClearSettings.Text = StringTable.text[9];
      this.mnuAllowBadSkills.Text = StringTable.text[10];
      this.mnuAllowEventArmor.Text = StringTable.text[12];
      this.mnuAllowLowerTierArmor.Text = StringTable.text[14];
      this.mnuAllowJapaneseOnlyDLC.Text = StringTable.text[13];
      this.mnuAllowPiercings.Text = StringTable.text[11];
      this.mnuCheckForUpdates.Text = StringTable.text[16];
      this.mnuAbout.Text = StringTable.text[17];
      this.mnuMaxResults.Text = StringTable.text[104];
      this.mnuPrintDecoNames.Text = StringTable.text[105];
      this.mnuSortSkillsAlphabetically.Text = StringTable.text[15];
      this.lblHR.Text = StringTable.text[38];
      this.lblElder.Text = StringTable.text[39];
      this.lblSlots.Text = StringTable.text[40];
      this.lblCharmTable.Text = StringTable.text[93];
      this.lblWhatIsCharmTable.Location = new Point(this.lblCharmTable.Location.X + this.lblCharmTable.Size.Width - 5, this.lblWhatIsCharmTable.Location.Y);
      this.grpSort.Text = StringTable.text[44];
      this.grpCharmFilter.Text = StringTable.text[43];
      this.grpCharms.Text = StringTable.text[58];
      this.grpResults.Text = StringTable.text[92];
      this.rdoMale.Text = StringTable.text[41];
      this.rdoFemale.Text = StringTable.text[42];
      this.tabBlademaster.Text = StringTable.text[66];
      this.tabGunner.Text = StringTable.text[67];
      this.grpBSkills.Text = StringTable.text[64];
      this.grpGSkills.Text = StringTable.text[64];
      this.grpBSkillFilters.Text = StringTable.text[65];
      this.grpGSkillFilters.Text = StringTable.text[65];
      this.btnCharms.Text = StringTable.text[18];
      this.btnImport.Text = StringTable.text[19];
      this.btnSearch.Text = StringTable.text[20];
      this.btnAdvancedSearch.Text = StringTable.text[21];
      this.btnCancel.Text = StringTable.text[22];
      this.cmbCharmSelect.Items[0] = (object) StringTable.text[59];
      this.cmbCharmSelect.Items[1] = (object) StringTable.text[60];
      this.cmbCharmSelect.Items[2] = (object) StringTable.text[61];
      this.cmbCharmSelect.Items[3] = (object) StringTable.text[62];
      this.cmbCharmSelect.Items[4] = (object) StringTable.text[63];
      this.cmbSort.Items[0] = (object) <Module>.StripAmpersands(StringTable.text[26]);
      this.cmbSort.Items[1] = (object) StringTable.text[46];
      this.cmbSort.Items[2] = (object) StringTable.text[47];
      this.cmbSort.Items[3] = (object) StringTable.text[48];
      this.cmbSort.Items[4] = (object) StringTable.text[49];
      this.cmbSort.Items[5] = (object) StringTable.text[50];
      this.cmbSort.Items[6] = (object) StringTable.text[51];
      this.cmbSort.Items[7] = (object) StringTable.text[52];
      this.cmbSort.Items[8] = (object) StringTable.text[53];
      this.cmbSort.Items[9] = (object) StringTable.text[54];
      this.cmbSort.Items[10] = (object) StringTable.text[55];
      this.cmbSort.Items[11] = (object) StringTable.text[56];
      this.cmbSort.Items[12] = (object) StringTable.text[57];
      this.cmbCharmTable.Items[0] = (object) StringTable.text[94];
      this.cmbCharmTable.Items[18] = (object) StringTable.text[95];
      this.charm_solution_map.Clear();
      List<Solution>.Enumerator enumerator3 = this.all_solutions.GetEnumerator();
      while (enumerator3.MoveNext())
      {
        Solution current = enumerator3.Current;
        if (current.charm != null)
        {
          string name = current.charm.GetName();
          if (!this.charm_solution_map.ContainsKey(name))
            this.charm_solution_map.Add(name, new List<Solution>());
          this.charm_solution_map[name].Add(current);
        }
      }
      for (int index = 0; index < 6; ++index)
      {
        this.bSkills[index].BeginUpdate();
        Form1 form1_1 = this;
        ComboBox comboBox1 = form1_1.bSkillFilters[index];
        List<ComboBox> skills1 = this.bSkills;
        List<ComboBox> skill_filters1 = this.bSkillFilters;
        List<Dictionary<uint, uint>> index_maps1 = this.bIndexMaps;
        form1_1.cmbSkillFilter_SelectedIndexChanged((object) comboBox1, skills1, skill_filters1, index_maps1);
        this.bSkills[index].EndUpdate();
        this.gSkills[index].BeginUpdate();
        Form1 form1_2 = this;
        ComboBox comboBox2 = form1_2.gSkillFilters[index];
        List<ComboBox> skills2 = this.gSkills;
        List<ComboBox> skill_filters2 = this.gSkillFilters;
        List<Dictionary<uint, uint>> index_maps2 = this.gIndexMaps;
        form1_2.cmbSkillFilter_SelectedIndexChanged((object) comboBox2, skills2, skill_filters2, index_maps2);
        this.gSkills[index].EndUpdate();
      }
      this.UpdateCharmComboBox();
      if (this.construction_complete)
        CharmDatabase.SaveCustom();
      this.updating_language = false;
      this.cmbCharms_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmAbout frmAbout1 = new frmAbout();
      frmAbout frmAbout2;
      // ISSUE: fault handler
      try
      {
        frmAbout2 = frmAbout1;
        int num = (int) frmAbout2.ShowDialog((IWin32Window) this);
      }
      __fault
      {
        frmAbout2.Dispose();
      }
      frmAbout2.Dispose();
    }

    private void HRChanged(object sender, EventArgs e)
    {
      this.DeleteOptions(sender, e);
    }

    private void DeleteOptions(object sender, EventArgs e)
    {
      this.glast_options = (List<List<uint>>) null;
      this.blast_options = (List<List<uint>>) null;
    }

    private void SortResults()
    {
      if (this.cmbSort.SelectedIndex < 1 || this.sort_off)
        return;
      if (this.cmbSort.SelectedIndex == 1)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByDragonRes));
      else if (this.cmbSort.SelectedIndex == 2)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByFireRes));
      else if (this.cmbSort.SelectedIndex == 3)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByIceRes));
      else if (this.cmbSort.SelectedIndex == 4)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByThunderRes));
      else if (this.cmbSort.SelectedIndex == 5)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByWaterRes));
      else if (this.cmbSort.SelectedIndex == 6)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByDefence));
      else if (this.cmbSort.SelectedIndex == 7)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByMaxDefence));
      else if (this.cmbSort.SelectedIndex == 8)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByDifficulty));
      else if (this.cmbSort.SelectedIndex == 9)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByRarity));
      else if (this.cmbSort.SelectedIndex == 10)
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionBySlotsSpare));
      else if (this.cmbSort.SelectedIndex == 11)
      {
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionByFamily));
      }
      else
      {
        if (this.cmbSort.SelectedIndex != 12)
          return;
        this.final_solutions.Sort(new Comparison<Solution>(<Module>.MH3GASS>CompareSolutionsByExtraSkills));
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    private bool EndsWithSlots(ref string s)
    {
      return s.EndsWith(" ---") || s.EndsWith("O--") || (s.EndsWith("OO-") || s.EndsWith("OOO"));
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      if (this.txtSolutions.Text == "")
        return;
      this.cmsSolutions.Items.Clear();
      e.Cancel = true;
      string s = this.txtSolutions.Lines[this.txtSolutions.GetLineFromCharIndex(this.txtSolutions.GetCharIndexFromPosition(this.txtSolutions.PointToClient(Control.MousePosition)))];
      if (s == "")
        return;
      if (s.Length >= 4 && (s.Substring(1, 2) == "x " || s.Substring(2, 2) == "x "))
      {
        string str = s;
        int num1 = 32;
        int startIndex = str.IndexOf((char) num1) + 1;
        string name = str.Substring(startIndex);
        Decoration decoration1 = Decoration.FindDecoration(name);
        if (decoration1 != null)
        {
          <Module>.Utility>UpdateContextMenu(this.cmsSolutions, decoration1);
          e.Cancel = false;
        }
        else
        {
          if (!name.EndsWith(" " + StringTable.text[107]))
            return;
          int num2 = name.IndexOf('+');
          uint points = Convert.ToUInt32(name.Substring(num2 + 1, 1));
          Decoration decoration2 = Decoration.FindDecoration(name.Substring(0, num2 - 1), points);
          if (decoration2 == null)
            return;
          <Module>.Utility>UpdateContextMenu(this.cmsSolutions, decoration2);
          e.Cancel = false;
        }
      }
      else
      {
        Armor armor1 = Armor.FindArmor(s);
        if (armor1 != null)
        {
          <Module>.Utility>UpdateContextMenu(this.cmsSolutions, armor1);
          e.Cancel = false;
        }
        else
        {
          int num = s.LastIndexOf('(');
          if (num != -1)
          {
            s = s.Substring(0, num - 1);
            Armor armor2 = Armor.FindArmor(s);
            if (armor2 != null)
            {
              <Module>.Utility>UpdateContextMenu(this.cmsSolutions, armor2);
              e.Cancel = false;
            }
          }
          if (!this.EndsWithSlots(ref s))
            return;
          <Module>.Utility>UpdateContextMenu(this.cmsSolutions, s, (uint) this.cmbCharmTable.SelectedIndex);
          e.Cancel = false;
        }
      }
    }

    private void cmbCharms_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.updating_language || this.cmbCharms.SelectedIndex < 0)
        return;
      this.last_result = "";
      this.final_solutions.Clear();
      string key = (string) this.cmbCharms.SelectedItem;
      if (this.cmbCharms.SelectedIndex == 0)
      {
        this.final_solutions.AddRange((IEnumerable<Solution>) this.no_charm_solutions);
        this.SortResults();
        Form1 form1 = this;
        List<Solution> solutions = form1.final_solutions;
        form1.UpdateResultString(solutions);
      }
      else if (this.cmbCharms.SelectedIndex == 1)
      {
        this.final_solutions.AddRange((IEnumerable<Solution>) this.all_solutions);
        this.SortResults();
        Form1 form1 = this;
        List<Solution> solutions = form1.final_solutions;
        form1.UpdateResultString(solutions);
      }
      else if (this.charm_solution_map.ContainsKey(key))
      {
        this.final_solutions.AddRange((IEnumerable<Solution>) this.charm_solution_map[key]);
        this.SortResults();
        Form1 form1 = this;
        List<Solution> solutions = form1.final_solutions;
        form1.UpdateResultString(solutions);
      }
      else
        this.UpdateResultString(new List<Solution>());
    }

    private void btnCharms_Click(object sender, EventArgs e)
    {
      ManageCharms manageCharms1 = new ManageCharms(this.language, this.data, this.mnuSortSkillsAlphabetically.Checked);
      ManageCharms manageCharms2;
      // ISSUE: fault handler
      try
      {
        manageCharms2 = manageCharms1;
        int num = (int) manageCharms2.ShowDialog((IWin32Window) this);
        if (manageCharms2.detected_charm_table != -1)
        {
          this.cmbCharmTable.SelectedIndex = manageCharms2.detected_charm_table;
          this.OptionsChanged((object) null, (EventArgs) null);
        }
      }
      __fault
      {
        manageCharms2.Dispose();
      }
      manageCharms2.Dispose();
    }

    private void UpdateMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://forums.minegarde.com/topic/5304-athenas-armor-set-search-for-mh3g");
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.cmbSort.SelectedIndex = 0;
      List<ComboBox>.Enumerator enumerator1 = this.bSkills.GetEnumerator();
      while (enumerator1.MoveNext())
        enumerator1.Current.SelectedIndex = -1;
      List<ComboBox>.Enumerator enumerator2 = this.gSkills.GetEnumerator();
      while (enumerator2.MoveNext())
        enumerator2.Current.SelectedIndex = -1;
      List<ComboBox>.Enumerator enumerator3 = this.bSkillFilters.GetEnumerator();
      while (enumerator3.MoveNext())
        enumerator3.Current.SelectedIndex = 0;
      List<ComboBox>.Enumerator enumerator4 = this.gSkillFilters.GetEnumerator();
      while (enumerator4.MoveNext())
        enumerator4.Current.SelectedIndex = 0;
    }

    private void mnuLoad_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      OpenFileDialog openFileDialog2;
      // ISSUE: fault handler
      try
      {
        openFileDialog2 = openFileDialog1;
        openFileDialog2.InitialDirectory = Environment.CurrentDirectory;
        openFileDialog2.DefaultExt = ".ass";
        openFileDialog2.AddExtension = true;
        openFileDialog2.Filter = StringTable.text[103] + " " + "(*.ass)|*.ass";
        openFileDialog2.FilterIndex = 0;
        if (openFileDialog2.ShowDialog() == DialogResult.OK)
          this.LoadConfig(openFileDialog2.FileName);
      }
      __fault
      {
        openFileDialog2.Dispose();
      }
      openFileDialog2.Dispose();
    }

    private void mnuSave_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      SaveFileDialog saveFileDialog2;
      // ISSUE: fault handler
      try
      {
        saveFileDialog2 = saveFileDialog1;
        saveFileDialog2.InitialDirectory = Environment.CurrentDirectory;
        saveFileDialog2.DefaultExt = ".ass";
        saveFileDialog2.AddExtension = true;
        saveFileDialog2.Filter = StringTable.text[103] + " " + "(*.ass)|*.ass";
        saveFileDialog2.FilterIndex = 0;
        saveFileDialog2.FileName = "results.ass";
        if (saveFileDialog2.ShowDialog() == DialogResult.OK)
          this.SaveConfig(saveFileDialog2.FileName);
      }
      __fault
      {
        saveFileDialog2.Dispose();
      }
      saveFileDialog2.Dispose();
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      OpenFileDialog openFileDialog2;
      // ISSUE: fault handler
      try
      {
        openFileDialog2 = openFileDialog1;
        openFileDialog2.InitialDirectory = Environment.CurrentDirectory;
        openFileDialog2.DefaultExt = ".bin";
        openFileDialog2.AddExtension = true;
        openFileDialog2.Filter = StringTable.text[83] + " " + "(*.csv)|CHARM.csv";
        openFileDialog2.FilterIndex = 0;
        if (openFileDialog2.ShowDialog() == DialogResult.OK)
        {
          ImportCharms importCharms1 = new ImportCharms();
          ImportCharms importCharms2;
          // ISSUE: fault handler
          try
          {
            importCharms2 = importCharms1;
            importCharms2.language = (uint) this.language;
            importCharms2.LoadCharms(openFileDialog2.FileName);
            importCharms2.ShowDialog();
          }
          __fault
          {
            importCharms2.Dispose();
          }
          importCharms2.Dispose();
        }
      }
      __fault
      {
        openFileDialog2.Dispose();
      }
      openFileDialog2.Dispose();
    }

    private void cmbCharmTable_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.lblWhatIsCharmTable.Visible = this.cmbCharmTable.SelectedIndex == 0 || this.cmbCharmTable.SelectedIndex == 18;
      this.OptionsChanged(sender, e);
    }

    private void lblWhatIsCharmTable_Click(object sender, EventArgs e)
    {
      int num = CharmDatabase.DetectCharmTable();
      if (num == -1)
        return;
      this.cmbCharmTable.SelectedIndex = num;
      this.OptionsChanged(sender, e);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EForm1();
        }
        finally
        {
          base.Dispose(true);
        }
      }
      else
        base.Dispose(false);
    }
  }
}
