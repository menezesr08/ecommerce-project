using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // The apicontroller attribute validates errors for us
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}