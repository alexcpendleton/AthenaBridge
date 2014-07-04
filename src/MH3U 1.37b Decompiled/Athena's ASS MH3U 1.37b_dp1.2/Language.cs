// Decompiled with JetBrains decompiler
// Type: Language
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;

internal class Language
{
  public readonly List<string> decorations = new List<string>();
  public readonly List<string> skills = new List<string>();
  public readonly List<string> abilities = new List<string>();
  public readonly List<string> tags = new List<string>();
  public readonly List<string> components = new List<string>();
  public readonly List<string> string_table = new List<string>();
  public readonly List<List<string>> armors = new List<List<string>>();
  public string name;

  public Language()
  {
    for (uint index = 0U; index < 5U; ++index)
      this.armors.Add(new List<string>());
  }
}
