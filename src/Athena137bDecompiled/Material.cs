// Type: Material
// Assembly: MH3G ASS, Version=1.0.5292.33333, Culture=neutral, PublicKeyToken=null
// MVID: 1D205E18-91F0-4956-ABD8-C3ABCEEBBBAB
// Assembly location: C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.37b.exe

using System;
using System.Collections.Generic;
using System.IO;

internal class Material
{
  public static readonly List<Material> static_materials = new List<Material>();
  public static readonly Dictionary<string, Material> static_material_map = new Dictionary<string, Material>();
  public string name;
  public bool event_only;
  public bool jap_only;
  public uint difficulty;
  public uint ping_index;

  static Material()
  {
  }

  public static Material FindMaterial(string name)
  {
    return !Material.static_material_map.ContainsKey(name) ? (Material) null : Material.static_material_map[name];
  }

  public static void LoadMaterials(string filename)
  {
    Material.static_materials.Clear();
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      while (!streamReader2.EndOfStream)
      {
        string str = streamReader2.ReadLine();
        if (!(str == ""))
        {
          Material material = new Material();
          List<string> vec = new List<string>();
          <Module>.Utility>SplitString(vec, str, ',');
          material.name = vec[1];
          material.ping_index = Convert.ToUInt32(vec[0]);
          material.event_only = false;
          material.jap_only = false;
          material.difficulty = 0U;
          if (vec.Count >= 3)
          {
            if (vec[2] == "Event")
              material.event_only = true;
            else if (vec[2] == "jEvent")
            {
              material.event_only = true;
              material.jap_only = true;
            }
            else if (vec[2] != "")
              material.difficulty = Convert.ToUInt32(vec[2]);
          }
          Material.static_materials.Add(material);
        }
      }
      streamReader2.Close();
      Material.static_material_map.Clear();
      List<Material>.Enumerator enumerator = Material.static_materials.GetEnumerator();
      while (enumerator.MoveNext())
      {
        Material current = enumerator.Current;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Material& local = @current;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        Material.static_material_map.Add((^local).name, ^local);
      }
    }
    __fault
    {
      streamReader2.Dispose();
    }
    streamReader2.Dispose();
  }

  public static void LoadLanguage(string filename)
  {
    Material.static_material_map.Clear();
    StreamReader streamReader1 = new StreamReader(filename);
    StreamReader streamReader2;
    // ISSUE: fault handler
    try
    {
      streamReader2 = streamReader1;
      int index = 0;
      while (index < Material.static_materials.Count)
      {
        string key = streamReader2.ReadLine();
        if (!(key == "") && (int) key[0] != 35)
        {
          Material.static_materials[index].name = key;
          Material.static_material_map.Add(key, Material.static_materials[index]);
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
}
