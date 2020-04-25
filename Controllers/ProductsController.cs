using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepository _dutchRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository dutchRepository,
                                  ILogger<ProductsController> logger)
        {
            this._dutchRepository = dutchRepository;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
           var products = _dutchRepository.GetAllProducts();

           return Ok(products);
        }

    }
}
