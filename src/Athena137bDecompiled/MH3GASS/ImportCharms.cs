// Type: MH3GASS.ImportCharms
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MH3GASS
{
  public class ImportCharms : Form
  {
    private readonly List<Charm> charms = new List<Charm>();
    private ColumnHeader columnHeader1;
    private Button btnSelectNone;
    private Button btnSelectBest;
    private Button btnSort;
    public uint language;
    private Button btnOK;
    private Button btnCancel;
    private ListView listView1;
    private GroupBox grpCharms;
    private CheckBox chkDeleteExisting;
    private GroupBox groupBox2;
    private Container components;

    public ImportCharms()
    {
      // ISSUE: fault handler
      try
      {
        this.InitializeComponent();
        this.listView1.Columns[0].Width = -1;
        this.DialogResult = DialogResult.Cancel;
        this.Text = StringTable.text[69];
        this.btnOK.Text = StringTable.text[19];
        this.btnSelectBest.Text = StringTable.text[29];
        this.btnSelectNone.Text = StringTable.text[28];
        this.btnCancel.Text = StringTable.text[22];
        this.btnSort.Text = StringTable.text[73];
        this.chkDeleteExisting.Text = StringTable.text[71];
        this.grpCharms.Text = StringTable.text[58];
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    private void \u007EImportCharms()
    {
      if (this.components == null)
        return;
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
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.listView1 = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.grpCharms = new GroupBox();
      this.chkDeleteExisting = new CheckBox();
      this.groupBox2 = new GroupBox();
      this.btnSelectBest = new Button();
      this.btnSelectNone = new Button();
      this.btnSort = new Button();
      this.grpCharms.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      this.btnOK.Location = new Point(87, 42);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "&Import";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Location = new Point(87, 71);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listView1.CheckBoxes = true;
      this.listView1.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.listView1.Location = new Point(6, 19);
      this.listView1.MultiSelect = false;
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(335, 293);
      this.listView1.TabIndex = 2;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.List;
      this.grpCharms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpCharms.Controls.Add((Control) this.listView1);
      this.grpCharms.Location = new Point(12, 8);
      this.grpCharms.Name = "grpCharms";
      this.grpCharms.Size = new Size(347, 318);
      this.grpCharms.TabIndex = 3;
      this.grpCharms.TabStop = false;
      this.grpCharms.Text = "Charms";
      this.chkDeleteExisting.AutoSize = true;
      this.chkDeleteExisting.Checked = true;
      this.chkDeleteExisting.CheckState = CheckState.Checked;
      this.chkDeleteExisting.Location = new Point(6, 19);
      this.chkDeleteExisting.Name = "chkDeleteExisting";
      this.chkDeleteExisting.Size = new Size(134, 17);
      this.chkDeleteExisting.TabIndex = 4;
      this.chkDeleteExisting.Text = "Delete Existing Charms";
      this.chkDeleteExisting.UseVisualStyleBackColor = true;
      this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.groupBox2.Controls.Add((Control) this.btnSort);
      this.groupBox2.Controls.Add((Control) this.btnSelectBest);
      this.groupBox2.Controls.Add((Control) this.btnSelectNone);
      this.groupBox2.Controls.Add((Control) this.chkDeleteExisting);
      this.groupBox2.Controls.Add((Control) this.btnOK);
      this.groupBox2.Controls.Add((Control) this.btnCancel);
      this.groupBox2.Location = new Point(365, 8);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(174, 129);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.btnSelectBest.Location = new Point(6, 71);
      this.btnSelectBest.Name = "btnSelectBest";
      this.btnSelectBest.Size = new Size(75, 23);
      this.btnSelectBest.TabIndex = 6;
      this.btnSelectBest.Text = "Select &Best";
      this.btnSelectBest.UseVisualStyleBackColor = true;
      this.btnSelectBest.Click += new EventHandler(this.btnSelectBest_Click);
      this.btnSelectNone.Location = new Point(6, 42);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 5;
      this.btnSelectNone.Text = "Select &None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.btnSort.Location = new Point(6, 100);
      this.btnSort.Name = "btnSort";
      this.btnSort.Size = new Size(75, 23);
      this.btnSort.TabIndex = 7;
      this.btnSort.Text = "&Sort";
      this.btnSort.UseVisualStyleBackColor = true;
      this.btnSort.Click += new EventHandler(this.btnSort_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(551, 338);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.grpCharms);
      this.Name = "ImportCharms";
      this.Text = "Import Charms";
      this.grpCharms.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void AddCharms()
    {
      for (int index = 0; index < this.charms.Count; ++index)
      {
        if (!this.listView1.Items[index].Checked || this.charms[index].hacked)
          continue;
        this.charms[index].custom = true;
        CharmDatabase.mycharms.Add(this.charms[index]);
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      if (this.chkDeleteExisting != null)
        CharmDatabase.mycharms.Clear();
      this.listView1.Enabled = false;
      this.AddCharms();
      CharmDatabase.SaveCustom();
      this.Close();
    }

    public void RefreshCharmList()
    {
      this.listView1.BeginUpdate();
      this.listView1.Clear();
      List<Charm>.Enumerator enumerator = this.charms.GetEnumerator();
      while (enumerator.MoveNext())
        this.listView1.Items.Add(enumerator.Current.GetName());
      this.listView1.EndUpdate();
      this.btnSelectBest_Click((object) null, (EventArgs) null);
    }

    public void LoadCharms(string filename)
    {
      string[] strArray = File.ReadAllLines(filename, Encoding.GetEncoding("shift-jis"));
      uint num = 0U;
      foreach (string str in strArray)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        string& local = @str;
        List<string> vec = new List<string>();
        // ISSUE: explicit reference operation
        <Module>.Utility>SplitString(vec, ^local, ',');
        if (vec.Count < 2)
          continue;
        ++num;
        Charm charm = new Charm();
        charm.custom = true;
        charm.num_slots = Convert.ToUInt32(vec[1]);
        if (vec.Count >= 4 && vec[3] != "")
        {
          Ability charmAbility1 = Ability.FindCharmAbility(vec[2]);
          int i1;
          if (!<Module>.ConvertInt(ref i1, vec[3], StringTable.StringIndex.MasaxMHCharmListCorrupted))
            return;
          charm.abilities.Add(new AbilityPair(charmAbility1, i1));
          if (vec.Count == 6 && vec[5] != "")
          {
            Ability charmAbility2 = Ability.FindCharmAbility(vec[4]);
            int i2;
            if (!<Module>.ConvertInt(ref i2, vec[5], StringTable.StringIndex.MasaxMHCharmListCorrupted))
              return;
            charm.abilities.Add(new AbilityPair(charmAbility2, i2));
          }
        }
        this.charms.Add(charm);
      }
      this.RefreshCharmList();
    }

    public void btnSelectNone_Click(object sender, EventArgs e)
    {
      this.listView1.BeginUpdate();
      IEnumerator enumerator = this.listView1.Items.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((ListViewItem) enumerator.Current).Checked = false;
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        int num;
        if (disposable != null)
        {
          disposable.Dispose();
          num = 0;
        }
        else
          num = 0;
      }
      this.listView1.EndUpdate();
    }

    public void btnSelectBest_Click(object sender, EventArgs e)
    {
      this.listView1.BeginUpdate();
      List<Ability>.Enumerator enumerator = Ability.static_abilities.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.relevant = true;
      for (int index1 = 0; index1 < this.charms.Count; ++index1)
      {
        if (!this.charms[index1].hacked)
          goto label_8;
        Color red = Color.Red;
        this.listView1.Items[index1].ForeColor = red;
        continue;
label_8:
        this.listView1.Items[index1].Checked = true;
        for (int index2 = 0; index2 < index1; ++index2)
        {
          if (!this.listView1.Items[index2].Checked)
            continue;
          if (this.charms[index1].StrictlyBetterThan(this.charms[index2]))
            this.listView1.Items[index2].Checked = false;
          else if (this.charms[index2].StrictlyBetterThan(this.charms[index1]) || this.charms[index1].op_Equality(this.charms[index2]))
            this.listView1.Items[index1].Checked = false;
        }
      }
      this.listView1.EndUpdate();
    }

    public void listView1_ItemChecked(object sender, ItemCheckEventArgs e)
    {
      if (!this.charms[e.Index].hacked)
        return;
      ItemCheckEventArgs itemCheckEventArgs = e;
      int num = (int) itemCheckEventArgs.CurrentValue;
      itemCheckEventArgs.NewValue = (CheckState) num;
    }

    public void btnSort_Click(object sender, EventArgs e)
    {
      this.btnSort.Enabled = false;
      this.charms.Sort(new Comparison<Charm>(<Module>.CompareCharms));
      this.RefreshCharmList();
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EImportCharms();
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
