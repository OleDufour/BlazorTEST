using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST.Server.Models;
using TEST.Shared;

namespace TEST.Server.Controllers
{
    // [AllowAnonymous]
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class DossierController : ControllerBase
    {
        private readonly MonConciergeContext _context;
        UserManager<ApplicationUser> _userManager;

        public DossierController(MonConciergeContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<Dossier>> GetDossiers()
        {
            List<Dossier> v = await _context.Dossier.ToListAsync();
            return v;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Dossier GetDossier(int id)
        {
            Dossier d = _context.Dossier.Include(b => b.Vote).Where(x => x.Id == id).Single();//.Select(x => x);
            return d;
        }


        [HttpPost]
        //[Route("Create")]
        public async Task<IActionResult> Add(Dossier dossier)
        {
            var response = new ResponseSingle<int>();
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            dossier.CreationDate = DateTime.Now;
            dossier.UserId = currentUserID;

            _context.Add(dossier);

            await _context.SaveChangesAsync();      
            return Ok(response);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
