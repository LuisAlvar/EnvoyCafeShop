using Microsoft.AspNetCore.Mvc;

namespace teaapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TeaController : ControllerBase
{
    private static readonly string[] TeaType = new[]
    {
        "Green","Peppermint","Earl Grey","English Breakfast","Camomile"
    };

    private static readonly string[] Size = new[]
    {
        "Small","Medium","Large","XLarge"
    };

    private readonly ILogger<TeaController> _logger;

    public TeaController(ILogger<TeaController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTea")]
    public IEnumerable<Tea> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Tea
        {
            Price = Random.Shared.Next(1,6),
            Size = Size[Random.Shared.Next(Size.Length)],
            Description = TeaType[Random.Shared.Next(TeaType.Length)]
        })
        .ToArray();
    }
}
