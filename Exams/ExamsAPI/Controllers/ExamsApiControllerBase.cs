using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamsAPI.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XemsLogger;
using XemsMailer.Mailers;

namespace ExamsAPI.Controllers
{
    [ApiController]
    public class ExamsApiControllerBase : ControllerBase
    {
        protected readonly IXemsLogger _logger;

        protected readonly MessageMailer _mailer;

        public ExamsApiControllerBase()
        {
            this._logger = Globals.Logger;
            this._mailer = Globals.Mailer;
        }
    }
}