using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Models;

public class ProdutoDataSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ProdutoCollectionName { get; set; } = null!;
}
