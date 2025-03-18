using EmployeeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public ActionResult SendResponse(ApiResponse response,bool showMessage = false)
        {
            if (showMessage) { response.Message ??= Convert.ToString(Enum.Parse<StatusFlags>(Convert.ToString(response.Status)));}
            return response.Status == (byte)StatusFlags.Failed ? BadRequest(response) : Ok(response);
        }
    }
}
