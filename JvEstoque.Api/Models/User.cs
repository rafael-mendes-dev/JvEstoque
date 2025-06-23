using Microsoft.AspNetCore.Identity;

namespace JvEstoque.Api.Models;

public class User : IdentityUser<int>
{
    public List<IdentityRole<int>>? Roles { get; set; }
}