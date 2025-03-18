using EmployeeApi.Models;
using EmployeeApi.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ProductProcess process;

        public ProductController() { process = new ProductProcess(); }
        [HttpGet] public async Task<IActionResult> GetProduct() => SendResponse(await process.GetProduct(),false);
        [HttpPost] public async Task<IActionResult> SaveProduct(Product prod) => SendResponse(await process.SaveProduct(prod), true);
        [HttpDelete] public async Task<IActionResult> DeleteProduct(int id) => SendResponse(await process.DeleteProduct(id), true);
    }
}
                                    