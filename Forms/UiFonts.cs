using System.Drawing;

namespace Vesy13.Forms;

internal static class UiFonts
{
    // ── General UI ────────────────────────────────────────────────────────────
    public static readonly Font Small         = new("Segoe UI",  8F);
    public static readonly Font Body          = new("Segoe UI", 12F);
    public static readonly Font BodyBold      = new("Segoe UI", 12F, FontStyle.Bold);
    public static readonly Font Medium        = new("Segoe UI", 10F);
    public static readonly Font MediumBold    = new("Segoe UI", 10F, FontStyle.Bold);
    public static readonly Font SubHeader     = new("Segoe UI", 11F);
    public static readonly Font SubHeaderBold = new("Segoe UI", 11F, FontStyle.Bold);
    public static readonly Font NavButton     = new("Segoe UI", 13F);
    public static readonly Font WeighButton   = new("Segoe UI", 14F, FontStyle.Bold);
    public static readonly Font UnitLabel     = new("Segoe UI", 20F);
    public static readonly Font Title         = new("Segoe UI", 22F, FontStyle.Bold);

    // ── Grid ──────────────────────────────────────────────────────────────────
    public static readonly Font GridBody   = new("Segoe UI",  9F);
    public static readonly Font GridHeader = new("Segoe UI", 12F, FontStyle.Bold);

    // ── Monospace ─────────────────────────────────────────────────────────────
    public static readonly Font MonoSmall      = new("Courier New",  9F);
    public static readonly Font Mono           = new("Courier New", 10F);
    public static readonly Font MonoBold       = new("Courier New", 10F, FontStyle.Bold);
    public static readonly Font MonoLiveAdc    = new("Courier New", 13F, FontStyle.Bold);
    public static readonly Font MonitorDisplay = new("Courier New", 48F, FontStyle.Bold);
    public static readonly Font Display        = new("Courier New", 60F, FontStyle.Bold);
}
