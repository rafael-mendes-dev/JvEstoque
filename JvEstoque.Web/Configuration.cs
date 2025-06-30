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
            Primary = "#3D63DD",
            Secondary = Colors.Blue.Darken3,
            Background = "#f1f5f9", // "#EEEEF0"
            AppbarBackground = Colors.Shades.White, // "#1F59A2"
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            TextSecondary = Colors.Shades.Black,
            DrawerText = Colors.Shades.Black,
            DrawerBackground = Colors.Shades.White
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#F27400",
            Secondary = Colors.Orange.Lighten3,
            AppbarText = Colors.Shades.White,
            DrawerText = Colors.Shades.White,
            TextPrimary = Colors.Shades.White,
            BackgroundGray = Colors.Shades.White,
        }
    };
}