using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs.VisitorDTO;

namespace VisitorAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VisitorApiController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorApiController(IVisitorService visitorService) => _visitorService = visitorService;
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInVisitorDTO visitorDto)
        {
            try
            {
                var visitor = await _visitorService.RegisterVisitorAsync(visitorDto);
                return Ok(new { Message = "Visitor signed in successfully.", Visitor = visitor });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Errors = ex.ValidationErrors });
            }
            catch (VisitorAlreadySignedInException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut([FromBody] SignOutVisitorDTO visitorDto)
        {
            try
            {
                await _visitorService.SignOutVisitorAsync(visitorDto);
                return Ok("Visitor signed out successfully.");
            }
            catch (VisitorNotSignedInException ex)
            {
                return BadRequest(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("monitoring")]
        public async Task<IActionResult> GetVisitorMonitoring()
        {
            var visitors = await _visitorService.GetVisitorMonitoringAsync();
            if (visitors == null)
                return NotFound(new { message = "Visitors not found." });

            return Ok(visitors);
        }

        [HttpGet("registration-search/{search}")]
        public async Task<IActionResult> GetVisitorRegistrationSearch(string search)
        {
            var visitors = await _visitorService.GetVisitorRegistrationSearchAsync(search);
            if (visitors == null)
                return NotFound(new { message = "Visitors not found." });

            return Ok(visitors);
        }
    }
}
