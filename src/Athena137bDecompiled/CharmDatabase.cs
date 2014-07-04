// Type: CharmDatabase
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using <CppImplementationDetails>;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

internal class CharmDatabase
{
  public static readonly List<Charm> mycharms = new List<Charm>();
  public static TableSlotDatum[,] min_max;
  public static Dictionary<uint, uint> hash_to_table;
  public static Dictionary<string, CharmLocationDatum> location_cache;
  public static byte[,] have_slots;

  static CharmDatabase()
  {
  }

  public static void LoadCustom()
  {
    CharmDatabase.mycharms.Clear();
    if (!File.Exists("Data/mycharms.txt"))
      return;
    StreamReader streamReader1 = new StreamReader("Data/mycharms.txt");
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      while (!streamReader2.EndOfStream)
      {
        string str = streamReader2.ReadLine();
        if (!(str == "") && (int) str[0] != 35)
        {
          List<string> vec = new List<string>();
          <Module>.Utility>SplitString(vec, str, ',');
          if (vec.Count != 5)
          {
            int num1 = (int) MessageBox.Show((IWin32Window) null, "Incorrect number of commas", str);
          }
          else
          {
            Charm charm = new Charm();
            try
            {
              charm.num_slots = Convert.ToUInt32(vec[0]);
              if (vec[1] != "")
              {
                if (vec[1] == "ElementAtk")
                  vec[1] = "Elemental";
                else if (vec[1] == "EdgeMaster")
                  vec[1] = "Edgemaster";
                Ability ability = Ability.FindAbility(vec[1]);
                if (ability == null)
                {
                  int num2 = (int) MessageBox.Show((IWin32Window) null, "\"" + vec[1] + "\": No such skill", str);
                  continue;
                }
                else
                  charm.abilities.Add(new AbilityPair(ability, Convert.ToInt32(vec[2])));
              }
              if (vec[3] != "")
              {
                Ability ability = Ability.FindAbility(vec[3]);
                if (ability == null)
                {
                  int num2 = (int) MessageBox.Show((IWin32Window) null, "\"" + vec[3] + "\": No such skill", str);
                  continue;
                }
                else
                  charm.abilities.Add(new AbilityPair(ability, Convert.ToInt32(vec[4])));
              }
            }
            catch (Exception ex)
            {
              int num2 = (int) MessageBox.Show((IWin32Window) null, "Could not read skill points:\n" + ex.ToString(), str);
              continue;
            }
            if (!CharmDatabase.CharmExists(charm) || !CharmDatabase.CharmIsLegal(charm))
            {
              flag = true;
              stringBuilder.AppendLine(charm.GetName());
            }
            else
            {
              charm.custom = true;
              CharmDatabase.mycharms.Add(charm);
            }
          }
        }
      }
      if (flag)
      {
        int num = (int) MessageBox.Show(StringTable.text[87] + "\n\n" + stringBuilder.ToString());
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void SaveCustom()
  {
    StreamWriter streamWriter1 = new StreamWriter("Data/mycharms.txt");
    StreamWriter streamWriter2;
    // ISSUE: fault handler
    try
    {
      streamWriter2 = streamWriter1;
      streamWriter2.WriteLine("#Format: NumSlots,Skill1,Points1,Skill2,Points2");
      List<Charm>.Enumerator enumerator = CharmDatabase.mycharms.GetEnumerator();
      while (enumerator.MoveNext())
      {
        Charm current = enumerator.Current;
        streamWriter2.Write(Convert.ToString(current.num_slots));
        for (int index = 0; index < 2; ++index)
        {
          if (index >= current.abilities.Count)
            goto label_8;
          streamWriter2.Write("," + current.abilities[index].ability.name + "," + Convert.ToString(current.abilities[index].amount));
          continue;
label_8:
          streamWriter2.Write(",,");
        }
        streamWriter2.WriteLine();
      }
    }
    __fault
    {
      streamWriter2.Dispose();
    }
    streamWriter2.Dispose();
  }

  public static int DetectCharmTable()
  {
    uint[] counts1 = new uint[17];
    uint num1 = 0U;
    List<Charm>.Enumerator enumerator1 = CharmDatabase.mycharms.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      Charm current = enumerator1.Current;
      if (!<Module>.IsAutoguardCharm(current))
      {
        uint hash = current.GetHash();
        if (CharmDatabase.hash_to_table.ContainsKey(hash))
        {
          int v = (int) CharmDatabase.hash_to_table[hash] & 268435455;
          int num2 = v;
          int num3 = -v;
          int num4 = num2 & num3;
          if (num2 == num4)
          {
            int positionFromRight = <Module>.GetBitPositionFromRight(v);
            counts1[positionFromRight - 1] = counts1[positionFromRight - 1] + 1U;
          }
        }
        else
          ++num1;
      }
    }
    int num5 = <Module>.CheckCount(counts1);
    int num6;
    if (num5 != -1)
    {
      string text = StringTable.text[97].Replace("%1", Convert.ToString(counts1[num5 - 1])).Replace("%2", Convert.ToString(num5)) + ".";
      if (num5 == 11 || num5 == 12 || (num5 == 15 || num5 == 16) || num5 == 17)
        text = text + " " + StringTable.text[101];
      int num2 = (int) MessageBox.Show(text);
      num6 = num5;
    }
    else
    {
      uint[] counts2 = new uint[17];
      List<Charm>.Enumerator enumerator2 = CharmDatabase.mycharms.GetEnumerator();
label_12:
      while (enumerator2.MoveNext())
      {
        uint hash = enumerator2.Current.GetHash();
        if (CharmDatabase.hash_to_table.ContainsKey(hash))
        {
          int num2 = (int) CharmDatabase.hash_to_table[hash];
          int num3 = 1;
          while (true)
          {
            if (num3 <= counts2.Length)
            {
              if ((num2 & 1 << num3) != 0)
                goto label_18;
label_15:
              ++num3;
              continue;
label_18:
              counts2[num3 - 1] = counts2[num3 - 1] + 1U;
              goto label_15;
            }
            else
              goto label_12;
          }
        }
      }
      int num4 = <Module>.CheckCount(counts2);
      if (num4 != -1)
      {
        string text = StringTable.text[98].Replace("%1", Convert.ToString(counts2[num4 - 1])).Replace("%2", Convert.ToString(num4)) + ".";
        if (num4 == 11 || num4 == 12 || (num4 == 15 || num4 == 16) || num4 == 17)
          text = text + " " + StringTable.text[101];
        int num2 = (int) MessageBox.Show(text);
        num6 = num4;
      }
      else
      {
        int num2 = (int) MessageBox.Show(StringTable.text[99]);
        num6 = -1;
      }
    }
    return num6;
  }

  public static void GenerateCharmTable()
  {
    List<Ability>.Enumerator enumerator1 = Ability.static_abilities.GetEnumerator();
    while (enumerator1.MoveNext())
      enumerator1.Current.relevant = true;
    uint num1 = (uint) Ability.static_abilities.Count;
    uint num2 = (uint) StaticData.slot_table.Length;
    int[] numArray = new int[17]
    {
      1,
      15,
      5,
      13,
      4,
      3,
      9,
      12,
      26,
      18,
      163,
      401,
      6,
      2,
      489,
      802,
      1203
    };
    CharmDatabase.min_max = new TableSlotDatum[19, (int) num2];
    CharmDatabase.hash_to_table = new Dictionary<uint, uint>();
    CharmDatabase.location_cache = new Dictionary<string, CharmLocationDatum>();
    CharmDatabase.have_slots = new byte[19, (int) num2];
    for (uint index1 = 0U; index1 < 19U; ++index1)
    {
      for (uint index2 = 0U; index2 < num2; ++index2)
      {
        TableSlotDatum tableSlotDatum = new TableSlotDatum();
        CharmDatabase.min_max[(int) index1, (int) index2] = tableSlotDatum;
        tableSlotDatum.max_single = new sbyte[(int) num1, 4];
        if (index1 <= 0U)
          continue;
        tableSlotDatum.two_skill_data = new List<Charm>[4][,];
        for (uint index3 = 0U; index3 < 4U; ++index3)
        {
          List<Charm>[][,] listArray1 = tableSlotDatum.two_skill_data;
          int index4 = (int) index3;
          int length = (int) num1;
          List<Charm>[,] listArray2 = new List<Charm>[length, length];
          listArray1[index4] = listArray2;
          for (uint index5 = 1U; index5 < num1; ++index5)
          {
            for (uint index6 = 0U; index6 < index5; ++index6)
            {
              tableSlotDatum.two_skill_data[(int) index3][(int) index5, (int) index6] = new List<Charm>();
              tableSlotDatum.two_skill_data[(int) index3][(int) index6, (int) index5] = tableSlotDatum.two_skill_data[(int) index3][(int) index5, (int) index6];
            }
          }
        }
      }
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      for (uint charm_type = 0U; charm_type < num2; ++charm_type)
      {
        int n = numArray[index];
        do
        {
          <Module>.GenerateCharm(charm_type, (uint) (index + 1), n);
          n = <Module>.rnd(n);
        }
        while (n != numArray[index]);
      }
    }
    List<Ability>.Enumerator enumerator2 = Ability.static_abilities.GetEnumerator();
    while (enumerator2.MoveNext())
      enumerator2.Current.relevant = false;
    for (uint index1 = 1U; index1 < 19U; ++index1)
    {
      for (uint index2 = 1U; index2 < num2; ++index2)
        CharmDatabase.have_slots[(int) index1, (int) index2] = (byte) ((uint) CharmDatabase.have_slots[(int) index1, (int) index2] | (uint) CharmDatabase.have_slots[(int) index1, (int) index2 - 1]);
    }
    for (uint index1 = 0U; index1 < num2; ++index1)
    {
      Array.Copy((Array) CharmDatabase.min_max[1, (int) index1].max_single, 0, (Array) CharmDatabase.min_max[0, (int) index1].max_single, 0, (int) num1 * 4);
      CharmDatabase.have_slots[0, (int) index1] = CharmDatabase.have_slots[1, (int) index1];
      for (uint index2 = 2U; index2 < 18U; ++index2)
      {
        if ((int) index2 == 12 || (int) index2 == 13 || ((int) index2 == 15 || (int) index2 == 16) || (int) index2 == 17)
          continue;
        for (uint index3 = 0U; index3 < num1; ++index3)
        {
          for (uint index4 = 0U; index4 < 4U; ++index4)
          {
            sbyte num3 = CharmDatabase.min_max[(int) index2, (int) index1].max_single[(int) index3, (int) index4];
            <Module>.MinOut<signed\u0020char>(CharmDatabase.min_max[0, (int) index1].max_single.Address((int) index3, (int) index4), (int) num3);
          }
        }
        CharmDatabase.have_slots[0, (int) index1] = (byte) ((uint) CharmDatabase.have_slots[0, (int) index1] & (uint) CharmDatabase.have_slots[(int) index2, (int) index1]);
      }
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool CharmExists(Charm charm)
  {
    return <Module>.IsAutoguardCharm(charm) || CharmDatabase.hash_to_table.ContainsKey(charm.GetHash());
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool CharmIsLegal(Charm charm)
  {
    bool flag;
    if (charm.num_slots >= 4U)
      flag = false;
    else if (charm.abilities.Count == 0 || <Module>.IsAutoguardCharm(charm))
      flag = true;
    else if (charm.abilities.Count == 1)
    {
      flag = charm.abilities[0].amount <= (int) CharmDatabase.min_max[18, 3].max_single[(int) charm.abilities[0].ability.static_index, (int) charm.num_slots];
    }
    else
    {
      if (charm.abilities.Count == 2)
      {
        List<Charm>.Enumerator enumerator = CharmDatabase.min_max[18, 3].two_skill_data[(int) charm.num_slots][(int) charm.abilities[0].ability.static_index, (int) charm.abilities[1].ability.static_index].GetEnumerator();
        while (enumerator.MoveNext())
        {
          Charm current = enumerator.Current;
          if ((int) current.abilities[0].ability.static_index == (int) charm.abilities[0].ability.static_index && (int) current.abilities[1].ability.static_index == (int) charm.abilities[1].ability.static_index && (current.abilities[0].amount >= charm.abilities[0].amount && current.abilities[1].amount >= charm.abilities[1].amount) || (int) current.abilities[0].ability.static_index == (int) charm.abilities[1].ability.static_index && (int) current.abilities[1].ability.static_index == (int) charm.abilities[0].ability.static_index && (current.abilities[0].amount >= charm.abilities[1].amount && current.abilities[1].amount >= charm.abilities[0].amount))
          {
            flag = true;
            goto label_12;
          }
        }
      }
      flag = false;
    }
label_12:
    return flag;
  }

  public static unsafe List<Charm> GetCharms(Query query, [MarshalAs(UnmanagedType.U1)] bool use_two_skill_charms)
  {
    List<Charm> list1 = new List<Charm>();
    $ArrayType$$$BY03_N arrayTypeBy03N;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(sbyte&) @arrayTypeBy03N = (sbyte) 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(sbyte&) ((IntPtr) &arrayTypeBy03N + 1) = (sbyte) 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(sbyte&) ((IntPtr) &arrayTypeBy03N + 2) = (sbyte) 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(sbyte&) ((IntPtr) &arrayTypeBy03N + 3) = (sbyte) 0;
    uint max_charm_type = <Module>.CalcMaxCharmType(query);
    <Module>.GetCharms(list1, query.charm_table, query.skills, max_charm_type);
    if (use_two_skill_charms && query.charm_table > 0U)
    {
      TableSlotDatum tableSlotDatum = CharmDatabase.min_max[(int) query.charm_table, (int) max_charm_type];
      for (int index1 = 1; index1 < query.skills.Count; ++index1)
      {
        for (int index2 = 0; index2 < index1; ++index2)
        {
          for (uint index3 = 0U; index3 < 4U; ++index3)
          {
            List<Charm> list2 = tableSlotDatum.two_skill_data[(int) index3][(int) query.skills[index1].ability.static_index, (int) query.skills[index2].ability.static_index];
            if (list2 == null)
              continue;
            list1.AddRange((IEnumerable<Charm>) list2);
          }
        }
      }
    }
    List<Charm>.Enumerator enumerator = list1.GetEnumerator();
    while (enumerator.MoveNext())
    {
      Charm current = enumerator.Current;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03N + (int) current.num_slots) = (sbyte) 1;
    }
    if (query.hr >= 4U || query.elder_star >= 8U)
      <Module>.AddSlotOnlyCharms(list1, query, 3U, max_charm_type, ($ArrayType$$$BY03$$CB_N*) &arrayTypeBy03N);
    else if (query.hr >= 3U || query.elder_star >= 6U)
      <Module>.AddSlotOnlyCharms(list1, query, 2U, max_charm_type, ($ArrayType$$$BY03$$CB_N*) &arrayTypeBy03N);
    else
      <Module>.AddSlotOnlyCharms(list1, query, 1U, max_charm_type, ($ArrayType$$$BY03$$CB_N*) &arrayTypeBy03N);
    return list1;
  }
}
