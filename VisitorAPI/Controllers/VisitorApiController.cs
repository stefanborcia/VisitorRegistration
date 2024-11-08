using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Services;
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
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
