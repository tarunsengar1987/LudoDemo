using System.ComponentModel;

namespace LudoWebAPI.Models
{
    public enum ItemType
    {
        [Description("Dice")]
        Dice,

        [Description("Board")]
        Board,

        [Description("Costume")]
        Costume,
    }
}
