// Type: Query
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;

internal class Query
{
  public readonly List<Skill> skills = new List<Skill>();
  public readonly List<List<Armor>> rel_armor = new List<List<Armor>>();
  public readonly List<List<Armor>> inf_armor = new List<List<Armor>>();
  public readonly List<Ability> rel_abilities = new List<Ability>();
  public readonly List<Decoration> rel_decorations = new List<Decoration>();
  public readonly List<Decoration> inf_decorations = new List<Decoration>();
  public HunterType hunter_type;
  public Gender gender;
  public uint hr;
  public uint elder_star;
  public uint weapon_slots_allowed;
  public uint charm_table;
  public bool include_piercings;
  public bool allow_bad;
  public bool allow_event;
  public bool allow_lower_tier;
  public bool allow_japs;
}
