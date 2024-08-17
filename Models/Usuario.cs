using System;
using System.Collections.Generic;

namespace WebUser.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int PhoneNumber { get; set; }
}
