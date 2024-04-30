using Microsoft.AspNetCore.Identity;

namespace AppDev2.Models
{ 
public class UserModel1 : IdentityUser
     {
            public string Name { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
     }
}
