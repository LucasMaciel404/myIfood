using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Models;

public class UserDataSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UserCollectionName { get; set; } = null!;
}
