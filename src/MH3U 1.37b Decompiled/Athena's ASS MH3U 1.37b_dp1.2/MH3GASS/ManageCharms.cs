// Decompiled with JetBrains decompiler
// Type: MH3GASS.ManageCharms
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MH3GASS
{
  public class ManageCharms : Form
  {
    private int language = language;
    private bool update_skills = true;
    private bool updating_charms = false;
    private bool sort_alphabetically = sort_alphabetically;
    private LoadedData data = _data;
    private readonly List<Charm> best_charms = new List<Charm>();
    public int detected_charm_table = -1;
    private ListBox lstCharms;
    private Label lblSlots;
    private NumericUpDown nudSlots;
    private Button btnAddNew;
    private Button btnSave;
    private ComboBox cmbSkills1;
    private ComboBox cmbSkills2;
    private ComboBox cmbAmount1;
    private ComboBox cmbAmount2;
    private Button btnDelete;
    private Button btnMoveUp;
    private Button btnMoveDown;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private ComboBox cmbSkillFilters2;
    private ComboBox cmbSkillFilters1;
    private Button btnDeleteAll;
    private Button btnTrim;
    private Button btnDetectTable;
    private Button btnSort;
    private Container components;

    public ManageCharms(int language, LoadedData _data, [MarshalAs(UnmanagedType.U1)] bool sort_alphabetically)
    {
      // ISSUE: fault handler
      try
      {
        this.InitializeComponent();
        this.lstCharms.DrawItem += new DrawItemEventHandler(this.lstCharms_DrawItem);
        List<Ability>.Enumerator enumerator = Ability.static_abilities.GetEnumerator();
        while (enumerator.MoveNext())
          enumerator.Current.relevant = true;
        this.RefreshList(-1);
        this.UpdateBestCharms();
        ManageCharms manageCharms1 = this;
        ComboBox cb1 = manageCharms1.cmbSkillFilters1;
        manageCharms1.InitFilter(cb1);
        ManageCharms manageCharms2 = this;
        ComboBox cb2 = manageCharms2.cmbSkillFilters2;
        manageCharms2.InitFilter(cb2);
        this.cmbAmount1.SelectedIndex = 0;
        this.cmbAmount2.SelectedIndex = 0;
        this.cmbAmount1.LostFocus += new EventHandler(this.cmbAmount_LostFocus);
        this.cmbAmount2.LostFocus += new EventHandler(this.cmbAmount_LostFocus);
        this.Text = \u003CModule\u003E.StripAmpersands(StringTable.text[18]);
        this.btnAddNew.Text = StringTable.text[30];
        this.btnSave.Text = StringTable.text[7];
        this.btnDelete.Text = StringTable.text[31];
        this.btnDeleteAll.Text = StringTable.text[32];
        this.btnMoveUp.Text = StringTable.text[33];
        this.btnMoveDown.Text = StringTable.text[34];
        this.btnTrim.Text = StringTable.text[35];
        this.btnDetectTable.Text = StringTable.text[96];
        this.btnSort.Text = StringTable.text[73];
        this.lblSlots.Text = StringTable.text[74];
        this.btnAddNew.Width = TextRenderer.MeasureText(this.btnAddNew.Text, this.btnAddNew.Font).Width + 10;
        this.btnSave.Width = TextRenderer.MeasureText(this.btnSave.Text, this.btnSave.Font).Width + 10;
        this.lblSlots.Width = TextRenderer.MeasureText(this.lblSlots.Text, this.lblSlots.Font).Width;
        ManageCharms manageCharms3 = this;
        NumericUpDown numericUpDown1 = manageCharms3.nudSlots;
        Label label = this.lblSlots;
        int extra1 = 6;
        manageCharms3.PlaceToTheRightOf((Control) numericUpDown1, (Control) label, extra1);
        ManageCharms manageCharms4 = this;
        Button button1 = manageCharms4.btnAddNew;
        NumericUpDown numericUpDown2 = this.nudSlots;
        int extra2 = 6;
        manageCharms4.PlaceToTheRightOf((Control) button1, (Control) numericUpDown2, extra2);
        ManageCharms manageCharms5 = this;
        Button button2 = manageCharms5.btnSave;
        Button button3 = this.btnAddNew;
        int extra3 = 6;
        manageCharms5.PlaceToTheRightOf((Control) button2, (Control) button3, extra3);
        if (this.lstCharms.SelectedIndex != -1)
          return;
        this.btnSave.Enabled = false;
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    public void PlaceToTheRightOf(Control to_move, Control left, int extra)
    {
      Point location = to_move.Location;
      Point point = new Point(left.Location.X + left.Width + extra, location.Y);
      to_move.Location = point;
    }

    public void InitFilter(ComboBox cb)
    {
      cb.BeginUpdate();
      cb.Items.Clear();
      List<SkillTag>.Enumerator enumerator = SkillTag.tags.GetEnumerator();
      while (enumerator.MoveNext())
      {
        SkillTag current = enumerator.Current;
        cb.Items.Add((object) current.name);
      }
      cb.SelectedIndex = 0;
      cb.EndUpdate();
    }

    private void \u007EManageCharms()
    {
      if (this.components == null)
        return;
      List<Ability>.Enumerator enumerator = Ability.static_abilities.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.relevant = false;
      IDisposable disposable = (IDisposable) this.components;
      int num;
      if (disposable != null)
      {
        disposable.Dispose();
        num = 0;
      }
      else
        num = 0;
    }

    private void InitializeComponent()
    {
      this.lstCharms = new ListBox();
      this.lblSlots = new Label();
      this.nudSlots = new NumericUpDown();
      this.btnAddNew = new Button();
      this.btnSave = new Button();
      this.cmbSkills1 = new ComboBox();
      this.cmbSkills2 = new ComboBox();
      this.cmbAmount1 = new ComboBox();
      this.cmbAmount2 = new ComboBox();
      this.btnDelete = new Button();
      this.btnMoveUp = new Button();
      this.btnMoveDown = new Button();
      this.groupBox1 = new GroupBox();
      this.cmbSkillFilters2 = new ComboBox();
      this.cmbSkillFilters1 = new ComboBox();
      this.groupBox2 = new GroupBox();
      this.btnSort = new Button();
      this.btnDetectTable = new Button();
      this.btnTrim = new Button();
      this.btnDeleteAll = new Button();
      this.nudSlots.BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      this.lstCharms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.lstCharms.DrawMode = DrawMode.OwnerDrawFixed;
      this.lstCharms.FormattingEnabled = true;
      this.lstCharms.IntegralHeight = false;
      this.lstCharms.Location = new Point(6, 19);
      this.lstCharms.Name = "lstCharms";
      this.lstCharms.Size = new Size(193, 186);
      this.lstCharms.TabIndex = 1;
      this.lstCharms.SelectedIndexChanged += new EventHandler(this.lstCharms_SelectedIndexChanged);
      this.lblSlots.AutoSize = true;
      this.lblSlots.Location = new Point(5, 87);
      this.lblSlots.Name = "lblSlots";
      this.lblSlots.Size = new Size(30, 13);
      this.lblSlots.TabIndex = 2;
      this.lblSlots.Text = "Slots";
      this.nudSlots.Location = new Point(41, 84);
      this.nudSlots.Maximum = new Decimal(new int[4]
      {
        3,
        0,
        0,
        0
      });
      this.nudSlots.Name = "nudSlots";
      this.nudSlots.Size = new Size(31, 20);
      this.nudSlots.TabIndex = 3;
      this.btnAddNew.Location = new Point(78, 81);
      this.btnAddNew.Name = "btnAddNew";
      this.btnAddNew.Size = new Size(66, 25);
      this.btnAddNew.TabIndex = 4;
      this.btnAddNew.Text = "&Add New";
      this.btnAddNew.UseVisualStyleBackColor = true;
      this.btnAddNew.Click += new EventHandler(this.btnAddNew_Click);
      this.btnSave.Location = new Point(150, 81);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(66, 25);
      this.btnSave.TabIndex = 5;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.cmbSkills1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkills1.FormattingEnabled = true;
      this.cmbSkills1.Location = new Point(52, 19);
      this.cmbSkills1.Name = "cmbSkills1";
      this.cmbSkills1.Size = new Size(132, 21);
      this.cmbSkills1.TabIndex = 7;
      this.cmbSkills1.SelectedIndexChanged += new EventHandler(this.cmbSkills1_SelectedIndexChanged);
      this.cmbSkills2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkills2.FormattingEnabled = true;
      this.cmbSkills2.Location = new Point(52, 46);
      this.cmbSkills2.Name = "cmbSkills2";
      this.cmbSkills2.Size = new Size(132, 21);
      this.cmbSkills2.TabIndex = 8;
      this.cmbSkills2.SelectedIndexChanged += new EventHandler(this.cmbSkills2_SelectedIndexChanged);
      this.cmbAmount1.FormattingEnabled = true;
      this.cmbAmount1.Items.AddRange(new object[11]
      {
        (object) "0",
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7",
        (object) "8",
        (object) "9",
        (object) "10"
      });
      this.cmbAmount1.Location = new Point(6, 19);
      this.cmbAmount1.MaxLength = 3;
      this.cmbAmount1.Name = "cmbAmount1";
      this.cmbAmount1.Size = new Size(40, 21);
      this.cmbAmount1.TabIndex = 9;
      this.cmbAmount2.FormattingEnabled = true;
      this.cmbAmount2.Items.AddRange(new object[24]
      {
        (object) "0",
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7",
        (object) "8",
        (object) "9",
        (object) "10",
        (object) "11",
        (object) "12",
        (object) "13",
        (object) "-1",
        (object) "-2",
        (object) "-3",
        (object) "-4",
        (object) "-5",
        (object) "-6",
        (object) "-7",
        (object) "-8",
        (object) "-9",
        (object) "-10"
      });
      this.cmbAmount2.Location = new Point(6, 46);
      this.cmbAmount2.MaxLength = 3;
      this.cmbAmount2.Name = "cmbAmount2";
      this.cmbAmount2.Size = new Size(40, 21);
      this.cmbAmount2.TabIndex = 10;
      this.btnDelete.Location = new Point(205, 19);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(95, 25);
      this.btnDelete.TabIndex = 11;
      this.btnDelete.Text = "De&lete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.btnMoveUp.Location = new Point(205, 100);
      this.btnMoveUp.Name = "btnMoveUp";
      this.btnMoveUp.Size = new Size(95, 25);
      this.btnMoveUp.TabIndex = 12;
      this.btnMoveUp.Text = "Move &Up";
      this.btnMoveUp.UseVisualStyleBackColor = true;
      this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
      this.btnMoveDown.Location = new Point(205, 125);
      this.btnMoveDown.Name = "btnMoveDown";
      this.btnMoveDown.Size = new Size(95, 25);
      this.btnMoveDown.TabIndex = 13;
      this.btnMoveDown.Text = "Move &Down";
      this.btnMoveDown.UseVisualStyleBackColor = true;
      this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
      this.groupBox1.Controls.Add((Control) this.cmbSkillFilters2);
      this.groupBox1.Controls.Add((Control) this.cmbSkillFilters1);
      this.groupBox1.Controls.Add((Control) this.cmbAmount2);
      this.groupBox1.Controls.Add((Control) this.lblSlots);
      this.groupBox1.Controls.Add((Control) this.nudSlots);
      this.groupBox1.Controls.Add((Control) this.btnAddNew);
      this.groupBox1.Controls.Add((Control) this.btnSave);
      this.groupBox1.Controls.Add((Control) this.cmbAmount1);
      this.groupBox1.Controls.Add((Control) this.cmbSkills1);
      this.groupBox1.Controls.Add((Control) this.cmbSkills2);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(312, 117);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      this.cmbSkillFilters2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkillFilters2.FormattingEnabled = true;
      this.cmbSkillFilters2.Location = new Point(190, 44);
      this.cmbSkillFilters2.Name = "cmbSkillFilters2";
      this.cmbSkillFilters2.Size = new Size(110, 21);
      this.cmbSkillFilters2.TabIndex = 12;
      this.cmbSkillFilters2.SelectedIndexChanged += new EventHandler(this.cmbSkillFilters2_SelectedIndexChanged);
      this.cmbSkillFilters1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkillFilters1.FormattingEnabled = true;
      this.cmbSkillFilters1.Location = new Point(190, 19);
      this.cmbSkillFilters1.Name = "cmbSkillFilters1";
      this.cmbSkillFilters1.Size = new Size(110, 21);
      this.cmbSkillFilters1.TabIndex = 11;
      this.cmbSkillFilters1.SelectedIndexChanged += new EventHandler(this.cmbSkillFilters1_SelectedIndexChanged);
      this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.groupBox2.Controls.Add((Control) this.btnSort);
      this.groupBox2.Controls.Add((Control) this.btnDetectTable);
      this.groupBox2.Controls.Add((Control) this.btnTrim);
      this.groupBox2.Controls.Add((Control) this.btnDeleteAll);
      this.groupBox2.Controls.Add((Control) this.lstCharms);
      this.groupBox2.Controls.Add((Control) this.btnDelete);
      this.groupBox2.Controls.Add((Control) this.btnMoveDown);
      this.groupBox2.Controls.Add((Control) this.btnMoveUp);
      this.groupBox2.Location = new Point(12, 135);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(312, 213);
      this.groupBox2.TabIndex = 15;
      this.groupBox2.TabStop = false;
      this.btnSort.Location = new Point(205, 150);
      this.btnSort.Name = "btnSort";
      this.btnSort.Size = new Size(95, 25);
      this.btnSort.TabIndex = 17;
      this.btnSort.Text = "&Sort";
      this.btnSort.UseVisualStyleBackColor = true;
      this.btnSort.Click += new EventHandler(this.btnSort_Click);
      this.btnDetectTable.Location = new Point(205, 181);
      this.btnDetectTable.Name = "btnDetectTable";
      this.btnDetectTable.Size = new Size(95, 25);
      this.btnDetectTable.TabIndex = 16;
      this.btnDetectTable.Text = "&Detect Table";
      this.btnDetectTable.UseVisualStyleBackColor = true;
      this.btnDetectTable.Click += new EventHandler(this.btnDetectTable_Click);
      this.btnTrim.Location = new Point(205, 69);
      this.btnTrim.Name = "btnTrim";
      this.btnTrim.Size = new Size(95, 25);
      this.btnTrim.TabIndex = 15;
      this.btnTrim.Text = "&Trim";
      this.btnTrim.UseVisualStyleBackColor = true;
      this.btnTrim.Click += new EventHandler(this.btnTrim_Click);
      this.btnDeleteAll.Location = new Point(205, 44);
      this.btnDeleteAll.Name = "btnDeleteAll";
      this.btnDeleteAll.Size = new Size(95, 25);
      this.btnDeleteAll.TabIndex = 14;
      this.btnDeleteAll.Text = "Delete &All";
      this.btnDeleteAll.UseVisualStyleBackColor = true;
      this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(336, 360);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = "ManageCharms";
      this.Text = "My Charms";
      this.nudSlots.EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void lstCharms_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      e.DrawFocusRectangle();
      if (e.Index < 0 || e.Index >= CharmDatabase.mycharms.Count || CharmDatabase.mycharms[e.Index] == null)
        return;
      SolidBrush solidBrush = new SolidBrush(!CharmDatabase.mycharms[e.Index].optimal ? Color.Red : Color.Black);
      RectangleF layoutRectangle = (RectangleF) e.Bounds;
      e.Graphics.DrawString(this.lstCharms.Items[e.Index].ToString(), e.Font, (Brush) solidBrush, layoutRectangle);
    }

    private void RefreshList(int new_index)
    {
      this.lstCharms.BeginUpdate();
      this.lstCharms.Items.Clear();
      List<Charm>.Enumerator enumerator = CharmDatabase.mycharms.GetEnumerator();
      while (enumerator.MoveNext())
        this.lstCharms.Items.Add((object) enumerator.Current.GetName());
      this.lstCharms.SelectedIndex = new_index;
      this.lstCharms.EndUpdate();
    }

    private Charm CreateCharm()
    {
      uint num_slots = (uint) (int) this.nudSlots.Value;
      int am1 = 0;
      int am2 = 0;
      Ability ab1 = (Ability) null;
      Ability ab2 = (Ability) null;
      if (this.cmbAmount1.Text != "0" && this.cmbSkills1.SelectedIndex != -1)
      {
        am1 = Convert.ToInt32(this.cmbAmount1.Text);
        ab1 = Ability.FindAbility((string) this.cmbSkills1.SelectedItem);
      }
      if (this.cmbAmount2.Text != "0" && this.cmbSkills2.SelectedIndex != -1)
      {
        am2 = Convert.ToInt32(this.cmbAmount2.Text);
        ab2 = Ability.FindAbility((string) this.cmbSkills2.SelectedItem);
      }
      Charm charm1;
      if ((int) num_slots == 0 && ab1 == null && ab2 == null)
      {
        charm1 = (Charm) null;
      }
      else
      {
        Charm charm2 = new Charm(num_slots);
        charm2.custom = true;
        if (ab1 != null)
          charm2.abilities.Add(new AbilityPair(ab1, am1));
        if (ab2 != null)
          charm2.abilities.Add(new AbilityPair(ab2, am2));
        charm1 = CharmDatabase.CharmExists(charm2) || CharmDatabase.CharmIsLegal(charm2) ? charm2 : (Charm) null;
      }
      return charm1;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (this.lstCharms.SelectedIndex <= -1)
        return;
      this.updating_charms = true;
      try
      {
        Charm charm = this.CreateCharm();
        if (charm != null)
        {
          this.best_charms.Remove(CharmDatabase.mycharms[this.lstCharms.SelectedIndex]);
          CharmDatabase.mycharms[this.lstCharms.SelectedIndex] = charm;
          this.lstCharms.BeginUpdate();
          this.lstCharms.Items[this.lstCharms.SelectedIndex] = (object) charm.GetName();
          this.lstCharms.EndUpdate();
          Charm.AddToOptimalList(this.best_charms, charm);
          this.UpdateBestCharmsColours();
          CharmDatabase.SaveCustom();
        }
      }
      catch (FormatException ex)
      {
      }
      this.updating_charms = false;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (this.lstCharms.SelectedIndex <= -1)
        return;
      int selectedIndex = this.lstCharms.SelectedIndex;
      CharmDatabase.mycharms.RemoveAt(this.lstCharms.SelectedIndex);
      if (selectedIndex < CharmDatabase.mycharms.Count)
        this.RefreshList(selectedIndex);
      else
        this.RefreshList(CharmDatabase.mycharms.Count - 1);
      this.UpdateBestCharms();
      CharmDatabase.SaveCustom();
    }

    private void btnAddNew_Click(object sender, EventArgs e)
    {
      this.updating_charms = true;
      try
      {
        Charm charm = this.CreateCharm();
        if (charm != null)
        {
          charm.custom = true;
          CharmDatabase.mycharms.Add(charm);
          this.lstCharms.BeginUpdate();
          this.lstCharms.Items.Add((object) charm.GetName());
          this.lstCharms.SelectedIndex = this.lstCharms.Items.Count - 1;
          this.lstCharms.EndUpdate();
          Charm.AddToOptimalList(this.best_charms, charm);
          this.UpdateBestCharmsColours();
          CharmDatabase.SaveCustom();
        }
      }
      catch (FormatException ex)
      {
      }
      this.updating_charms = false;
    }

    private void btnMoveUp_Click(object sender, EventArgs e)
    {
      if (this.lstCharms.SelectedIndex <= 0)
        return;
      int selectedIndex = this.lstCharms.SelectedIndex;
      Charm charm = CharmDatabase.mycharms[selectedIndex];
      CharmDatabase.mycharms[selectedIndex] = CharmDatabase.mycharms[selectedIndex - 1];
      CharmDatabase.mycharms[selectedIndex - 1] = charm;
      this.RefreshList(selectedIndex - 1);
      CharmDatabase.SaveCustom();
    }

    private void btnMoveDown_Click(object sender, EventArgs e)
    {
      if (this.lstCharms.SelectedIndex <= -1 || this.lstCharms.SelectedIndex >= this.lstCharms.Items.Count - 1)
        return;
      int selectedIndex = this.lstCharms.SelectedIndex;
      Charm charm = CharmDatabase.mycharms[selectedIndex];
      CharmDatabase.mycharms[selectedIndex] = CharmDatabase.mycharms[selectedIndex + 1];
      CharmDatabase.mycharms[selectedIndex + 1] = charm;
      this.RefreshList(selectedIndex + 1);
      CharmDatabase.SaveCustom();
    }

    private void UpdateFilter(ComboBox skill, ComboBox filter, Ability banned)
    {
      if (!this.update_skills)
        return;
      List<Ability> list = !this.sort_alphabetically ? Ability.static_abilities : Ability.ordered_abilities;
      this.update_skills = false;
      string str1 = (string) skill.SelectedItem;
      skill.BeginUpdate();
      skill.Items.Clear();
      skill.Items.Add((object) StringTable.text[75]);
      if (filter.SelectedIndex == 0)
      {
        List<Ability>.Enumerator enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Ability current = enumerator.Current;
          if (current != Ability.torso_inc && current != banned)
            skill.Items.Add((object) current.name);
        }
      }
      else if (filter.SelectedIndex == 1)
      {
        List<Ability>.Enumerator enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Ability current = enumerator.Current;
          if (current.tags.Count == 0 && current != Ability.torso_inc && current != banned)
            skill.Items.Add((object) current.name);
        }
      }
      else
      {
        string str2 = (string) filter.SelectedItem;
        List<Ability>.Enumerator enumerator1 = list.GetEnumerator();
label_12:
        while (enumerator1.MoveNext())
        {
          Ability current = enumerator1.Current;
          if (current != banned)
          {
            List<SkillTag>.Enumerator enumerator2 = current.tags.GetEnumerator();
            do
            {
              if (!enumerator2.MoveNext())
                goto label_12;
            }
            while (!(enumerator2.Current.name == str2));
            skill.Items.Add((object) current.name);
          }
        }
      }
      for (int index = 0; index < skill.Items.Count; ++index)
      {
        if ((string) skill.Items[index] == str1)
        {
          skill.SelectedIndex = index;
          break;
        }
      }
      skill.EndUpdate();
      this.update_skills = true;
    }

    private void cmbSkillFilters2_SelectedIndexChanged(object sender, EventArgs e)
    {
      ManageCharms manageCharms = this;
      ComboBox skill = manageCharms.cmbSkills2;
      ComboBox filter = this.cmbSkillFilters2;
      Ability ability = Ability.FindAbility((string) this.cmbSkills1.SelectedItem);
      manageCharms.UpdateFilter(skill, filter, ability);
    }

    private void cmbSkillFilters1_SelectedIndexChanged(object sender, EventArgs e)
    {
      ManageCharms manageCharms = this;
      ComboBox skill = manageCharms.cmbSkills1;
      ComboBox filter = this.cmbSkillFilters1;
      Ability ability = Ability.FindAbility((string) this.cmbSkills2.SelectedItem);
      manageCharms.UpdateFilter(skill, filter, ability);
    }

    private void cmbSkills1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbSkills1.SelectedIndex == 0)
      {
        this.cmbSkills1.SelectedIndex = -1;
      }
      else
      {
        ManageCharms manageCharms = this;
        ComboBox skill = manageCharms.cmbSkills2;
        ComboBox filter = this.cmbSkillFilters2;
        Ability ability = Ability.FindAbility((string) this.cmbSkills1.SelectedItem);
        manageCharms.UpdateFilter(skill, filter, ability);
      }
    }

    private void cmbSkills2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbSkills2.SelectedIndex == 0)
      {
        this.cmbSkills2.SelectedIndex = -1;
      }
      else
      {
        ManageCharms manageCharms = this;
        ComboBox skill = manageCharms.cmbSkills1;
        ComboBox filter = this.cmbSkillFilters1;
        Ability ability = Ability.FindAbility((string) this.cmbSkills2.SelectedItem);
        manageCharms.UpdateFilter(skill, filter, ability);
      }
    }

    private void SwitchToAbility(ComboBox cb, Ability ab)
    {
      for (int index = 0; index < cb.Items.Count; ++index)
      {
        if (Ability.FindAbility((string) cb.Items[index]) == ab)
        {
          cb.BeginUpdate();
          cb.SelectedIndex = index;
          cb.EndUpdate();
          break;
        }
      }
    }

    private void lstCharms_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.updating_charms)
        return;
      if (this.lstCharms.SelectedIndex < 0)
      {
        this.btnSave.Enabled = false;
      }
      else
      {
        this.btnSave.Enabled = true;
        Charm charm = CharmDatabase.mycharms[this.lstCharms.SelectedIndex];
        this.cmbSkills1.BeginUpdate();
        this.cmbSkills2.BeginUpdate();
        this.nudSlots.Value = (Decimal) charm.num_slots;
        this.cmbSkillFilters1.SelectedIndex = 0;
        this.cmbSkillFilters2.SelectedIndex = 0;
        this.cmbSkills1.SelectedIndex = 0;
        this.cmbSkills2.SelectedIndex = 0;
        if (charm.abilities.Count > 0)
        {
          ManageCharms manageCharms1 = this;
          ComboBox cb1 = manageCharms1.cmbSkills1;
          Ability ab1 = charm.abilities[0].ability;
          manageCharms1.SwitchToAbility(cb1, ab1);
          this.cmbAmount1.Text = Convert.ToString(charm.abilities[0].amount);
          if (charm.abilities.Count > 1)
          {
            ManageCharms manageCharms2 = this;
            ComboBox cb2 = manageCharms2.cmbSkills2;
            Ability ab2 = charm.abilities[1].ability;
            manageCharms2.SwitchToAbility(cb2, ab2);
            this.cmbAmount2.Text = Convert.ToString(charm.abilities[1].amount);
          }
        }
        this.cmbSkills1.EndUpdate();
        this.cmbSkills2.EndUpdate();
      }
    }

    private void cmbAmount_LostFocus(object sender, EventArgs e)
    {
      ComboBox comboBox = (ComboBox) sender;
      try
      {
        int num = Convert.ToInt32(comboBox.Text);
        if (comboBox == this.cmbAmount1)
        {
          if (num < 0)
          {
            comboBox.Text = "0";
          }
          else
          {
            if (num <= 10)
              return;
            comboBox.Text = "10";
          }
        }
        else if (num < -10)
        {
          comboBox.Text = "-10";
        }
        else
        {
          if (num <= 13)
            return;
          comboBox.Text = "13";
        }
      }
      catch (FormatException ex)
      {
        comboBox.Text = "0";
      }
    }

    private void btnDeleteAll_Click(object sender, EventArgs e)
    {
      if (DialogResult.OK != MessageBox.Show(StringTable.text[72], StringTable.text[102], MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2))
        return;
      CharmDatabase.mycharms.Clear();
      this.RefreshList(-1);
      CharmDatabase.SaveCustom();
    }

    private void UpdateBestCharms()
    {
      this.best_charms.Clear();
      List<Charm>.Enumerator enumerator = CharmDatabase.mycharms.GetEnumerator();
      while (enumerator.MoveNext())
        Charm.AddToOptimalList(this.best_charms, enumerator.Current);
      this.UpdateBestCharmsColours();
    }

    private void UpdateBestCharmsColours()
    {
      this.lstCharms.BeginUpdate();
      List<Charm>.Enumerator enumerator1 = CharmDatabase.mycharms.GetEnumerator();
      while (enumerator1.MoveNext())
        enumerator1.Current.optimal = false;
      List<Charm>.Enumerator enumerator2 = this.best_charms.GetEnumerator();
      while (enumerator2.MoveNext())
        enumerator2.Current.optimal = true;
      this.lstCharms.EndUpdate();
    }

    private void btnTrim_Click(object sender, EventArgs e)
    {
      this.UpdateBestCharms();
      CharmDatabase.mycharms.Clear();
      CharmDatabase.mycharms.AddRange((IEnumerable<Charm>) this.best_charms);
      this.RefreshList(-1);
      CharmDatabase.SaveCustom();
    }

    private void btnDetectTable_Click(object sender, EventArgs e)
    {
      this.detected_charm_table = CharmDatabase.DetectCharmTable();
    }

    private void btnSort_Click(object sender, EventArgs e)
    {
      Charm other = (Charm) null;
      if (this.lstCharms.SelectedIndex != -1)
        other = CharmDatabase.mycharms[this.lstCharms.SelectedIndex];
      if (this.sort_alphabetically)
        CharmDatabase.mycharms.Sort(new Comparison<Charm>(\u003CModule\u003E.CompareCharmsAlphabetically));
      else
        CharmDatabase.mycharms.Sort(new Comparison<Charm>(\u003CModule\u003E.CompareCharms));
      int new_index = -1;
      if (other != null)
      {
        for (int index = 0; index < CharmDatabase.mycharms.Count; ++index)
        {
          if (CharmDatabase.mycharms[index].op_Equality(other))
          {
            new_index = index;
            break;
          }
        }
      }
      this.RefreshList(new_index);
      CharmDatabase.SaveCustom();
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EManageCharms();
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
