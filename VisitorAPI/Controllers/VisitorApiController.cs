using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VisitorBusinessLogic.Exceptions;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDTOs;
using ValidationException = VisitorBusinessLogic.Exceptions.ValidationException;

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
                // Return all validation errors at once
                return BadRequest(new { Errors = ex.ValidationErrors });
            }
            catch (VisitorAlreadySignedInException ex)
            {
                return BadRequest(new { Message = ex.Message });
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
                // Pass the entire DTO
                await _visitorService.SignOutVisitorAsync(visitorDto);
                return Ok("Visitor signed out successfully.");
            }
            catch (VisitorNotSignedInException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
