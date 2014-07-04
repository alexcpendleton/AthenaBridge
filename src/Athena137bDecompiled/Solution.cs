// Type: Solution
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using <CppImplementationDetails>;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

internal class Solution
{
  public readonly List<Armor> armors = new List<Armor>();
  public readonly List<Decoration> decorations = new List<Decoration>();
  public readonly List<Decoration> body_decorations = new List<Decoration>();
  public readonly List<Decoration> non_body_decorations = new List<Decoration>();
  public readonly List<Skill> extra_skills = new List<Skill>();
  public readonly List<Skill> bad_skills = new List<Skill>();
  public readonly Dictionary<Ability, int> abilities = new Dictionary<Ability, int>();
  public Charm charm;
  public uint torso_slots_spare;
  public uint torso_multiplier;
  public uint total_slots_spare;
  public int fire_res;
  public int ice_res;
  public int water_res;
  public int thunder_res;
  public int dragon_res;
  public int extra_skill_score;
  public uint defence;
  public uint max_defence;
  public uint rarity;
  public uint difficulty;
  public uint[] slots_spare;
  public uint[] family_score;
  public bool doubled;
  public bool reordered_to_fix;
  private CalculationData data;

  [return: MarshalAs(UnmanagedType.U1)]
  public bool MatchesQuery(Query query)
  {
    this.data = new CalculationData();
    this.data.query = query;
    this.data.solution = this;
    for (int index = 0; index < 4; ++index)
      this.data.rel_decoration_map.Add(new List<Decoration>());
    List<Decoration>.Enumerator enumerator1 = query.rel_decorations.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      Decoration current = enumerator1.Current;
      this.data.rel_decoration_map[(int) current.slots_required].Add(current);
    }
    <Module>.GetInitialData(this.data);
    <Module>.CalculateDecorations<1\u002C1>(this.data);
    bool flag;
    if (!this.HaveRequiredSkills(query))
    {
      flag = false;
    }
    else
    {
      this.CalculateExtraSkills();
      this.reordered_to_fix = false;
      if (!query.allow_bad && !this.CheckBadSkills())
      {
        flag = false;
      }
      else
      {
        if (this.charm != null)
        {
          if (this.charm.custom)
          {
            if (!this.reordered_to_fix)
              this.RearrangeDecorations();
          }
          else
          {
            this.ReduceSlots();
            this.ReduceSkills();
            this.RearrangeDecorations();
            this.ReduceCharm();
            this.CalculateExtraSkills();
          }
        }
        this.CalculateExtraSkillScore(query.hr, query.elder_star);
        this.slots_spare[(int) this.torso_slots_spare] = this.slots_spare[(int) this.torso_slots_spare] + 1U;
        List<Decoration>.Enumerator enumerator2 = this.decorations.GetEnumerator();
        while (enumerator2.MoveNext())
          this.difficulty += enumerator2.Current.difficulty;
        flag = true;
      }
    }
    return flag;
  }

  public void CalculateData(uint hr, uint elder_star)
  {
    this.abilities.Clear();
    this.max_defence = 0U;
    this.difficulty = 0U;
    this.rarity = 0U;
    this.defence = 0U;
    this.dragon_res = 0;
    this.water_res = 0;
    this.thunder_res = 0;
    this.ice_res = 0;
    this.fire_res = 0;
    this.torso_multiplier = 1U;
    List<Armor>.Enumerator enumerator1 = this.armors.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      Armor current = enumerator1.Current;
      if (current != null)
      {
        this.fire_res += current.fire_res;
        this.ice_res += current.ice_res;
        this.thunder_res += current.thunder_res;
        this.water_res += current.water_res;
        this.dragon_res += current.dragon_res;
        this.defence += current.defence;
        this.rarity += current.rarity;
        this.difficulty += current.difficulty;
        this.max_defence += current.max_defence;
        if (current.torso_inc)
          ++this.torso_multiplier;
        else
          <Module>.Utility>AddAbilitiesToMap(current.abilities, this.abilities, 1);
      }
    }
    if (this.armors[1] != null && this.torso_multiplier > 1U)
      <Module>.Utility>AddAbilitiesToMap(this.armors[1].abilities, this.abilities, (int) this.torso_multiplier - 1);
    if (this.charm != null)
      <Module>.Utility>AddAbilitiesToMap(this.charm.abilities, this.abilities, 1);
    List<Decoration>.Enumerator enumerator2 = this.non_body_decorations.GetEnumerator();
    while (enumerator2.MoveNext())
      <Module>.Utility>AddAbilitiesToMap(enumerator2.Current.abilities, this.abilities, 1);
    List<Decoration>.Enumerator enumerator3 = this.body_decorations.GetEnumerator();
    while (enumerator3.MoveNext())
      <Module>.Utility>AddAbilitiesToMap(enumerator3.Current.abilities, this.abilities, (int) this.torso_multiplier);
    this.CalculateFamilyScore();
    this.CalculateExtraSkillScore(hr, elder_star);
  }

  public void CalculateFamilyScore()
  {
    this.family_score = new uint[3];
    uint[] array = new uint[(int) Family.count];
    List<Armor>.Enumerator enumerator1 = this.armors.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      Armor current = enumerator1.Current;
      if (current != null)
        array[(int) current.family] = array[(int) current.family] + 1U;
    }
    uint[] numArray1 = new uint[6];
    for (int index = 0; index < numArray1.Length; ++index)
      numArray1[index] = Family.count;
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index] <= 0U)
        continue;
      numArray1[(int) array[index]] = (uint) index;
    }
    Array.Sort<uint>(array);
    switch (array[(int) Family.count - 1] - 2U)
    {
      case 0U:
        this.family_score[1] = <Module>.GetFamilyScore(this.armors, numArray1[2]);
        uint[] numArray2 = new uint[3]
        {
          uint.MaxValue,
          uint.MaxValue,
          uint.MaxValue
        };
        int index1 = 0;
        List<Armor>.Enumerator enumerator2 = this.armors.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          Armor current = enumerator2.Current;
          if (current != null && (int) current.family != (int) numArray1[2])
          {
            numArray2[index1] = current.family;
            ++index1;
          }
        }
        if ((int) numArray2[0] == (int) numArray2[1])
        {
          this.family_score[0] = 2U;
          this.family_score[2] = <Module>.GetFamilyScore(this.armors, numArray2[0]);
          break;
        }
        else if ((int) numArray2[0] == (int) numArray2[2])
        {
          this.family_score[0] = 2U;
          this.family_score[2] = <Module>.GetFamilyScore(this.armors, numArray2[0]);
          break;
        }
        else if ((int) numArray2[1] == (int) numArray2[2])
        {
          this.family_score[0] = 2U;
          this.family_score[2] = <Module>.GetFamilyScore(this.armors, numArray2[1]);
          break;
        }
        else
        {
          this.family_score[0] = 1U;
          break;
        }
      case 1U:
        this.family_score[1] = <Module>.GetFamilyScore(this.armors, numArray1[3]);
        if ((int) numArray1[2] != (int) Family.count)
        {
          this.family_score[0] = 4U;
          this.family_score[2] = <Module>.GetFamilyScore(this.armors, numArray1[2]);
          break;
        }
        else
        {
          this.family_score[0] = 3U;
          break;
        }
      case 2U:
        this.family_score[0] = 5U;
        this.family_score[1] = <Module>.GetFamilyScore(this.armors, numArray1[4]);
        break;
      case 3U:
        this.family_score[0] = 6U;
        break;
    }
  }

  public unsafe void CalculateExtraSkillScore(uint hr, uint elder_star)
  {
    $ArrayType$$$BY07H arrayTypeBy07H;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) @arrayTypeBy07H = 1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 4) = 5;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 8) = 20;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 12) = 50;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 16) = 100;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 20) = 1000;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 24) = 10000;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy07H + 28) = 100000;
    this.extra_skill_score = 0;
    Dictionary<Ability, int>.Enumerator enumerator = this.abilities.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (enumerator.Current.Value < 10 && enumerator.Current.Value > 3)
      {
        KeyValuePair<Ability, int> current1 = enumerator.Current;
        KeyValuePair<Ability, int> current2 = enumerator.Current;
        KeyValuePair<Ability, int> current3 = enumerator.Current;
        KeyValuePair<Ability, int> current4 = enumerator.Current;
        KeyValuePair<Ability, int> current5 = enumerator.Current;
        int num = current1.Value + <Module>.CalculateBonusExtraSkillPoints(current2.Key, (int) this.torso_slots_spare, (int) this.torso_multiplier, hr, elder_star) + <Module>.CalculateBonusExtraSkillPoints(current3.Key, (int) this.slots_spare[3] * 3, 1, hr, elder_star) + <Module>.CalculateBonusExtraSkillPoints(current4.Key, (int) this.slots_spare[2] * 2, 1, hr, elder_star) + <Module>.CalculateBonusExtraSkillPoints(current5.Key, (int) this.slots_spare[1], 1, hr, elder_star);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        this.extra_skill_score += ^(int&) ((IntPtr) &arrayTypeBy07H + Math.Min(num - 3, 10) * 4);
      }
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool FixBadSkill(Skill skill)
  {
    Decoration decoration = (Decoration) null;
    bool flag;
    while (this.torso_slots_spare > 0U)
    {
      Decoration availableDecoration = <Module>.GetBestAvailableDecoration(this.data, skill.ability, this.torso_slots_spare);
      if (availableDecoration == null || <Module>.Detrimental(availableDecoration) && !<Module>.CanTankDetrimental(availableDecoration, this.data))
      {
        flag = false;
        goto label_28;
      }
      else
      {
        this.torso_slots_spare -= availableDecoration.slots_required;
        if (<Module>.AddDecoration(this.data.solution, availableDecoration, (int) this.torso_multiplier, this.body_decorations))
        {
          flag = true;
          goto label_28;
        }
      }
    }
    int index1 = 3;
    while (index1 > 0 && (int) this.slots_spare[index1] == 0)
      --index1;
    if (index1 == 0)
    {
      flag = false;
    }
    else
    {
      uint num = (uint) (skill.points_required - this.abilities[skill.ability] + 1);
      for (int index2 = 1; index2 <= index1; ++index2)
      {
        decoration = <Module>.GetBestAvailableDecoration(this.data, skill.ability, (uint) index2);
        if (decoration != null && decoration.abilities[0].amount >= (int) num)
          break;
      }
      if (decoration == null || <Module>.Detrimental(decoration) && !<Module>.CanTankDetrimental(decoration, this.data))
      {
        flag = false;
      }
      else
      {
label_17:
        while (index1 > 0)
        {
          if ((int) this.slots_spare[(int) decoration.slots_required] == 0)
          {
            if ((int) this.slots_spare[(int) decoration.slots_required + 1] == 0)
            {
              this.slots_spare[3] = this.slots_spare[3] - 1U;
              this.slots_spare[2] = this.slots_spare[2] + 1U;
            }
            else
            {
              this.slots_spare[(int) decoration.slots_required + 1] = this.slots_spare[(int) decoration.slots_required + 1] - 1U;
              this.slots_spare[1] = this.slots_spare[1] + 1U;
            }
          }
          else
            this.slots_spare[(int) decoration.slots_required] = this.slots_spare[(int) decoration.slots_required] - 1U;
          if (<Module>.AddDecoration(this.data.solution, decoration, 1, this.non_body_decorations))
          {
            flag = true;
            goto label_28;
          }
          else
          {
            while (true)
            {
              if (index1 > 0 && (int) this.slots_spare[index1] == 0)
              {
                --index1;
                decoration = <Module>.GetBestAvailableDecoration(this.data, skill.ability, (uint) index1);
              }
              else
                goto label_17;
            }
          }
        }
        flag = false;
      }
    }
label_28:
    return flag;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool FixBadSkills()
  {
    List<Skill>.Enumerator enumerator = this.bad_skills.GetEnumerator();
    bool flag;
    while (enumerator.MoveNext())
    {
      if (!this.FixBadSkill(enumerator.Current))
      {
        flag = false;
        goto label_5;
      }
    }
    this.CalculateExtraSkills();
    flag = this.bad_skills.Count == 0;
label_5:
    return flag;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool CheckBadSkills()
  {
    return this.bad_skills.Count == 0 || this.total_slots_spare > 0U && this.FixBadSkills() || this.ReorderGems();
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool HaveRequiredSkills(Query query)
  {
    List<Skill>.Enumerator enumerator = query.skills.GetEnumerator();
    bool flag;
    while (enumerator.MoveNext())
    {
      Skill current = enumerator.Current;
      if (this.abilities[current.ability] < this.data.need[current.ability])
      {
        flag = false;
        goto label_5;
      }
    }
    flag = true;
label_5:
    return flag;
  }

  private unsafe Decoration Count1SocketGems(Ability ability, int* num)
  {
    *num = 0;
    Dictionary<Decoration, uint> dictionary = new Dictionary<Decoration, uint>();
    Decoration decoration = (Decoration) null;
    List<Decoration>.Enumerator enumerator = this.decorations.GetEnumerator();
    while (enumerator.MoveNext())
    {
      Decoration current = enumerator.Current;
      if ((int) current.slots_required == 1 && current.abilities.Count == 2 && current.abilities[1].ability == ability)
      {
        if (dictionary.ContainsKey(current))
        {
          uint num1 = dictionary[current] + 1U;
          dictionary[current] = num1;
          int num2 = (int) num1;
          if (num2 > *num)
          {
            *num = num2;
            decoration = current;
          }
        }
        else
        {
          dictionary.Add(current, 1U);
          if (*num == 0)
          {
            *num = 1;
            decoration = current;
          }
        }
      }
    }
    return decoration;
  }

  private int GetReplacable(Ability ability, uint slots)
  {
    int num;
    for (int index = 0; index < this.decorations.Count; ++index)
    {
      if ((int) this.decorations[index].slots_required == (int) slots && this.decorations[index].abilities.Count > 1 && this.decorations[index].abilities[1].ability != ability)
      {
        num = index;
        goto label_6;
      }
    }
    num = -1;
label_6:
    return num;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private unsafe bool ReorderGems()
  {
    List<Skill>.Enumerator enumerator = this.bad_skills.GetEnumerator();
    bool flag;
    while (enumerator.MoveNext())
    {
      Skill current = enumerator.Current;
      if (this.abilities[current.ability] < -9)
      {
        int num1;
        Decoration decoration1 = this.Count1SocketGems(current.ability, &num1);
        if (num1 == 0)
        {
          flag = false;
          goto label_20;
        }
        else
        {
          Decoration availableDecoration = <Module>.GetBestAvailableDecoration(this.data, decoration1.abilities[0].ability, 3U);
          int num2 = availableDecoration.abilities[0].amount;
          if (num1 < num2)
          {
            flag = false;
            goto label_20;
          }
          else
          {
            int replacable = this.GetReplacable(current.ability, availableDecoration.slots_required);
            if (replacable != -1 && num1 >= this.decorations[replacable].abilities[0].amount && this.abilities[this.decorations[replacable].abilities[1].ability] > -8)
            {
              Decoration decoration2 = Decoration.static_decoration_ability_map[this.decorations[replacable].abilities[0].ability][0];
              this.Replace((uint) replacable, availableDecoration);
              int num3 = 0;
              int num4 = this.decorations[replacable].abilities[0].amount <= num2 ? num2 : this.decorations[replacable].abilities[0].amount;
              for (int index = 0; index < this.decorations.Count; ++index)
              {
                if (this.decorations[index] != decoration1)
                  continue;
                int num5 = num3;
                int num6 = num4;
                ++num3;
                int num7 = num6;
                if (num5 < num7)
                  this.Replace((uint) index, decoration2);
              }
              if (decoration2.abilities.Count > 1 && (!this.abilities.ContainsKey(decoration2.abilities[1].ability) ? 0 : this.abilities[decoration2.abilities[1].ability]) < (!this.data.need.ContainsKey(decoration2.abilities[1].ability) ? 0 : this.data.need[decoration2.abilities[1].ability]))
              {
                flag = false;
                goto label_20;
              }
              else if (this.abilities[current.ability] < -9)
              {
                flag = false;
                goto label_20;
              }
            }
            else
            {
              flag = false;
              goto label_20;
            }
          }
        }
      }
    }
    this.CalculateExtraSkills();
    this.reordered_to_fix = true;
    flag = true;
label_20:
    return flag;
  }

  private void Replace(uint index, Decoration decoration)
  {
    List<AbilityPair>.Enumerator enumerator1 = this.decorations[(int) index].abilities.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      AbilityPair current = enumerator1.Current;
      this.abilities[current.ability] = this.abilities[current.ability] - current.amount;
    }
    this.decorations[(int) index] = decoration;
    List<AbilityPair>.Enumerator enumerator2 = decoration.abilities.GetEnumerator();
    while (enumerator2.MoveNext())
    {
      AbilityPair current = enumerator2.Current;
      if (this.abilities.ContainsKey(current.ability))
        this.abilities[current.ability] = this.abilities[current.ability] + current.amount;
      else
        this.abilities.Add(current.ability, current.amount);
    }
  }

  private void CalculateExtraSkills()
  {
    this.extra_skills.Clear();
    this.bad_skills.Clear();
    Dictionary<Ability, int>.Enumerator enumerator = this.abilities.GetEnumerator();
    while (enumerator.MoveNext())
    {
      ValueType valueType = (ValueType) enumerator.Current;
      Skill skill = ((KeyValuePair<Ability, int>) valueType).Key.GetSkill(((KeyValuePair<Ability, int>) valueType).Value);
      if (skill != null && !this.data.relevant.ContainsKey(skill))
      {
        if (((KeyValuePair<Ability, int>) valueType).Value < 0 && !this.data.query.allow_bad)
          this.bad_skills.Add(skill);
        else
          this.extra_skills.Add(skill);
      }
    }
  }

  private void ReduceSlots()
  {
    if (this.total_slots_spare <= 0U)
      return;
    uint num = this.charm.num_slots;
    while (num <= 3U && (int) this.slots_spare[(int) num] == 0)
      ++num;
    if (num > 3U)
      return;
    this.slots_spare[(int) num - (int) this.charm.num_slots] = this.slots_spare[(int) num - (int) this.charm.num_slots] + 1U;
    this.slots_spare[(int) num] = this.slots_spare[(int) num] - 1U;
    this.total_slots_spare -= this.charm.num_slots;
    this.charm.num_slots = 0U;
  }

  private void ReduceSkills()
  {
    if (this.total_slots_spare <= 0U)
      return;
    List<AbilityPair>.Enumerator enumerator1 = this.charm.abilities.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      AbilityPair current = enumerator1.Current;
      this.data.need[current.ability] = this.data.need[current.ability] + current.amount;
    }
    <Module>.CalculateDecorations<0\u002C0>(this.data);
    List<AbilityPair>.Enumerator enumerator2 = this.charm.abilities.GetEnumerator();
    while (enumerator2.MoveNext())
    {
      AbilityPair current = enumerator2.Current;
      this.data.need[current.ability] = this.data.need[current.ability] - current.amount;
    }
  }

  private void ReduceCharm()
  {
    List<AbilityPair>.Enumerator enumerator = this.charm.abilities.GetEnumerator();
    while (enumerator.MoveNext())
    {
      AbilityPair current = enumerator.Current;
      int num1 = this.data.need[current.ability];
      if (num1 > 0)
      {
        int num2 = this.abilities[current.ability] - num1;
        int num3 = num2 < current.amount ? num2 : current.amount;
        current.amount -= num3;
        this.abilities[current.ability] = this.abilities[current.ability] - num3;
      }
    }
    for (int index = 0; index < this.charm.abilities.Count; ++index)
    {
      if (this.charm.abilities[index].amount != 0)
        continue;
      this.charm.abilities.RemoveAt(index);
      --index;
    }
    if ((int) this.charm.num_slots != 0 || this.charm.abilities.Count != 0)
      return;
    this.charm = (Charm) null;
  }

  private void RearrangeDecorations()
  {
    List<AbilityPair>.Enumerator enumerator = this.charm.abilities.GetEnumerator();
label_1:
    if (!enumerator.MoveNext())
      return;
    AbilityPair current = enumerator.Current;
    uint num = <Module>.Num1SlotDecorations(this, current.ability);
    Decoration bestDecoration1 = Decoration.GetBestDecoration(current.ability, 3U, this.data.rel_decoration_map);
    Decoration bestDecoration2 = Decoration.GetBestDecoration(current.ability, 2U, this.data.rel_decoration_map);
    bool flag;
    do
    {
      flag = false;
      if (num >= 3U)
      {
        if (bestDecoration1 != null && (int) bestDecoration1.slots_required == 3 && <Module>.RoomFor3Slot(this.data))
        {
          flag = true;
          <Module>.SwapOut(this, bestDecoration1);
          num -= 3U;
        }
        else if (bestDecoration2 != null && (int) bestDecoration2.slots_required == 2 && <Module>.RoomFor2Slot(this.data))
        {
          flag = true;
          <Module>.SwapOut(this, bestDecoration2);
          num -= 2U;
        }
      }
      else if ((int) num == 2 && bestDecoration2 != null && ((int) bestDecoration2.slots_required == 2 && <Module>.RoomFor2Slot(this.data)))
      {
        flag = true;
        <Module>.SwapOut(this, bestDecoration2);
        num -= 2U;
      }
    }
    while (flag);
    goto label_1;
  }
}
