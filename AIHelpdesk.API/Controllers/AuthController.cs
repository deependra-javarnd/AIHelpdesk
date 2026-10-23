using AIHelpdesk.API.DTOs;
using AIHelpdesk.API.Interfaces;
using AIHelpdesk.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AIHelpdesk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);

        if (token == null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        return Ok(new
        {
            token
        });
    }
}

//                  POST /api/auth/login
//                           │
//                           ▼
//                    AuthController
//                           │
//                           ▼
//                      IAuthService
//                           │
//                           ▼
//                      AuthService
//                           │
//                    ┌──────┴──────┐
//                    ▼             ▼
//               SQL Users       BCrypt
//                    │             │
//                    └──────┬──────┘
//                           ▼
//                    Generate JWT
//                           │
//                           ▼
//                    Role Claim
//                 Admin / Agent / User
//                           │
//                           ▼
//                      JWT Token