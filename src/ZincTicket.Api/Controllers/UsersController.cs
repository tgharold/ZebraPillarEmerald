using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZincTicket.Client.Representations.Users;

namespace ZincTicket.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/users")]
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index([FromQuery] UsersIndexRequest request)
        {
            throw new NotImplementedException();            
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}