using CubeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CubeService.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected SharedError Error { get; set; }
        public BaseController(SharedError error)
        {
            Error = error;
        }
    }
}