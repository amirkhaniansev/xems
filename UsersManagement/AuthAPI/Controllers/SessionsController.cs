using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AccessCore.Repository;
using AuthAPI.Globals;
using AuthAPI.Models;

namespace AuthAPI.Controllers
{
    /// <summary>
    /// Controller for sessions
    /// </summary>
    [Route("api/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private readonly DataManager _dm;

        /// <summary>
        /// Creates new instance of <see cref="SessionsController"/>
        /// </summary>
        /// <param name="dm">Data manager</param>
        public SessionsController(DataManager dm)
        {
            this._dm = dm;
        }

        /// <summary>
        /// Posts new session
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Session session)
        {
            if (session == null)
                return this.BadRequest(Constants.SessionNullError);

            if (session.UserId != Provider.GetAuthenticatedUserId(this.User.Identity))
                return this.Unauthorized();

            var result = (int)await this._dm.OperateAsync<Session, object>(Constants.CreateSession, session);

            if (result == 1)
                return this.Ok(Constants.SessionCreationSuccess);

            return this.BadRequest(Constants.SessionError);
        }

        /// <summary>
        /// Ends session
        /// </summary>
        /// <param name="sessionEndInfo">Session End Information</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]SessionEndInfo sessionEndInfo)
        {
            if (sessionEndInfo == null)
                return this.BadRequest(Constants.SessionEndInfoNullError);

            if (sessionEndInfo.UserId != Provider.GetAuthenticatedUserId(this.User.Identity))
                return this.Unauthorized();

            var result = (int) await this._dm.OperateAsync<int, object>(Constants.EndSession, sessionEndInfo.SessionId);

            if (result == 1)
                return this.Ok(Constants.SessionEndSuccess);

            return this.BadRequest(Constants.SessionEndError);
        }
    }
}