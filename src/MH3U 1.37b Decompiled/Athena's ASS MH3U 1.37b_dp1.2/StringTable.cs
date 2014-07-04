// Decompiled with JetBrains decompiler
// Type: StringTable
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System.IO;
using System.Windows.Forms;

internal class StringTable
{
  public static string[] text;

  public static void LoadLanguage(string dir)
  {
    string str1 = "Data/Languages/" + dir + "/";
    StringTable.text = new string[108];
    StreamReader streamReader1 = new StreamReader((Stream) File.OpenRead(str1 + "strings.txt"));
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      int index = 0;
      while (index < StringTable.text.Length)
      {
        string str2 = streamReader2.ReadLine();
        if (str2 == null)
        {
          int num = (int) MessageBox.Show("Unexpected end of file: not enough lines of text");
          break;
        }
        else if (!(str2 == "") && (int) str2[0] != 35)
        {
          StringTable.text[index] = str2;
          ++index;
        }
      }
      streamReader2.Close();
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
    SkillTag.LoadLanguage(str1 + "tags.txt");
    Material.LoadLanguage(str1 + "components.txt");
    Armor.static_armor_map.Clear();
    Armor.LoadLanguage(str1 + "head.txt", Armor.ArmorType.HEAD);
    Armor.LoadLanguage(str1 + "body.txt", Armor.ArmorType.BODY);
    Armor.LoadLanguage(str1 + "arms.txt", Armor.ArmorType.ARMS);
    Armor.LoadLanguage(str1 + "waist.txt", Armor.ArmorType.WAIST);
    Armor.LoadLanguage(str1 + "legs.txt", Armor.ArmorType.LEGS);
    Decoration.LoadLanguage(str1 + "decorations.txt");
    Skill.LoadLanguage(str1 + "skills.txt");
    Skill.UpdateOrdering();
    Ability.UpdateOrdering();
  }

  public enum StringIndex
  {
    TranslationCredit,
    File,
    Options,
    Language,
    Help,
    LoadData,
    SaveData,
    SaveCharms,
    Exit,
    ClearSettings,
    AllowBadSkills,
    AllowPiercings,
    AllowEventArmor,
    AllowJapaneseOnlyDLC,
    AllowLowerTierArmor,
    SortSkillsAlphabetically,
    CheckForUpdates,
    About,
    MyCharms,
    Import,
    QuickSearch,
    AdvancedSearch,
    Cancel,
    Default,
    AdvancedNone,
    FilterByNone,
    SortByNone,
    Search,
    SelectNone,
    SelectBest,
    AddNew,
    Delete,
    DeleteAll,
    MoveUp,
    MoveDown,
    Trim,
    Close,
    FindNext,
    HR,
    VillageQuests,
    MaxWeaponSlots,
    Male,
    Female,
    FilterResultsByCharm,
    SortResultsBy,
    All,
    DragonRes,
    FireRes,
    IceRes,
    ThunderRes,
    WaterRes,
    BaseDefence,
    MaxDefence,
    Difficulty,
    Rarity,
    SortSlotsSpare,
    SortFamily,
    SortExtraSkills,
    Charms,
    UseNoCharms,
    UseOnlyMyCharms,
    UseOnlySlottedCharms,
    UseUpToOneSkillCharms,
    UseOnlyTwoSkillCharms,
    Skills,
    SkillFilters,
    Blademaster,
    Gunner,
    SelectArmor,
    ImportCharmsFromSaveData,
    Characters,
    DeleteExistingCharms,
    AreYouSure,
    Sort,
    Slots,
    NoneBrackets,
    SlotSpare,
    SlotsSpare,
    OrAnythingWithSingular,
    OrAnythingWithPlural,
    OrAnythingWithTorsoInc,
    SolutionsFound,
    ShowingFirstSolutionsOnly,
    MasaxMHCharmList,
    MasaxMHCharmListCorrupted,
    Version,
    Find,
    Cheater,
    Error,
    To,
    Defence,
    Table,
    Results,
    CharmTable,
    UnknownTable,
    AnyTable,
    DetectTable,
    FoundNCharmsUniqueToTable,
    FoundNCharmsOnTable,
    FindTableFailed,
    UnknownCharm,
    SorryAboutTheCharms,
    DeleteAllCharms,
    ASSSettings,
    MaxResults,
    PrintDecoNames,
    DefEleAbbrev,
    Jewel,
    NumStrings,
  }
}
