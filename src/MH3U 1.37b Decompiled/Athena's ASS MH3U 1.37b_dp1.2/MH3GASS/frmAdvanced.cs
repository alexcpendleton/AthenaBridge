// Decompiled with JetBrains decompiler
// Type: MH3GASS.frmAdvanced
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MH3GASS
{
  public class frmAdvanced : Form
  {
    public static uint AdvDefault = 8U;
    public static uint AdvForceDisable = 4U;
    public static uint AdvForceEnable = 2U;
    public static uint AdvChecked = 1U;
    private static Color color_default = Color.Black;
    private static Color color_enabled = Color.Green;
    private static Color color_disabled = Color.Gray;
    private readonly Dictionary<Button, int> button_dict = new Dictionary<Button, int>();
    public readonly List<ListView> boxes = new List<ListView>();
    public readonly List<Button> def_buttons = new List<Button>();
    public readonly List<Button> none_buttons = new List<Button>();
    private ContextMenuStrip contextMenuStrip1;
    private Query query;
    private bool manual_checking;
    private List<List<uint>> \u003Cbacking_store\u003Eresult;
    private GroupBox grpArmors;
    private ListView lvHead;
    private ListView lvLegs;
    private ListView lvWaist;
    private ListView lvArms;
    private ListView lvBody;
    private ColumnHeader columnHead;
    private ColumnHeader columnLegs;
    private ColumnHeader columnWaist;
    private ColumnHeader columnArms;
    private ColumnHeader columnBody;
    private Button btnNoneHead;
    private Button btnDefaultHead;
    private Button btnNoneBody;
    private Button btnDefaultBody;
    private Button btnNoneLegs;
    private Button btnDefaultLegs;
    private Button btnNoneWaist;
    private Button btnDefaultWaist;
    private Button btnNoneArms;
    private Button btnDefaultArms;
    private Button btnNoneDecorations;
    private Button btnDefaultDecorations;
    private ListView lvDecorations;
    private ColumnHeader columnHeader1;
    private Button btnSearch;
    private Button btnCancel;
    private GroupBox groupBox1;
    private IContainer components;

    public override Size DefaultSize
    {
      get
      {
        return new Size(1031, 587);
      }
    }

    public List<List<uint>> result
    {
      get
      {
        return this.\u003Cbacking_store\u003Eresult;
      }
      set
      {
        this.\u003Cbacking_store\u003Eresult = value;
      }
    }

    public frmAdvanced(Query query)
    {
      // ISSUE: fault handler
      try
      {
        this.manual_checking = false;
        this.InitializeComponent();
        this.query = query;
        this.grpArmors.Layout += new LayoutEventHandler(this.ArmorGroupResized);
        this.boxes.Add(this.lvHead);
        this.boxes.Add(this.lvBody);
        this.boxes.Add(this.lvArms);
        this.boxes.Add(this.lvWaist);
        this.boxes.Add(this.lvLegs);
        this.boxes.Add(this.lvDecorations);
        this.def_buttons.Add(this.btnDefaultHead);
        this.def_buttons.Add(this.btnDefaultBody);
        this.def_buttons.Add(this.btnDefaultArms);
        this.def_buttons.Add(this.btnDefaultWaist);
        this.def_buttons.Add(this.btnDefaultLegs);
        this.def_buttons.Add(this.btnDefaultDecorations);
        this.none_buttons.Add(this.btnNoneHead);
        this.none_buttons.Add(this.btnNoneBody);
        this.none_buttons.Add(this.btnNoneArms);
        this.none_buttons.Add(this.btnNoneWaist);
        this.none_buttons.Add(this.btnNoneLegs);
        this.none_buttons.Add(this.btnNoneDecorations);
        this.button_dict.Add(this.btnDefaultHead, 0);
        this.button_dict.Add(this.btnDefaultBody, 1);
        this.button_dict.Add(this.btnDefaultArms, 2);
        this.button_dict.Add(this.btnDefaultWaist, 3);
        this.button_dict.Add(this.btnDefaultLegs, 4);
        this.button_dict.Add(this.btnDefaultDecorations, 5);
        this.button_dict.Add(this.btnNoneHead, 0);
        this.button_dict.Add(this.btnNoneBody, 1);
        this.button_dict.Add(this.btnNoneArms, 2);
        this.button_dict.Add(this.btnNoneWaist, 3);
        this.button_dict.Add(this.btnNoneLegs, 4);
        this.button_dict.Add(this.btnNoneDecorations, 5);
        if (query == null)
          return;
        for (int index = 0; index < 5; ++index)
        {
          this.boxes[index].SuspendLayout();
          List<Armor>.Enumerator enumerator = query.inf_armor[index].GetEnumerator();
          while (enumerator.MoveNext())
          {
            Armor current = enumerator.Current;
            current.adv_index = (uint) this.boxes[index].Items.Count;
            frmAdvanced frmAdvanced = this;
            ListView lv = frmAdvanced.boxes[index];
            string name = current.name;
            int num = \u003CModule\u003E.Utility\u002EContains\u003Cstruct\u0020Armor\u003E(query.rel_armor[index], current) ? 1 : 0;
            Armor armor = current;
            frmAdvanced.AddCheckedItem(lv, name, num != 0, (AdvancedSearchOptions) armor);
          }
          frmAdvanced frmAdvanced1 = this;
          ListView lv1 = frmAdvanced1.boxes[index];
          frmAdvanced1.FixColumnWidth(lv1);
          this.boxes[index].ResumeLayout();
          this.boxes[index].ItemChecked += new ItemCheckedEventHandler(this.CheckBoxClicked);
        }
        this.lvDecorations.SuspendLayout();
        List<Decoration>.Enumerator enumerator1 = query.inf_decorations.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          Decoration current = enumerator1.Current;
          current.adv_index = (uint) this.lvDecorations.Items.Count;
          frmAdvanced frmAdvanced = this;
          ListView lv = frmAdvanced.lvDecorations;
          string name = current.name;
          int num = \u003CModule\u003E.Utility\u002EContains\u003Cstruct\u0020Decoration\u003E(query.rel_decorations, current) ? 1 : 0;
          Decoration decoration = current;
          frmAdvanced.AddCheckedItem(lv, name, num != 0, (AdvancedSearchOptions) decoration);
        }
        frmAdvanced frmAdvanced2 = this;
        ListView lv2 = frmAdvanced2.lvDecorations;
        frmAdvanced2.FixColumnWidth(lv2);
        this.lvDecorations.ResumeLayout();
        this.lvDecorations.ItemChecked += new ItemCheckedEventHandler(this.CheckBoxClicked);
        this.Text = \u003CModule\u003E.StripAmpersands(StringTable.text[21]);
        this.btnSearch.Text = StringTable.text[27];
        this.btnCancel.Text = StringTable.text[22];
        this.grpArmors.Text = StringTable.text[68];
        List<Button>.Enumerator enumerator2 = this.none_buttons.GetEnumerator();
        while (enumerator2.MoveNext())
          enumerator2.Current.Text = StringTable.text[24];
        List<Button>.Enumerator enumerator3 = this.def_buttons.GetEnumerator();
        while (enumerator3.MoveNext())
          enumerator3.Current.Text = StringTable.text[23];
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    public override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      this.manual_checking = true;
    }

    public unsafe void FixColumnWidth(ListView lv)
    {
      int num1 = 0;
      IEnumerator enumerator = lv.Items.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          ListViewItem listViewItem = (ListViewItem) enumerator.Current;
          int width = TextRenderer.MeasureText(listViewItem.Text, listViewItem.Font).Width;
          num1 = *\u003CModule\u003E.std\u002Emax\u003Cint\u003E(&num1, &width);
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        int num2;
        if (disposable != null)
        {
          disposable.Dispose();
          num2 = 0;
        }
        else
          num2 = 0;
      }
      lv.Columns[0].Width = num1 + 20;
    }

    private void \u007EfrmAdvanced()
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
      this.components = (IContainer) new Container();
      this.btnSearch = new Button();
      this.btnCancel = new Button();
      this.groupBox1 = new GroupBox();
      this.lvHead = new ListView();
      this.columnHead = new ColumnHeader();
      frmAdvanced frmAdvanced = this;
      ContextMenuStrip contextMenuStrip = new ContextMenuStrip(frmAdvanced.components);
      frmAdvanced.contextMenuStrip1 = contextMenuStrip;
      this.grpArmors = new GroupBox();
      this.btnNoneDecorations = new Button();
      this.btnDefaultDecorations = new Button();
      this.lvDecorations = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.btnNoneLegs = new Button();
      this.btnDefaultLegs = new Button();
      this.btnNoneWaist = new Button();
      this.btnDefaultWaist = new Button();
      this.btnNoneArms = new Button();
      this.btnDefaultArms = new Button();
      this.btnNoneBody = new Button();
      this.btnDefaultBody = new Button();
      this.btnNoneHead = new Button();
      this.btnDefaultHead = new Button();
      this.lvLegs = new ListView();
      this.columnLegs = new ColumnHeader();
      this.lvWaist = new ListView();
      this.columnWaist = new ColumnHeader();
      this.lvArms = new ListView();
      this.columnArms = new ColumnHeader();
      this.lvBody = new ListView();
      this.columnBody = new ColumnHeader();
      this.groupBox1.SuspendLayout();
      this.grpArmors.SuspendLayout();
      this.SuspendLayout();
      this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnSearch.Location = new Point(6, 13);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(75, 26);
      this.btnSearch.TabIndex = 10;
      this.btnSearch.Text = "&Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnCancel.Location = new Point(87, 13);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 26);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.groupBox1.Anchor = AnchorStyles.Bottom;
      this.groupBox1.Controls.Add((Control) this.btnSearch);
      this.groupBox1.Controls.Add((Control) this.btnCancel);
      this.groupBox1.Location = new Point(433, 491);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(168, 47);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.lvHead.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvHead.CheckBoxes = true;
      this.lvHead.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHead
      });
      this.lvHead.ContextMenuStrip = this.contextMenuStrip1;
      this.lvHead.HeaderStyle = ColumnHeaderStyle.None;
      this.lvHead.LabelWrap = false;
      this.lvHead.Location = new Point(6, 19);
      this.lvHead.MultiSelect = false;
      this.lvHead.Name = "lvHead";
      this.lvHead.Size = new Size(158, 405);
      this.lvHead.TabIndex = 0;
      this.lvHead.UseCompatibleStateImageBehavior = false;
      this.lvHead.View = View.Details;
      this.columnHead.Text = "";
      this.columnHead.Width = 137;
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(61, 4);
      this.contextMenuStrip1.Text = "asdfasdf";
      this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
      this.grpArmors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpArmors.Controls.Add((Control) this.btnNoneDecorations);
      this.grpArmors.Controls.Add((Control) this.btnDefaultDecorations);
      this.grpArmors.Controls.Add((Control) this.lvDecorations);
      this.grpArmors.Controls.Add((Control) this.btnNoneLegs);
      this.grpArmors.Controls.Add((Control) this.btnDefaultLegs);
      this.grpArmors.Controls.Add((Control) this.btnNoneWaist);
      this.grpArmors.Controls.Add((Control) this.btnDefaultWaist);
      this.grpArmors.Controls.Add((Control) this.btnNoneArms);
      this.grpArmors.Controls.Add((Control) this.btnDefaultArms);
      this.grpArmors.Controls.Add((Control) this.btnNoneBody);
      this.grpArmors.Controls.Add((Control) this.btnDefaultBody);
      this.grpArmors.Controls.Add((Control) this.btnNoneHead);
      this.grpArmors.Controls.Add((Control) this.btnDefaultHead);
      this.grpArmors.Controls.Add((Control) this.lvLegs);
      this.grpArmors.Controls.Add((Control) this.lvWaist);
      this.grpArmors.Controls.Add((Control) this.lvArms);
      this.grpArmors.Controls.Add((Control) this.lvBody);
      this.grpArmors.Controls.Add((Control) this.lvHead);
      this.grpArmors.Location = new Point(12, 12);
      this.grpArmors.Name = "grpArmors";
      this.grpArmors.Size = new Size(992, 476);
      this.grpArmors.TabIndex = 12;
      this.grpArmors.TabStop = false;
      this.grpArmors.Text = "Select Armor";
      this.btnNoneDecorations.Anchor = AnchorStyles.Bottom;
      this.btnNoneDecorations.Location = new Point(908, 430);
      this.btnNoneDecorations.Name = "btnNoneDecorations";
      this.btnNoneDecorations.Size = new Size(76, 26);
      this.btnNoneDecorations.TabIndex = 17;
      this.btnNoneDecorations.Text = "&None";
      this.btnNoneDecorations.UseVisualStyleBackColor = true;
      this.btnNoneDecorations.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultDecorations.Anchor = AnchorStyles.Bottom;
      this.btnDefaultDecorations.Location = new Point(826, 430);
      this.btnDefaultDecorations.Name = "btnDefaultDecorations";
      this.btnDefaultDecorations.Size = new Size(76, 26);
      this.btnDefaultDecorations.TabIndex = 16;
      this.btnDefaultDecorations.Text = "&Default";
      this.btnDefaultDecorations.UseVisualStyleBackColor = true;
      this.btnDefaultDecorations.Click += new EventHandler(this.DefaultChecked);
      this.lvDecorations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvDecorations.CheckBoxes = true;
      this.lvDecorations.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.lvDecorations.ContextMenuStrip = this.contextMenuStrip1;
      this.lvDecorations.HeaderStyle = ColumnHeaderStyle.None;
      this.lvDecorations.LabelWrap = false;
      this.lvDecorations.Location = new Point(826, 19);
      this.lvDecorations.MultiSelect = false;
      this.lvDecorations.Name = "lvDecorations";
      this.lvDecorations.Size = new Size(158, 405);
      this.lvDecorations.TabIndex = 15;
      this.lvDecorations.UseCompatibleStateImageBehavior = false;
      this.lvDecorations.View = View.Details;
      this.columnHeader1.Width = 137;
      this.btnNoneLegs.Anchor = AnchorStyles.Bottom;
      this.btnNoneLegs.Location = new Point(744, 430);
      this.btnNoneLegs.Name = "btnNoneLegs";
      this.btnNoneLegs.Size = new Size(76, 26);
      this.btnNoneLegs.TabIndex = 14;
      this.btnNoneLegs.Text = "&None";
      this.btnNoneLegs.UseVisualStyleBackColor = true;
      this.btnNoneLegs.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultLegs.Anchor = AnchorStyles.Bottom;
      this.btnDefaultLegs.Location = new Point(662, 430);
      this.btnDefaultLegs.Name = "btnDefaultLegs";
      this.btnDefaultLegs.Size = new Size(76, 26);
      this.btnDefaultLegs.TabIndex = 13;
      this.btnDefaultLegs.Text = "&Default";
      this.btnDefaultLegs.UseVisualStyleBackColor = true;
      this.btnDefaultLegs.Click += new EventHandler(this.DefaultChecked);
      this.btnNoneWaist.Anchor = AnchorStyles.Bottom;
      this.btnNoneWaist.Location = new Point(580, 430);
      this.btnNoneWaist.Name = "btnNoneWaist";
      this.btnNoneWaist.Size = new Size(76, 26);
      this.btnNoneWaist.TabIndex = 12;
      this.btnNoneWaist.Text = "&None";
      this.btnNoneWaist.UseVisualStyleBackColor = true;
      this.btnNoneWaist.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultWaist.Anchor = AnchorStyles.Bottom;
      this.btnDefaultWaist.Location = new Point(498, 430);
      this.btnDefaultWaist.Name = "btnDefaultWaist";
      this.btnDefaultWaist.Size = new Size(76, 26);
      this.btnDefaultWaist.TabIndex = 11;
      this.btnDefaultWaist.Text = "&Default";
      this.btnDefaultWaist.UseVisualStyleBackColor = true;
      this.btnDefaultWaist.Click += new EventHandler(this.DefaultChecked);
      this.btnNoneArms.Anchor = AnchorStyles.Bottom;
      this.btnNoneArms.Location = new Point(416, 430);
      this.btnNoneArms.Name = "btnNoneArms";
      this.btnNoneArms.Size = new Size(76, 26);
      this.btnNoneArms.TabIndex = 10;
      this.btnNoneArms.Text = "&None";
      this.btnNoneArms.UseVisualStyleBackColor = true;
      this.btnNoneArms.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultArms.Anchor = AnchorStyles.Bottom;
      this.btnDefaultArms.Location = new Point(334, 430);
      this.btnDefaultArms.Name = "btnDefaultArms";
      this.btnDefaultArms.Size = new Size(76, 26);
      this.btnDefaultArms.TabIndex = 9;
      this.btnDefaultArms.Text = "&Default";
      this.btnDefaultArms.UseVisualStyleBackColor = true;
      this.btnDefaultArms.Click += new EventHandler(this.DefaultChecked);
      this.btnNoneBody.Anchor = AnchorStyles.Bottom;
      this.btnNoneBody.Location = new Point(252, 430);
      this.btnNoneBody.Name = "btnNoneBody";
      this.btnNoneBody.Size = new Size(76, 26);
      this.btnNoneBody.TabIndex = 8;
      this.btnNoneBody.Text = "&None";
      this.btnNoneBody.UseVisualStyleBackColor = true;
      this.btnNoneBody.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultBody.Anchor = AnchorStyles.Bottom;
      this.btnDefaultBody.Location = new Point(170, 430);
      this.btnDefaultBody.Name = "btnDefaultBody";
      this.btnDefaultBody.Size = new Size(76, 26);
      this.btnDefaultBody.TabIndex = 7;
      this.btnDefaultBody.Text = "&Default";
      this.btnDefaultBody.UseVisualStyleBackColor = true;
      this.btnDefaultBody.Click += new EventHandler(this.DefaultChecked);
      this.btnNoneHead.Anchor = AnchorStyles.Bottom;
      this.btnNoneHead.Location = new Point(88, 430);
      this.btnNoneHead.Name = "btnNoneHead";
      this.btnNoneHead.Size = new Size(76, 26);
      this.btnNoneHead.TabIndex = 6;
      this.btnNoneHead.Text = "&None";
      this.btnNoneHead.UseVisualStyleBackColor = true;
      this.btnNoneHead.Click += new EventHandler(this.ClearChecked);
      this.btnDefaultHead.Anchor = AnchorStyles.Bottom;
      this.btnDefaultHead.Location = new Point(6, 430);
      this.btnDefaultHead.Name = "btnDefaultHead";
      this.btnDefaultHead.Size = new Size(76, 26);
      this.btnDefaultHead.TabIndex = 5;
      this.btnDefaultHead.Text = "&Default";
      this.btnDefaultHead.UseVisualStyleBackColor = true;
      this.btnDefaultHead.Click += new EventHandler(this.DefaultChecked);
      this.lvLegs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvLegs.CheckBoxes = true;
      this.lvLegs.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnLegs
      });
      this.lvLegs.ContextMenuStrip = this.contextMenuStrip1;
      this.lvLegs.HeaderStyle = ColumnHeaderStyle.None;
      this.lvLegs.LabelWrap = false;
      this.lvLegs.Location = new Point(662, 19);
      this.lvLegs.MultiSelect = false;
      this.lvLegs.Name = "lvLegs";
      this.lvLegs.Size = new Size(158, 405);
      this.lvLegs.TabIndex = 4;
      this.lvLegs.UseCompatibleStateImageBehavior = false;
      this.lvLegs.View = View.Details;
      this.columnLegs.Width = 137;
      this.lvWaist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvWaist.CheckBoxes = true;
      this.lvWaist.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnWaist
      });
      this.lvWaist.ContextMenuStrip = this.contextMenuStrip1;
      this.lvWaist.HeaderStyle = ColumnHeaderStyle.None;
      this.lvWaist.LabelWrap = false;
      this.lvWaist.Location = new Point(498, 19);
      this.lvWaist.MultiSelect = false;
      this.lvWaist.Name = "lvWaist";
      this.lvWaist.Size = new Size(158, 405);
      this.lvWaist.TabIndex = 3;
      this.lvWaist.UseCompatibleStateImageBehavior = false;
      this.lvWaist.View = View.Details;
      this.columnWaist.Width = 137;
      this.lvArms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvArms.CheckBoxes = true;
      this.lvArms.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnArms
      });
      this.lvArms.ContextMenuStrip = this.contextMenuStrip1;
      this.lvArms.HeaderStyle = ColumnHeaderStyle.None;
      this.lvArms.LabelWrap = false;
      this.lvArms.Location = new Point(334, 19);
      this.lvArms.MultiSelect = false;
      this.lvArms.Name = "lvArms";
      this.lvArms.Size = new Size(158, 405);
      this.lvArms.TabIndex = 2;
      this.lvArms.UseCompatibleStateImageBehavior = false;
      this.lvArms.View = View.Details;
      this.columnArms.Width = 137;
      this.lvBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.lvBody.CheckBoxes = true;
      this.lvBody.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnBody
      });
      this.lvBody.ContextMenuStrip = this.contextMenuStrip1;
      this.lvBody.HeaderStyle = ColumnHeaderStyle.None;
      this.lvBody.LabelWrap = false;
      this.lvBody.Location = new Point(170, 19);
      this.lvBody.MultiSelect = false;
      this.lvBody.Name = "lvBody";
      this.lvBody.Size = new Size(158, 405);
      this.lvBody.TabIndex = 1;
      this.lvBody.UseCompatibleStateImageBehavior = false;
      this.lvBody.View = View.Details;
      this.columnBody.Width = 137;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1015, 549);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.grpArmors);
      this.Name = "frmAdvanced";
      this.Text = "Advanced Search";
      this.groupBox1.ResumeLayout(false);
      this.grpArmors.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void ArmorGroupResized(object sender, LayoutEventArgs e)
    {
      if (this.grpArmors.Size.Width <= 36)
        return;
      int width = (this.grpArmors.Size.Width - 42) / 6;
      this.grpArmors.SuspendLayout();
      for (int index = 0; index < 6; ++index)
      {
        int x = (width + 6) * index + 6;
        this.boxes[index].SuspendLayout();
        Size size1 = this.grpArmors.Size;
        Point location = this.boxes[index].Location;
        this.boxes[index].SetBounds(x, location.Y, width, size1.Height - 72);
        this.def_buttons[index].Location.X = x;
        this.none_buttons[index].Location.X = x + width / 2 + 3;
        Size size2 = this.def_buttons[index].Size;
        Size size3 = this.grpArmors.Size;
        this.def_buttons[index].SetBounds(x, size3.Height - 48, width / 2 - 3, size2.Height);
        Size size4 = this.none_buttons[index].Size;
        Size size5 = this.grpArmors.Size;
        this.none_buttons[index].SetBounds(x + width / 2 + 3, size5.Height - 48, width / 2 - 3, size4.Height);
        this.boxes[index].ResumeLayout();
      }
      this.grpArmors.ResumeLayout();
    }

    private uint GetResultBits(ListViewItem item)
    {
      AdvancedSearchOptions advancedSearchOptions = (AdvancedSearchOptions) item.Tag;
      int num1 = !item.Checked ? 0 : 1;
      uint num2 = !advancedSearchOptions.force_enable ? 0U : 2U;
      uint num3 = !advancedSearchOptions.force_disable ? 0U : 4U;
      uint num4 = !advancedSearchOptions.default_piece ? 0U : 8U;
      int num5 = (int) num2;
      return (uint) (num1 + num5) + num3 + num4;
    }

    private void CreateResult()
    {
      this.result = new List<List<uint>>();
      for (int index = 0; index < 5; ++index)
      {
        this.result.Add(new List<uint>());
        IEnumerator enumerator = this.boxes[index].Items.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            ListViewItem listViewItem = (ListViewItem) enumerator.Current;
            this.result[index].Add(this.GetResultBits(listViewItem));
          }
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
      }
      this.result.Add(new List<uint>());
      IEnumerator enumerator1 = this.lvDecorations.Items.GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
          this.result[5].Add(this.GetResultBits((ListViewItem) enumerator1.Current));
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
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.CreateResult();
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.CreateResult();
      this.Close();
    }

    private void AddCheckedItem(ListView lv, string name, [MarshalAs(UnmanagedType.U1)] bool @checked, AdvancedSearchOptions tag)
    {
      ListViewItem listViewItem1 = new ListViewItem(name);
      tag.default_piece = @checked;
      listViewItem1.Checked = @checked;
      tag.force_disable = false;
      tag.force_enable = false;
      listViewItem1.Tag = (object) tag;
      if (@checked)
      {
        ListViewItem listViewItem2 = listViewItem1;
        Font font = new Font(listViewItem2.Font.Name, listViewItem1.Font.Size, listViewItem1.Font.Style | FontStyle.Bold);
        listViewItem2.Font = font;
      }
      lv.Items.Add(listViewItem1);
    }

    private void ClearChecked(object sender, EventArgs e)
    {
      this.manual_checking = false;
      ListView listView = this.boxes[this.button_dict[(Button) sender]];
      listView.BeginUpdate();
      IEnumerator enumerator = listView.Items.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          ListViewItem listViewItem = (ListViewItem) enumerator.Current;
          listViewItem.Checked = false;
          listViewItem.ForeColor = frmAdvanced.color_disabled;
          AdvancedSearchOptions advancedSearchOptions = (AdvancedSearchOptions) listViewItem.Tag;
          advancedSearchOptions.force_disable = true;
          advancedSearchOptions.force_enable = false;
        }
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
      listView.EndUpdate();
      this.manual_checking = true;
    }

    private void DefaultChecked(object sender, EventArgs e)
    {
      this.manual_checking = false;
      ListView listView = this.boxes[this.button_dict[(Button) sender]];
      listView.BeginUpdate();
      IEnumerator enumerator = listView.Items.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          ListViewItem listViewItem1 = (ListViewItem) enumerator.Current;
          ListViewItem listViewItem2 = listViewItem1;
          int num = listViewItem2.Font.Bold ? 1 : 0;
          listViewItem2.Checked = num != 0;
          listViewItem1.ForeColor = frmAdvanced.color_default;
          AdvancedSearchOptions advancedSearchOptions = (AdvancedSearchOptions) listViewItem1.Tag;
          advancedSearchOptions.force_disable = false;
          advancedSearchOptions.force_enable = false;
        }
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
      listView.EndUpdate();
      this.manual_checking = true;
    }

    public void CheckResult(List<List<uint>> result)
    {
      if (result == null)
        return;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        for (int index2 = 0; index2 < result[index1].Count; ++index2)
        {
          ListViewItem listViewItem1 = this.boxes[index1].Items[index2];
          AdvancedSearchOptions advancedSearchOptions = (AdvancedSearchOptions) listViewItem1.Tag;
          uint num1 = result[index1][index2];
          byte num2 = ((int) num1 & 1) == 0 ? (byte) 0 : (byte) 1;
          listViewItem1.Checked = (bool) num2;
          int num3 = ((int) num1 & 2) == 0 ? 0 : 1;
          advancedSearchOptions.force_enable = num3 != 0;
          int num4 = ((int) num1 & 4) == 0 ? 0 : 1;
          advancedSearchOptions.force_disable = num4 != 0;
          int num5 = ((int) num1 & 8) == 0 ? 0 : 1;
          advancedSearchOptions.default_piece = num5 != 0;
          Color color = !advancedSearchOptions.force_enable ? (!advancedSearchOptions.force_disable ? frmAdvanced.color_default : frmAdvanced.color_disabled) : frmAdvanced.color_enabled;
          listViewItem1.ForeColor = color;
          if (!advancedSearchOptions.default_piece)
            goto label_9;
          ListViewItem listViewItem2 = listViewItem1;
          Font font1 = new Font(listViewItem2.Font.Name, listViewItem1.Font.Size, listViewItem1.Font.Style | FontStyle.Bold);
          listViewItem2.Font = font1;
          continue;
label_9:
          ListViewItem listViewItem3 = listViewItem1;
          Font font2 = new Font(listViewItem3.Font.Name, listViewItem1.Font.Size, listViewItem1.Font.Style);
          listViewItem3.Font = font2;
        }
      }
    }

    private void RecalculateDefaults\u003CDecoration\u003E(ListView lv)
    {
      List<Decoration> inf_decs = new List<Decoration>();
      List<Decoration> list = new List<Decoration>();
      for (int index = 0; index < lv.Items.Count; ++index)
      {
        Decoration decoration = (Decoration) lv.Items[index].Tag;
        \u003CModule\u003E.AddToList(list, decoration, this.query.rel_abilities, inf_decs, true);
      }
      List<Decoration>.Enumerator enumerator1 = list.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        Decoration current = enumerator1.Current;
        if (!current.force_enable && !current.force_disable && !lv.Items[(int) current.adv_index].Checked)
          lv.Items[(int) current.adv_index].Checked = true;
      }
      List<Decoration>.Enumerator enumerator2 = inf_decs.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        Decoration current = enumerator2.Current;
        if (!current.force_enable && !current.force_disable && (lv.Items[(int) current.adv_index].Checked && !\u003CModule\u003E.Utility\u002EContains\u003Cstruct\u0020Decoration\u003E(list, current)))
          lv.Items[(int) current.adv_index].Checked = false;
      }
    }

    private void RecalculateDefaults\u003CArmor\u003E(ListView lv)
    {
      List<Armor> inf_armor = new List<Armor>();
      List<Armor> list = new List<Armor>();
      for (int index = 0; index < lv.Items.Count; ++index)
      {
        Armor armor = (Armor) lv.Items[index].Tag;
        \u003CModule\u003E.AddToList(list, armor, this.query.rel_abilities, inf_armor, true);
      }
      List<Armor>.Enumerator enumerator1 = list.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        Armor current = enumerator1.Current;
        if (!current.force_enable && !current.force_disable && !lv.Items[(int) current.adv_index].Checked)
          lv.Items[(int) current.adv_index].Checked = true;
      }
      List<Armor>.Enumerator enumerator2 = inf_armor.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        Armor current = enumerator2.Current;
        if (!current.force_enable && !current.force_disable && (lv.Items[(int) current.adv_index].Checked && !\u003CModule\u003E.Utility\u002EContains\u003Cstruct\u0020Armor\u003E(list, current)))
          lv.Items[(int) current.adv_index].Checked = false;
      }
    }

    private void RecheckDefaultItems(object sender)
    {
      if (sender == this.lvDecorations)
      {
        frmAdvanced frmAdvanced = this;
        ListView lv = frmAdvanced.lvDecorations;
        frmAdvanced.RecalculateDefaults\u003CDecoration\u003E(lv);
      }
      else
      {
        List<ListView>.Enumerator enumerator = this.boxes.GetEnumerator();
        while (enumerator.MoveNext())
        {
          ListView current = enumerator.Current;
          if (current == sender)
          {
            this.RecalculateDefaults\u003CArmor\u003E(current);
            break;
          }
        }
      }
    }

    private void CheckBoxClicked(object sender, ItemCheckedEventArgs e)
    {
      if (!this.manual_checking)
        return;
      this.manual_checking = false;
      AdvancedSearchOptions advancedSearchOptions = (AdvancedSearchOptions) e.Item.Tag;
      if (e.Item.Checked)
      {
        if (advancedSearchOptions.force_disable)
        {
          e.Item.ForeColor = frmAdvanced.color_default;
        }
        else
        {
          advancedSearchOptions.force_enable = true;
          e.Item.ForeColor = frmAdvanced.color_enabled;
        }
        advancedSearchOptions.force_disable = false;
      }
      else
      {
        if (advancedSearchOptions.force_enable)
        {
          e.Item.ForeColor = frmAdvanced.color_default;
        }
        else
        {
          advancedSearchOptions.force_disable = true;
          e.Item.ForeColor = frmAdvanced.color_disabled;
        }
        advancedSearchOptions.force_enable = false;
      }
      this.RecheckDefaultItems(sender);
      this.manual_checking = true;
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      this.contextMenuStrip1.Items.Clear();
      e.Cancel = true;
      if (this.lvDecorations.Focused && this.lvDecorations.SelectedIndices.Count == 1)
      {
        \u003CModule\u003E.Utility\u002EUpdateContextMenu(this.contextMenuStrip1, this.query.inf_decorations[this.lvDecorations.SelectedIndices[0]]);
        e.Cancel = false;
      }
      else
      {
        for (uint index = 0U; index < 5U; ++index)
        {
          if (this.boxes[(int) index].Focused && this.boxes[(int) index].SelectedIndices.Count == 1)
          {
            \u003CModule\u003E.Utility\u002EUpdateContextMenu(this.contextMenuStrip1, this.query.inf_armor[(int) index][this.boxes[(int) index].SelectedIndices[0]]);
            e.Cancel = false;
            break;
          }
        }
      }
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EfrmAdvanced();
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
