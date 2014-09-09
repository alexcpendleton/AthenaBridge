using System.Collections.Generic;
using System.Windows.Forms;

namespace FormInstantiationImplementation
{
    public interface IMainFormControlMap {
        Button btnSearch { get; set; }
        NumericUpDown TanziaQuests { get; set; }
        NumericUpDown MogaQuests { get; set; }
        NumericUpDown MaxWeaponSlots { get; set; }
        ComboBox CharmTable { get; set; }
        RadioButton Male { get; set; }
        RadioButton Female { get; set; }
        ComboBox SortResultsBy { get; set; }
        ComboBox FilterResultsByCharm { get; set; }
        ComboBox Charms { get; set; }
        Button btnMyCharms { get; set; }
        Button btnImport { get; set; }
        List<ComboBox> BlademasterSkills { get; set; }
        List<ComboBox> BlademasterSkillFilters { get; set; }
        List<ComboBox> GunnerSkills { get; set; }
        List<ComboBox> GunnerSkillFilters { get; set; }
        Button btnAdvancedSearch { get; set; }
        Button btnCancel { get; set; }
        ProgressBar ProgressBar { get; set; }
        RichTextBox Results { get; set; }
        TabPage BlademasterTab { get; set; }
        TabPage GunnerTab { get; set; }
    }
}