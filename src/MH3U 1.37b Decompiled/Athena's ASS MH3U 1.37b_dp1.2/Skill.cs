// Decompiled with JetBrains decompiler
// Type: Skill
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;
using System.IO;

internal class Skill
{
  public static readonly List<Skill> static_skills = new List<Skill>();
  public static readonly List<Skill> ordered_skills = new List<Skill>();
  public static readonly Dictionary<string, Skill> static_skill_map = new Dictionary<string, Skill>();
  public string name;
  public int points_required;
  public int order;
  public int static_index;
  public int ping_index;
  public Ability ability;

  public static void Load(string filename)
  {
    string str = (string) null;
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      Ability.static_abilities.Clear();
      Ability.static_ability_map.Clear();
      Skill.static_skills.Clear();
      Skill.static_skill_map.Clear();
      Skill.ordered_skills.Clear();
      Ability.static_abilities.Capacity = 128;
      Skill.static_skills.Capacity = 256;
      while (!streamReader2.EndOfStream && str != "")
      {
        str = streamReader2.ReadLine();
        if (!(str == ""))
        {
          if ((int) str[0] != 35)
          {
            List<string> vec = new List<string>();
            \u003CModule\u003E.Utility\u002ESplitString(vec, str, ',');
            Skill skill = new Skill();
            skill.ping_index = (int) Convert.ToUInt32(vec[0]);
            skill.name = vec[2];
            if (vec[4] == "")
            {
              Ability.torso_inc = new Ability();
              Ability.torso_inc.name = vec[2];
              Ability.torso_inc.ping_index = 1U;
              Ability.torso_inc.static_index = (uint) Ability.static_abilities.Count;
              Ability.static_abilities.Add(Ability.torso_inc);
              Ability.static_ability_map[Ability.torso_inc.name] = Ability.torso_inc;
            }
            else
            {
              skill.points_required = Convert.ToInt32(vec[4]);
              skill.ability = Ability.FindAbility(vec[3]);
              if (skill.ability == null)
              {
                Ability ability1 = new Ability();
                ability1.ping_index = Convert.ToUInt32(vec[1]);
                ability1.name = vec[3];
                Ability ability2 = ability1;
                int num = ability2.name == "自動防御" ? 1 : 0;
                ability2.auto_guard = num != 0;
                ability1.static_index = (uint) Ability.static_abilities.Count;
                Ability.static_abilities.Add(ability1);
                Ability.static_ability_map[ability1.name] = ability1;
                Ability.charm_ability_map[ability1.name] = ability1;
                skill.ability = ability1;
                for (int index = 6; index < vec.Count; ++index)
                {
                  if (!(vec[index] != ""))
                    continue;
                  SkillTag tag = SkillTag.FindTag(vec[index]);
                  if (tag == null)
                    throw new Exception("Skill Tag '" + vec[index] + "' does not exist");
                  ability1.tags.Add(tag);
                }
              }
              skill.ability.skills[skill.points_required] = skill;
              skill.static_index = Skill.static_skills.Count;
              Skill.static_skills.Add(skill);
              Skill.static_skill_map[skill.name] = skill;
            }
          }
        }
        else
          break;
      }
      streamReader2.Close();
      Skill.static_skills.TrimExcess();
      Ability.static_abilities.TrimExcess();
      Ability.UpdateOrdering();
      Skill.UpdateOrdering();
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void LoadLanguage(string filename)
  {
    Ability.static_ability_map.Clear();
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      bool flag = false;
      int index1 = 0;
      while (index1 < Ability.static_abilities.Count)
      {
        string key = streamReader2.ReadLine();
        if (!(key == "") && (int) key[0] != 35)
        {
          if (!flag)
          {
            Ability.static_abilities[index1].name = key;
            Ability.static_ability_map.Add(key, Ability.static_abilities[index1]);
          }
          ++index1;
        }
      }
      Skill.static_skill_map.Clear();
      int index2 = 0;
      while (index2 < Skill.static_skills.Count)
      {
        string key = streamReader2.ReadLine();
        if (!(key == "") && (int) key[0] != 35)
        {
          if (!flag)
          {
            Skill.static_skills[index2].name = key;
            Skill.static_skill_map.Add(key, Skill.static_skills[index2]);
          }
          ++index2;
        }
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static Skill FindSkill(string name)
  {
    return !Skill.static_skill_map.ContainsKey(name) ? (Skill) null : Skill.static_skill_map[name];
  }

  public static void UpdateOrdering()
  {
    Skill.ordered_skills.Clear();
    Skill.ordered_skills.AddRange((IEnumerable<Skill>) Skill.static_skills);
    Skill.ordered_skills.Sort(new Comparison<Skill>(\u003CModule\u003E.CompareSkills));
    for (int index = 0; index < Skill.ordered_skills.Count; ++index)
      Skill.ordered_skills[index].order = index;
  }
}
