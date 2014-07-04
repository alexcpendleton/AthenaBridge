// Decompiled with JetBrains decompiler
// Type: CalculationData
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;

internal class CalculationData
{
  public readonly Dictionary<Ability, int> need = new Dictionary<Ability, int>();
  public readonly Dictionary<Skill, int> relevant = new Dictionary<Skill, int>();
  public readonly List<List<Decoration>> rel_decoration_map = new List<List<Decoration>>();
  public Query query;
  public Solution solution;
}
