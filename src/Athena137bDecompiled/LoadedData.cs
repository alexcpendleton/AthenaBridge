// Type: LoadedData
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.Collections.Generic;

internal class LoadedData
{
  public Skill FindSkill(uint skill)
  {
    return Skill.static_skills[(int) skill];
  }

  public void ImportTextFiles()
  {
    Armor.static_armor_map.Clear();
    Armor.static_armors = new List<Armor>[5];
    for (int index = 0; index < Armor.static_armors.Length; ++index)
      Armor.static_armors[index] = new List<Armor>();
    Material.LoadMaterials("Data/components.txt");
    SkillTag.Load("Data/tags.txt");
    Skill.Load("Data/skills.txt");
    Armor.Load("Data/head.txt", Armor.ArmorType.HEAD);
    Armor.Load("Data/body.txt", Armor.ArmorType.BODY);
    Armor.Load("Data/arms.txt", Armor.ArmorType.ARMS);
    Armor.Load("Data/waist.txt", Armor.ArmorType.WAIST);
    Armor.Load("Data/legs.txt", Armor.ArmorType.LEGS);
    Decoration.Load("Data/decorations.txt");
  }

  public void GetRelevantData(Query query)
  {
    List<Ability>.Enumerator enumerator1 = Ability.static_abilities.GetEnumerator();
    while (enumerator1.MoveNext())
      enumerator1.Current.relevant = false;
    List<Ability> list1 = new List<Ability>();
    List<Skill>.Enumerator enumerator2 = query.skills.GetEnumerator();
    while (enumerator2.MoveNext())
    {
      Skill current = enumerator2.Current;
      query.rel_abilities.Add(current.ability);
      current.ability.relevant = true;
    }
    query.rel_abilities.TrimExcess();
    <Module>.GetRelevantDecorations(query);
    query.rel_decorations.TrimExcess();
    for (int index = 0; index < 5; ++index)
    {
      Query query1 = query;
      List<Armor> rel_armor = query1.rel_armor[index];
      List<Armor> list2 = Armor.static_armors[index];
      List<Armor> inf_armor = query.inf_armor[index];
      List<Ability> danger_skills = list1;
      <Module>.GetRelevantArmors(query1, rel_armor, list2, inf_armor, danger_skills);
      query.rel_armor[index].TrimExcess();
    }
    query.rel_armor.TrimExcess();
  }
}
