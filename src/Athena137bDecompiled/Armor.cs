// Type: Armor
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal class Armor : AdvancedSearchOptions
{
  public static readonly Dictionary<string, Armor> static_armor_map = new Dictionary<string, Armor>();
  public readonly List<AbilityPair> abilities = new List<AbilityPair>();
  public readonly List<MaterialComponent> components = new List<MaterialComponent>();
  public string name;
  public uint hr;
  public uint elder_star;
  public uint num_slots;
  public uint defence;
  public uint max_defence;
  public uint rarity;
  public uint difficulty;
  public uint index;
  public uint family;
  public uint ping_index;
  public int ice_res;
  public int water_res;
  public int fire_res;
  public int thunder_res;
  public int dragon_res;
  public Gender gender;
  public HunterType type;
  public bool torso_inc;
  public bool no_skills;
  public bool is_piercing;
  public bool is_event;
  public bool jap_only;
  public Ability danger;
  public static List<Armor>[] static_armors;

  static Armor()
  {
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool IsBetterThan(Armor other, List<Ability> rel_abilities)
  {
    bool flag;
    if (this.num_slots > other.num_slots)
      flag = true;
    else if (this.no_skills && other.no_skills || this.torso_inc && other.torso_inc)
      flag = (int) this.max_defence != (int) other.max_defence ? this.max_defence > other.max_defence : this.rarity > other.rarity;
    else if (this.torso_inc || other.torso_inc)
    {
      flag = true;
    }
    else
    {
      List<Ability>.Enumerator enumerator = rel_abilities.GetEnumerator();
      while (enumerator.MoveNext())
      {
        Ability current = enumerator.Current;
        if (this.GetSkillAt(current) > other.GetSkillAt(current))
        {
          flag = true;
          goto label_11;
        }
      }
      flag = false;
    }
label_11:
    return flag;
  }

  public int GetSkillAt(Ability ability)
  {
    List<AbilityPair>.Enumerator enumerator = this.abilities.GetEnumerator();
    int num;
    while (enumerator.MoveNext())
    {
      AbilityPair current = enumerator.Current;
      if (current.ability == ability)
      {
        num = current.amount;
        goto label_5;
      }
    }
    num = 0;
label_5:
    return num;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool MatchesQuery(Query query, List<Ability> danger_skills, uint max_slots)
  {
    bool flag1;
    if (!query.include_piercings && this.is_piercing || !query.allow_event && this.is_event || !query.allow_japs && this.jap_only || (!query.allow_lower_tier && (<Module>.GetHRTier(this.hr) < <Module>.GetHRTier(query.hr) || <Module>.GetVETier(this.elder_star) < <Module>.GetVETier(query.elder_star)) || (this.type != HunterType.BOTH_TYPES && query.hunter_type != this.type || this.gender != Gender.BOTH_GENDERS && this.gender != query.gender)) || this.hr > query.hr && this.elder_star > query.elder_star)
      flag1 = false;
    else if (this.torso_inc)
    {
      flag1 = true;
    }
    else
    {
      this.danger = (Ability) null;
      List<AbilityPair>.Enumerator enumerator = this.abilities.GetEnumerator();
      while (enumerator.MoveNext())
      {
        AbilityPair current = enumerator.Current;
        if (current.amount < 0 && <Module>.Utility>Contains<struct\u0020Ability>(danger_skills, current.ability))
        {
          this.danger = current.ability;
          break;
        }
      }
      this.no_skills = true;
      bool flag2 = false;
      for (int index = 0; index < this.abilities.Count; ++index)
      {
        if (!<Module>.Utility>Contains<struct\u0020Ability>(query.rel_abilities, this.abilities[index].ability))
          continue;
        if (this.abilities[index].amount > 0)
        {
          this.no_skills = false;
          flag1 = true;
          goto label_16;
        }
        else
          flag2 = false;
      }
      flag1 = (int) this.num_slots != (int) max_slots ? this.num_slots > max_slots && this.danger == null : this.danger == null && !flag2;
    }
label_16:
    return flag1;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool ContainsAnyAbility(List<Ability> to_search)
  {
    List<AbilityPair>.Enumerator enumerator = this.abilities.GetEnumerator();
    bool flag;
    while (enumerator.MoveNext())
    {
      AbilityPair current = enumerator.Current;
      if (current.amount > 0 && <Module>.Utility>Contains<struct\u0020Ability>(to_search, current.ability))
      {
        flag = true;
        goto label_5;
      }
    }
    flag = false;
label_5:
    return flag;
  }

  public static void Load(string filename, Armor.ArmorType armor_type)
  {
    string str1 = (string) null;
    List<Armor> armors = Armor.static_armors[(int) armor_type];
    armors.Clear();
    armors.Capacity = 512;
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      while (!streamReader2.EndOfStream && str1 != "")
      {
        str1 = streamReader2.ReadLine();
        if (!(str1 == ""))
        {
          if ((int) str1[0] != 35)
          {
            List<string> vec = new List<string>();
            <Module>.Utility>SplitString(vec, str1, ',');
            if (!<Module>.ArmorExists(armors, vec[0]))
            {
              Armor armor1 = new Armor();
              armor1.torso_inc = false;
              armor1.is_event = false;
              armor1.danger = (Ability) null;
              armor1.name = vec[0];
              Gender gender = !(vec[1] == "1") ? (!(vec[1] == "2") ? Gender.BOTH_GENDERS : Gender.FEMALE) : Gender.MALE;
              armor1.gender = gender;
              HunterType hunterType = !(vec[2] == "1") ? (!(vec[2] == "2") ? HunterType.BOTH_TYPES : HunterType.GUNNER) : HunterType.BLADEMASTER;
              armor1.type = hunterType;
              armor1.rarity = (uint) Convert.ToInt32(vec[3]);
              Armor armor2 = armor1;
              int num1 = armor2.name.EndsWith("ピアス") ? 1 : 0;
              armor2.is_piercing = num1 != 0;
              armor1.is_event = false;
              armor1.num_slots = Convert.ToUInt32(vec[4]);
              armor1.hr = Convert.ToUInt32(vec[5]);
              armor1.elder_star = Convert.ToUInt32(vec[6]);
              armor1.defence = Convert.ToUInt32(vec[7]);
              armor1.max_defence = Convert.ToUInt32(vec[8]);
              armor1.fire_res = Convert.ToInt32(vec[9]);
              armor1.water_res = Convert.ToInt32(vec[10]);
              armor1.ice_res = Convert.ToInt32(vec[11]);
              armor1.thunder_res = Convert.ToInt32(vec[12]);
              armor1.dragon_res = Convert.ToInt32(vec[13]);
              armor1.difficulty = 0U;
              for (uint index = 1U; index <= 4U; ++index)
              {
                string str2 = vec[(int) index * 2 + 22];
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                string& local = @str2;
                // ISSUE: explicit reference operation
                // ISSUE: explicit reference operation
                if (!(^local != "") || (^local).StartsWith("※"))
                  continue;
                MaterialComponent materialComponent = new MaterialComponent();
                // ISSUE: explicit reference operation
                materialComponent.material = Material.FindMaterial(^local);
                materialComponent.amount = (uint) Convert.ToInt32(vec[(int) index * 2 + 23]);
                armor1.components.Add(materialComponent);
                Armor armor3 = armor1;
                int num2 = armor3.is_event | materialComponent.material.event_only ? 1 : 0;
                armor3.is_event = num2 != 0;
                Armor armor4 = armor1;
                int num3 = armor4.jap_only | materialComponent.material.jap_only ? 1 : 0;
                armor4.jap_only = num3 != 0;
                armor1.difficulty += materialComponent.material.difficulty * materialComponent.amount;
              }
              for (uint index = 0U; index < 5U; ++index)
              {
                uint num2 = (uint) ((int) index * 2 + 14);
                if (!(vec[(int) num2] != ""))
                  continue;
                AbilityPair abilityPair = new AbilityPair(Ability.FindAbility(vec[(int) num2]), 0);
                if (vec[(int) num2 + 1] != "")
                  abilityPair.amount = Convert.ToInt32(vec[(int) num2 + 1]);
                armor1.abilities.Add(abilityPair);
                if (abilityPair.ability == Ability.torso_inc)
                  armor1.torso_inc = true;
              }
              armor1.ping_index = Convert.ToUInt32(vec[32]);
              if (33U < (uint) vec.Count && vec[33] == "jEvent")
                armor1.jap_only = true;
              Armor armor5 = armor1;
              int num4 = (int) <Module>.GetFamily(armor5);
              armor5.family = (uint) num4;
              armor1.index = (uint) armors.Count;
              armors.Add(armor1);
              Armor.static_armor_map.Add(armor1.name, armor1);
            }
          }
        }
        else
          break;
      }
      streamReader2.Close();
      armors.TrimExcess();
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void LoadLanguage(string filename, Armor.ArmorType armor_type)
  {
    List<Armor> list = Armor.static_armors[(int) armor_type];
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      int index = 0;
      while (index < list.Count)
      {
        Armor armor = list[index];
        string key = streamReader2.ReadLine();
        if (key == null)
        {
          char[] anyOf = new char[2]
          {
            '/',
            '\\'
          };
          int num1 = filename.LastIndexOfAny(anyOf);
          int num2 = (int) MessageBox.Show("Unexpected end of file: not enough lines of text?", filename.Substring(num1 + 1));
          break;
        }
        else if (!(key == "") && (int) key[0] != 35)
        {
          if (Armor.static_armor_map.ContainsKey(key))
          {
            int num = (int) MessageBox.Show("Duplicate armor name in " + filename + ": " + key);
          }
          else
          {
            armor.name = key;
            Armor.static_armor_map.Add(key, armor);
            ++index;
          }
        }
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static Armor FindArmor(string name)
  {
    return !Armor.static_armor_map.ContainsKey(name) ? (Armor) null : Armor.static_armor_map[name];
  }

  public enum ArmorType
  {
    HEAD,
    BODY,
    ARMS,
    WAIST,
    LEGS,
    NumArmorTypes,
  }
}
