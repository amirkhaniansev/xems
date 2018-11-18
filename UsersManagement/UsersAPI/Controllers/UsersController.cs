using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XemsMailer.Mailers;

namespace UsersAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    [Produces("application/json")]
    public class UsersController : UsersApiBaseController
    {
        private readonly MessageMailer _mailer;

        public UsersController()
        {
            this._mailer = Globals.Mailer;
        }

        /*[HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

        }

        private async Task<IActionResult> Get<TParameter, TPublic, TPrivate>(string opName, TParameter parameter)
        {

        }*/
    }
}