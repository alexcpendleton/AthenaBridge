// Decompiled with JetBrains decompiler
// Type: Charm
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal class Charm
{
  public readonly List<AbilityPair> abilities = new List<AbilityPair>();
  public uint num_slots = num_slots;
  public bool custom = false;
  public bool optimal = false;
  public bool hacked;

  public Charm(uint num_slots)
  {
  }

  public Charm(Charm other)
  {
    List<AbilityPair>.Enumerator enumerator = other.abilities.GetEnumerator();
    while (enumerator.MoveNext())
    {
      AbilityPair current = enumerator.Current;
      this.abilities.Add(new AbilityPair(current.ability, current.amount));
    }
  }

  public Charm()
  {
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool StrictlyBetterThan(Charm other)
  {
    bool flag1;
    if (this.num_slots < other.num_slots)
    {
      flag1 = false;
    }
    else
    {
      bool flag2 = false;
      List<AbilityPair>.Enumerator enumerator1 = other.abilities.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        AbilityPair current1 = enumerator1.Current;
        if (current1.ability.relevant)
        {
          bool flag3 = false;
          List<AbilityPair>.Enumerator enumerator2 = this.abilities.GetEnumerator();
          while (enumerator2.MoveNext())
          {
            AbilityPair current2 = enumerator2.Current;
            if (current2.ability == current1.ability)
            {
              flag3 = true;
              if (current2.amount < current1.amount)
              {
                flag1 = false;
                goto label_28;
              }
              else if (current2.amount > current1.amount)
              {
                flag2 = true;
                break;
              }
              else
                break;
            }
          }
          if (!flag3)
          {
            if (current1.amount > 0)
            {
              flag1 = false;
              goto label_28;
            }
            else
              flag2 = true;
          }
        }
      }
      if (flag2)
      {
        flag1 = true;
      }
      else
      {
        List<AbilityPair>.Enumerator enumerator2 = this.abilities.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          AbilityPair current = enumerator2.Current;
          if (current.ability.relevant)
          {
            bool flag3 = false;
            List<AbilityPair>.Enumerator enumerator3 = other.abilities.GetEnumerator();
            while (enumerator3.MoveNext())
            {
              if (enumerator3.Current.ability == current.ability)
              {
                flag3 = true;
                break;
              }
            }
            if (!flag3 && current.amount > 0)
            {
              flag1 = true;
              goto label_28;
            }
          }
        }
        flag1 = this.num_slots > other.num_slots;
      }
    }
label_28:
    return flag1;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool BasicallyTheSameAs(Charm other)
  {
    bool flag;
    if ((int) this.num_slots != (int) other.num_slots)
    {
      flag = false;
    }
    else
    {
      List<AbilityPair> l1 = new List<AbilityPair>();
      List<AbilityPair> l2 = new List<AbilityPair>();
      List<AbilityPair>.Enumerator enumerator1 = this.abilities.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        AbilityPair current = enumerator1.Current;
        if (current.ability.relevant)
          l1.Add(current);
      }
      List<AbilityPair>.Enumerator enumerator2 = other.abilities.GetEnumerator();
      while (enumerator2.MoveNext())
      {
        AbilityPair current = enumerator2.Current;
        if (current.ability.relevant)
          l2.Add(current);
      }
      flag = l1.Count == l2.Count && \u003CModule\u003E.EqualAbilities(l1, l2);
    }
    return flag;
  }

  [SpecialName]
  [return: MarshalAs(UnmanagedType.U1)]
  public bool op_Equality(Charm other)
  {
    return (int) this.num_slots == (int) other.num_slots && this.abilities.Count == other.abilities.Count && \u003CModule\u003E.EqualAbilities(this.abilities, other.abilities);
  }

  public string GetName()
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.abilities.Count > 0)
    {
      stringBuilder.Append("+").Append(this.abilities[0].amount).Append(" ").Append(this.abilities[0].ability.name);
      if (this.abilities.Count > 1)
      {
        string str = this.abilities[1].amount <= 0 ? ", " : ", +";
        stringBuilder.Append(str).Append(this.abilities[1].amount).Append(" ").Append(this.abilities[1].ability.name).Append(" ");
      }
      else
        stringBuilder.Append(" ");
    }
    for (uint index = 0U; index < this.num_slots; ++index)
      stringBuilder.Append("O");
    for (uint index = this.num_slots; index < 3U; ++index)
      stringBuilder.Append("-");
    return stringBuilder.ToString();
  }

  public uint GetHash()
  {
    return this.abilities.Count != 0 ? (this.abilities.Count != 1 ? Charm.HashFunction(this.num_slots, this.abilities[0].ability, this.abilities[0].amount, this.abilities[1].ability, this.abilities[1].amount) : Charm.HashFunction(this.num_slots, this.abilities[0].ability, this.abilities[0].amount, (Ability) null, 0)) : Charm.HashFunction(this.num_slots, (Ability) null, 0, (Ability) null, 0);
  }

  public static uint HashFunction(uint num_slots, Ability a1, int p1, Ability a2, int p2)
  {
    uint num1 = a1 == null ? (uint) Ability.static_abilities.Count : a1.static_index;
    uint num2 = a2 == null ? (uint) Ability.static_abilities.Count : a2.static_index;
    int num3 = a1 == null ? 0 : p1 << 2;
    int num4 = a2 == null ? 12 : p2 + 12;
    return (uint) ((int) num_slots + num3 + (num4 << 6) + ((int) num1 << 11) + ((int) num2 << 18));
  }

  public static void AddToOptimalList(List<Charm> lst, Charm new_charm)
  {
    for (int index1 = 0; index1 < lst.Count; ++index1)
    {
      Charm other = lst[index1];
      if (!new_charm.StrictlyBetterThan(other))
        goto label_5;
      int index2 = index1;
      lst.RemoveAt(index2);
      --index1;
      continue;
label_5:
      if (other.StrictlyBetterThan(new_charm) || other.BasicallyTheSameAs(new_charm))
        return;
    }
    lst.Add(new_charm);
  }
}
