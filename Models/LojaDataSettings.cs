using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Models;

public class LojaDataSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string LojaCollectionName { get; set; } = null!;
}
