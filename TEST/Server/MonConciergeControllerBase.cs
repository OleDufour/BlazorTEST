using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TEST.Server
{
    [Authorize]
    public class MonConciergeControllerBase : ControllerBase
    {
        public ClaimsPrincipal CurrentUser { get; private set; }
        public string CurrentUserID { get; private set; }
        public MonConciergeControllerBase()
        {
            //CurrentUser = this.User;
            //CurrentUserID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
