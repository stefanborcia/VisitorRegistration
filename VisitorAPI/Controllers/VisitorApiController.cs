using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorApiController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorApiController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInVisitorDTO visitorDto)
        {
            try
            {
                await _visitorService.RegisterVisitorAsync(visitorDto);
                return Ok("Visitor signed in successfully.");
            }
            catch (VisitorAlreadySignedInException ex)
            {
                return BadRequest($"Error: {ex.Message}");  // This will display the custom error message
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");  // Generic error handling
            }
        }

    }
}
