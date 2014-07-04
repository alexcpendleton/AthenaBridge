// Decompiled with JetBrains decompiler
// Type: MH3GASS.frmFind
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MH3GASS
{
  public class frmFind : Form
  {
    public int index;
    public int length;
    public RichTextBox text_box;
    private TextBox txtFind;
    private Button btnFind;
    private Button btnClose;
    private Container components;

    public event EventHandler DialogClosing;

    public event EventHandler TextFound;

    public frmFind(RichTextBox text_box)
    {
      // ISSUE: fault handler
      try
      {
        this.InitializeComponent();
        this.text_box = text_box;
        this.txtFind.KeyDown += new KeyEventHandler(this.KeyDown);
        this.Text = \u003CModule\u003E.StripAmpersands(StringTable.text[86]);
        this.btnFind.Text = StringTable.text[37];
        this.btnClose.Text = StringTable.text[36];
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    [SpecialName]
    protected void raise_TextFound(object value0, EventArgs value1)
    {
      EventHandler eventHandler = this.\u003Cbacking_store\u003ETextFound;
      if (eventHandler == null)
        return;
      eventHandler(value0, value1);
    }

    [SpecialName]
    protected void raise_DialogClosing(object value0, EventArgs value1)
    {
      EventHandler eventHandler = this.\u003Cbacking_store\u003EDialogClosing;
      if (eventHandler == null)
        return;
      eventHandler(value0, value1);
    }

    private void \u007EfrmFind()
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
      this.txtFind = new TextBox();
      this.btnFind = new Button();
      this.btnClose = new Button();
      this.SuspendLayout();
      this.txtFind.Location = new Point(12, 12);
      this.txtFind.Name = "txtFind";
      this.txtFind.Size = new Size(207, 20);
      this.txtFind.TabIndex = 0;
      this.btnFind.Location = new Point(225, 10);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new Size(75, 23);
      this.btnFind.TabIndex = 1;
      this.btnFind.Text = "&Find Next";
      this.btnFind.UseVisualStyleBackColor = true;
      this.btnFind.Click += new EventHandler(this.btnFind_Click);
      this.btnClose.Location = new Point(306, 10);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 2;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(393, 41);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.btnFind);
      this.Controls.Add((Control) this.txtFind);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFind";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Find";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      frmFind frmFind = this;
      EventArgs eventArgs = EventArgs.Empty;
      frmFind.raise_DialogClosing((object) frmFind, eventArgs);
      this.Close();
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
      if (this.txtFind.Text == "")
        return;
      int startIndex = this.text_box.SelectionStart + this.text_box.SelectionLength;
      if (startIndex == this.text_box.Text.Length)
        startIndex = 0;
      frmFind frmFind1 = this;
      int num = frmFind1.text_box.Text.IndexOf(this.txtFind.Text, startIndex, StringComparison.CurrentCultureIgnoreCase);
      frmFind1.index = num;
      frmFind frmFind2 = this;
      int length = frmFind2.txtFind.Text.Length;
      frmFind2.length = length;
      frmFind frmFind3 = this;
      EventArgs eventArgs = EventArgs.Empty;
      frmFind3.raise_TextFound((object) frmFind3, eventArgs);
    }

    private void KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      e.Handled = true;
      e.SuppressKeyPress = true;
      frmFind frmFind = this;
      EventArgs e1 = EventArgs.Empty;
      frmFind.btnFind_Click((object) frmFind, e1);
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EfrmFind();
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
