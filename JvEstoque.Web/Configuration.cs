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
            Primary = "#1F59A2",
            Secondary = Colors.Blue.Darken3,
            Background = "#f1f5f9", // "#EEEEF0"
            AppbarBackground = Colors.Shades.White, // "#1F59A2"
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            TextSecondary = "#222325",
            DrawerText = Colors.Shades.Black,
            DrawerBackground = Colors.Shades.White
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#F27400",
            Secondary = Colors.Orange.Lighten3,
            AppbarBackground = Colors.Shades.Black, // "#F27400"
            AppbarText = Colors.Shades.White,
            PrimaryContrastText = "#000000",
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.Shades.Black, //"#342E6E"
            Background = Colors.Shades.Black,
            TextPrimary = Colors.Shades.White,
            BackgroundGray = Colors.Shades.White
        }
    };
}