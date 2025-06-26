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
            Background = "#EEEEF0",
            AppbarBackground = "#1F59A2",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            TextSecondary = "#222325",
            DrawerText = Colors.Shades.Black,
            DrawerBackground = "#1F59A2"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#F27400",
            Secondary = Colors.Orange.Lighten3,
            AppbarBackground = "#F27400",
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = "#000000",
            Background = "#19191B",
            DrawerText = Colors.Shades.Black,
            DrawerBackground = "#F27400"
        }
    };
}