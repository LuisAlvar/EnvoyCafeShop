using Microsoft.AspNetCore.Mvc;

namespace coffeeapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private static readonly string[] CoffeeType = new[]
    {
        "Flat White", "Black", "Latte", "Americano", "Cappuccino"
    };

    private static readonly string[] Size = new[]
    {
        "Small", "Medium", "Large", "XLarge"
    };

    private readonly ILogger<CoffeeController> _logger;

    public CoffeeController(ILogger<CoffeeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCoffee")]
    public IEnumerable<Coffee> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Coffee
        {
            Price = Random.Shared.Next(1,6),
            Size = Size[Random.Shared.Next(Size.Length)],
            Description = CoffeeType[Random.Shared.Next(CoffeeType.Length)]
        })
        .ToArray();
    }
}
