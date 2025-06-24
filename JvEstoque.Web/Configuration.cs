using MudBlazor;

namespace JvEstoque.Web;

public class Configuration
{
    public const string HttpClientName = "JvEstoque";

    public static string BackendUrl { get; set; } = null!;

    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Inter", "sans-serif"]
            }
        },
        PaletteLight = new PaletteLight
        {
            Primary = "#BD4400",
            Secondary = Colors.Orange.Darken3,
            Background = "#B2B3BD",
            AppbarBackground = "#BD4400",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            TextSecondary = "#222325",
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.Orange.Darken3
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#A25734",
            Secondary = Colors.Orange.Lighten3,
            AppbarBackground = "#A25734",
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = "#000000"
        }
    };
}