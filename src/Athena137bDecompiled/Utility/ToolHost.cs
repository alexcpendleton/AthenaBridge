// Type: Utility.ToolHost
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utility
{
  internal class ToolHost : ToolStripControlHost
  {
    private readonly RichTextBox rtb;
    private string rtf;

    public ToolHost(uint table_number, uint mystery, uint shining, uint timeworn, uint weathered)
    {
      RichTextBox richTextBox1 = new RichTextBox();
      // ISSUE: fault handler
      try
      {
        this.rtb = richTextBox1;
        ToolHost toolHost1 = this;
        RichTextBox richTextBox2 = toolHost1.rtb;
        // ISSUE: explicit constructor call
        ((ToolStripControlHost) toolHost1).>ctor((Control) richTextBox2);
        // ISSUE: fault handler
        try
        {
          this.rtb.SelectionColor = Color.Black;
          this.rtb.AppendText(StringTable.text[91] + " " + Convert.ToString(table_number) + ": ");
          this.rtb.SelectionColor = Color.Gray;
          this.rtb.AppendText(Convert.ToString(mystery));
          this.rtb.SelectionColor = Control.DefaultForeColor;
          this.rtb.AppendText(", ");
          this.rtb.SelectionColor = Color.DarkGoldenrod;
          this.rtb.AppendText(Convert.ToString(shining));
          this.rtb.SelectionColor = Control.DefaultForeColor;
          this.rtb.AppendText(", ");
          this.rtb.SelectionColor = Color.Red;
          this.rtb.AppendText(Convert.ToString(timeworn));
          this.rtb.AppendText(", ");
          this.rtb.SelectionColor = Color.Blue;
          this.rtb.AppendText(Convert.ToString(weathered));
          this.rtb.BorderStyle = BorderStyle.None;
          this.rtb.ReadOnly = true;
          ToolHost toolHost2 = this;
          string rtf = toolHost2.rtb.Rtf;
          toolHost2.rtf = rtf;
        }
        __fault
        {
          base.Dispose(true);
        }
      }
      __fault
      {
        this.rtb.Dispose();
      }
    }

    public override void OnPaint(PaintEventArgs args)
    {
      this.rtb.Rtf = this.rtf;
      base.OnPaint(args);
    }

    public void \u007EToolHost()
    {
    }

    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
    {
      if (param0)
      {
        try
        {
          this.\u007EToolHost();
        }
        finally
        {
          try
          {
            base.Dispose(true);
          }
          finally
          {
            this.rtb.Dispose();
          }
        }
      }
      else
        base.Dispose(false);
    }
  }
}
