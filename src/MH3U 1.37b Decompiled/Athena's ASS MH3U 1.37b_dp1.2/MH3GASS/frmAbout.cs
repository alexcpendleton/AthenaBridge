// Decompiled with JetBrains decompiler
// Type: MH3GASS.frmAbout
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MH3GASS
{
  public class frmAbout : Form
  {
    private Label label1;
    private Label lblVersion;
    private Button btnClose;
    private PictureBox pictureBox1;
    private Container components;

    public frmAbout()
    {
      // ISSUE: fault handler
      try
      {
        this.InitializeComponent();
        this.lblVersion.Text = StringTable.text[85] + " " + "1.37b";
        this.btnClose.Text = StringTable.text[36];
        this.Text = \u003CModule\u003E.StripAmpersands(StringTable.text[17]);
        string str = StringTable.text[0];
        if (str != "-")
        {
          this.Height += 20;
          Label label = new Label();
          label.AutoSize = true;
          label.Name = "lblTranslation";
          label.Text = str;
          Size size = TextRenderer.MeasureText(label.Text, label.Font);
          label.Size = size;
          Point point = new Point((this.Width - label.Width) / 2, 260);
          label.Location = point;
          this.Controls.Add((Control) label);
        }
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    private void \u007EfrmAbout()
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmAbout));
      this.label1 = new Label();
      this.lblVersion = new Label();
      this.btnClose = new Button();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(28, 213);
      this.label1.Name = "label1";
      this.label1.Size = new Size(238, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Athena's Armor Set Search for MH3G and MH3U";
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new Point(111, 235);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new Size(42, 13);
      this.lblVersion.TabIndex = 1;
      this.lblVersion.Text = "Version";
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnClose.Location = new Point(108, 263);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 2;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(25, 10);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(244, 191);
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(293, 298);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.lblVersion);
      this.Controls.Add((Control) this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAbout";
      this.Text = "About";
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EfrmAbout();
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
