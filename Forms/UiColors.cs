using System.Drawing;

namespace Vesy13.Forms;

internal static class UiColors
{
    public static readonly Color AppBackground      = Color.FromArgb(240, 242, 245);
    public static readonly Color Surface            = Color.White;
    public static readonly Color DisplayBackground  = Color.FromArgb(5, 8, 12);
    public static readonly Color StatusBar          = Color.FromArgb(18, 32, 65);

    public static readonly Color TextPrimary        = Color.FromArgb(32, 42, 58);
    public static readonly Color TextMuted          = Color.FromArgb(80, 90, 106);
    public static readonly Color TextOnDark         = Color.White;
    public static readonly Color TextOnDarkMuted    = Color.FromArgb(200, 210, 225);

    public static readonly Color GridHeaderBack     = Color.FromArgb(230, 236, 245);
    public static readonly Color GridHeaderText     = Color.FromArgb(35, 45, 65);
    public static readonly Color GridAlternateRow   = Color.FromArgb(244, 247, 252);

    public static readonly Color PrimaryAction      = Color.FromArgb(22, 138, 58);
    public static readonly Color PendingAction      = Color.FromArgb(196, 122, 0);
    public static readonly Color DangerAction       = Color.FromArgb(138, 58, 26);
    public static readonly Color NavigationAction   = Color.FromArgb(40, 70, 130);
    public static readonly Color Info               = Color.FromArgb(31, 95, 168);
    public static readonly Color Error              = Color.FromArgb(198, 40, 40);
    public static readonly Color Disconnected       = Color.FromArgb(122, 127, 136);
}
