﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoyaltyConsole.MVC.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected void SetFullName()
        {
            string token = HttpContext.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                var secretKey = "sdfgdf-463dgdfsd j-fdvnji2387nLoyalty";
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                try
                {
                    var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    }, out SecurityToken validatedToken);

                    string fullname = claimsPrincipal?.Identity?.Name;
                    if (!string.IsNullOrEmpty(fullname))
                    {
                        ViewBag.FullName = fullname;
                    }

                    string userId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        ViewBag.Id = userId;
                    }

                    var roleClaim = claimsPrincipal?.FindFirst(ClaimTypes.Role);
                    if (roleClaim != null)
                    {
                        ViewBag.Role = roleClaim.Value;
                    }
                    else
                    {
                        var customRoleClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "customRole");
                        if (customRoleClaim != null)
                        {
                            ViewBag.Role = customRoleClaim.Value;
                        }
                    }
                }
                catch (SecurityTokenException)
                {
                }
            }

        }
    }
}
