// Type: Ability
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;

internal class Ability
{
  public static readonly List<Ability> static_abilities = new List<Ability>();
  public static readonly List<Ability> ordered_abilities = new List<Ability>();
  public static readonly Dictionary<string, Ability> static_ability_map = new Dictionary<string, Ability>();
  public static readonly Dictionary<string, Ability> charm_ability_map = new Dictionary<string, Ability>();
  public readonly List<SkillTag> tags = new List<SkillTag>();
  public readonly Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
  public string name;
  public bool relevant;
  public bool auto_guard;
  public bool related;
  public uint static_index;
  public uint ping_index;
  public int order;
  public static Ability torso_inc;

  static Ability()
  {
  }

  public Skill GetSkill(int amount)
  {
    Skill skill;
    if (amount == 0)
    {
      skill = (Skill) null;
    }
    else
    {
      int index = 0;
      Dictionary<int, Skill>.Enumerator enumerator = this.skills.GetEnumerator();
      if (amount > 0)
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Key <= amount && enumerator.Current.Key > index)
            index = enumerator.Current.Key;
        }
      }
      else
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Key >= amount && enumerator.Current.Key < index)
            index = enumerator.Current.Key;
        }
      }
      skill = index != 0 ? this.skills[index] : (Skill) null;
    }
    return skill;
  }

  public static Ability FindAbility(string name)
  {
    return name == null || !Ability.static_ability_map.ContainsKey(name) ? (Ability) null : Ability.static_ability_map[name];
  }

  public static Ability FindCharmAbility(string name)
  {
    return name == null || !Ability.charm_ability_map.ContainsKey(name) ? (Ability) null : Ability.charm_ability_map[name];
  }

  public static void UpdateOrdering()
  {
    Ability.ordered_abilities.Clear();
    Ability.ordered_abilities.AddRange((IEnumerable<Ability>) Ability.static_abilities);
    Ability.ordered_abilities.Sort(new Comparison<Ability>(<Module>.CompareAbilities));
    for (int index = 0; index < Ability.ordered_abilities.Count; ++index)
      Ability.ordered_abilities[index].order = index;
  }
}
