using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZebraPillarEmerald.Client.Representations.Users;
using ZebraPillarEmerald.Core.Database;

namespace ZebraPillarEmerald.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/users")]
    public class UsersController : Controller
    {
        private readonly ZpeDbContext _dbContext;

        public UsersController(
            ZpeDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index([FromQuery] UsersIndexRequest request)
        {
            var users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}