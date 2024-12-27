using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Models;

public class PedidoDataSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PedidoCollectionName { get; set; } = null!;
}
