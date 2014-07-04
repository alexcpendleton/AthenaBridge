// Decompiled with JetBrains decompiler
// Type: Decoration
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

internal class Decoration : AdvancedSearchOptions
{
  public static readonly List<Decoration> static_decorations = new List<Decoration>();
  public static readonly Dictionary<Ability, List<Decoration>> static_decoration_ability_map = new Dictionary<Ability, List<Decoration>>();
  public static readonly Dictionary<string, Decoration> static_decoration_map = new Dictionary<string, Decoration>();
  public readonly List<AbilityPair> abilities = new List<AbilityPair>();
  public readonly List<MaterialComponent> components = new List<MaterialComponent>();
  public readonly List<MaterialComponent> components2 = new List<MaterialComponent>();
  public string name;
  public uint hr;
  public uint elder_star;
  public uint slots_required;
  public uint rarity;
  public uint difficulty;
  public uint index;
  public uint ping_index;
  public bool is_event;

  [return: MarshalAs(UnmanagedType.U1)]
  public bool IsBetterThan(Decoration other, List<Ability> rel_abilities)
  {
    bool flag;
    if (this.slots_required < other.slots_required || this.abilities[0].ability != other.abilities[0].ability)
    {
      flag = true;
    }
    else
    {
      int num1 = this.abilities[0].amount * (int) other.slots_required;
      int num2 = other.abilities[0].amount * (int) this.slots_required;
      flag = num1 == num2 ? \u003CModule\u003E.NotWorse(this, other) : num1 > num2;
    }
    return flag;
  }

  public int GetSkillAt(Ability ability)
  {
    int num;
    for (int index = 0; index < this.abilities.Count; ++index)
    {
      if (this.abilities[index].ability == ability)
      {
        num = this.abilities[index].amount;
        goto label_6;
      }
    }
    num = 0;
label_6:
    return num;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool MatchesQuery(Query query)
  {
    bool flag;
    if (this.hr > query.hr && this.elder_star > query.elder_star || this.is_event && !query.allow_event)
    {
      flag = false;
    }
    else
    {
      List<Skill>.Enumerator enumerator = query.skills.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.ability == this.abilities[0].ability)
        {
          flag = true;
          goto label_7;
        }
      }
      flag = false;
    }
label_7:
    return flag;
  }

  public static void Load(string filename)
  {
    Decoration.static_decoration_map.Clear();
    Decoration.static_decoration_ability_map.Clear();
    Decoration.static_decorations.Clear();
    Decoration.static_decorations.Capacity = 256;
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      while (!streamReader2.EndOfStream)
      {
        string str1 = streamReader2.ReadLine();
        if (!(str1 == "") && (int) str1[0] != 35)
        {
          List<string> vec = new List<string>();
          \u003CModule\u003E.Utility\u002ESplitString(vec, str1, ',');
          Decoration decoration1 = new Decoration();
          decoration1.is_event = false;
          decoration1.name = vec[0];
          decoration1.rarity = Convert.ToUInt32(vec[1]);
          decoration1.slots_required = Convert.ToUInt32(vec[2]);
          decoration1.hr = Convert.ToUInt32(vec[3]);
          decoration1.elder_star = Convert.ToUInt32(vec[4]);
          decoration1.difficulty = 0U;
          for (int index = 0; index < 2; ++index)
          {
            if (vec[index * 2 + 5] == "")
              continue;
            AbilityPair abilityPair = new AbilityPair(Ability.FindAbility(vec[index * 2 + 5]), Convert.ToInt32(vec[index * 2 + 6]));
            decoration1.abilities.Add(abilityPair);
          }
          for (uint index = 0U; index < 4U; ++index)
          {
            string str2 = vec[(int) index * 2 + 9];
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            string& local = @str2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            if (^local != "" && !(^local).StartsWith("※"))
            {
              MaterialComponent materialComponent = new MaterialComponent();
              materialComponent.amount = Convert.ToUInt32(vec[(int) index * 2 + 10]);
              // ISSUE: explicit reference operation
              materialComponent.material = Material.FindMaterial(^local);
              decoration1.components.Add(materialComponent);
              Decoration decoration2 = decoration1;
              int num = decoration2.is_event | materialComponent.material.event_only ? 1 : 0;
              decoration2.is_event = num != 0;
              decoration1.difficulty += materialComponent.material.difficulty;
            }
            if (vec[(int) index * 2 + 17] != "" && !vec[(int) index * 2 + 7].StartsWith("※"))
              decoration1.components2.Add(new MaterialComponent()
              {
                amount = Convert.ToUInt32(vec[(int) index * 2 + 18]),
                material = Material.FindMaterial(vec[(int) index * 2 + 17])
              });
            decoration1.ping_index = Convert.ToUInt32(vec[25]);
          }
          decoration1.index = (uint) Decoration.static_decorations.Count;
          Decoration.static_decorations.Add(decoration1);
          if (!Decoration.static_decoration_ability_map.ContainsKey(decoration1.abilities[0].ability))
            Decoration.static_decoration_ability_map.Add(decoration1.abilities[0].ability, new List<Decoration>());
          Decoration.static_decoration_ability_map[decoration1.abilities[0].ability].Add(decoration1);
          Decoration.static_decoration_map.Add(decoration1.name, decoration1);
        }
      }
      Decoration.static_decorations.TrimExcess();
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void LoadLanguage(string filename)
  {
    Decoration.static_decoration_map.Clear();
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      int index = 0;
      while (index < Decoration.static_decorations.Count)
      {
        string key = streamReader2.ReadLine();
        if (!(key == "") && (int) key[0] != 35)
        {
          Decoration.static_decorations[index].name = key;
          Decoration.static_decoration_map.Add(key, Decoration.static_decorations[index]);
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

  public static Decoration FindDecoration(string ability_name, uint points)
  {
    List<Decoration>.Enumerator enumerator = Decoration.static_decorations.GetEnumerator();
    Decoration decoration;
    while (enumerator.MoveNext())
    {
      Decoration current = enumerator.Current;
      if (current.abilities[0].ability.name == ability_name && current.abilities[0].amount == (int) points)
      {
        decoration = current;
        goto label_5;
      }
    }
    decoration = (Decoration) null;
label_5:
    return decoration;
  }

  public static Decoration FindDecoration(string name)
  {
    return !Decoration.static_decoration_map.ContainsKey(name) ? (Decoration) null : Decoration.static_decoration_map[name];
  }

  public static Decoration GetBestDecoration(Ability ability, uint max_slots, uint hr, uint elder_star)
  {
    Decoration other = (Decoration) null;
    List<Ability> rel_abilities = new List<Ability>();
    rel_abilities.Add(ability);
    List<Decoration>.Enumerator enumerator1 = Decoration.static_decoration_ability_map[ability].GetEnumerator();
label_1:
    while (enumerator1.MoveNext())
    {
      Decoration current1 = enumerator1.Current;
      if ((current1.hr <= hr || current1.elder_star <= elder_star) && current1.slots_required <= max_slots)
      {
        List<AbilityPair>.Enumerator enumerator2 = current1.abilities.GetEnumerator();
        while (true)
        {
          AbilityPair current2;
          do
          {
            if (enumerator2.MoveNext())
              current2 = enumerator2.Current;
            else
              goto label_1;
          }
          while (current2.amount <= 0 || current2.ability != ability || other != null && !current1.IsBetterThan(other, rel_abilities));
          other = current1;
        }
      }
    }
    return other;
  }

  public static Decoration GetBestDecoration(Ability ability, uint max_slots, List<List<Decoration>> rel_deco_map)
  {
    int index = (int) max_slots + 1;
label_1:
    int num1 = index;
    --index;
    int num2 = 0;
    Decoration decoration;
    if (num1 > num2)
    {
      List<Decoration>.Enumerator enumerator = rel_deco_map[index].GetEnumerator();
      Decoration current;
      do
      {
        if (enumerator.MoveNext())
          current = enumerator.Current;
        else
          goto label_1;
      }
      while (current.abilities[0].ability != ability);
      decoration = current;
    }
    else
      decoration = (Decoration) null;
    return decoration;
  }
}
