using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace UnifyPermission.Handler
{
    public class LfgAuthenticationHandler : AuthenticationHandler<LfgAuthenticationOption>
    {
        public LfgAuthenticationHandler(IOptionsMonitor<LfgAuthenticationOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(null, null, "lfg")));
        }
    }
}
