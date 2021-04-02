using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.DataAccess;
using Kaizen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UploadandDowloadService.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //[Authorize(Roles = Role.Admin)]

        [HttpGet("getall")]
        public async Task<ActionResult<List<IdentityRole>>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpPost("createrole")]
        public async Task<ActionResult<UsersRole>> CreateRole(UsersRole role)
        {

            var exists = await _roleManager.RoleExistsAsync(role.Name);
            if (!exists)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
                if (result.Succeeded)
                {
                    return new UsersRole()
                    {
                        Name = role.Name
                    };
                }
            }
            return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            return Ok();
        }

        [HttpDelete("removefromRole")]
        public async Task<ActionResult> RemoveUserFromRole(RoleViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserEmail);
            var result = await _userManager.RemoveFromRoleAsync(user, model.Role);
            return Ok(result);
        }

    }
}