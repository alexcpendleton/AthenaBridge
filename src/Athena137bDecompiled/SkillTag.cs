// Type: SkillTag
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;
using System.IO;

internal class SkillTag
{
  public static readonly List<SkillTag> tags = new List<SkillTag>();
  public string name = s;
  public bool disable_b = s == "Bow/Gunner";
  public bool disable_g = s == "Blademaster";

  static SkillTag()
  {
  }

  public SkillTag(string s)
  {
  }

  public static SkillTag FindTag(string tag)
  {
    List<SkillTag>.Enumerator enumerator = SkillTag.tags.GetEnumerator();
    SkillTag skillTag;
    while (enumerator.MoveNext())
    {
      SkillTag current = enumerator.Current;
      if (current.name == tag)
      {
        skillTag = current;
        goto label_5;
      }
    }
    skillTag = (SkillTag) null;
label_5:
    return skillTag;
  }

  public static void Load(string filename)
  {
    SkillTag.tags.Clear();
    SkillTag.tags.Add(new SkillTag("All"));
    SkillTag.tags.Add(new SkillTag("Misc"));
    SkillTag.tags.Add(new SkillTag("Related"));
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      while (!streamReader2.EndOfStream)
      {
        string s = streamReader2.ReadLine();
        if (s != "")
          SkillTag.tags.Add(new SkillTag(s));
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void LoadLanguage(string filename)
  {
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      int index = 0;
      while (index < SkillTag.tags.Count)
      {
        string str = streamReader2.ReadLine();
        if (!(str == "") && (int) str[0] != 35)
        {
          SkillTag.tags[index].name = str;
          ++index;
        }
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }
}
