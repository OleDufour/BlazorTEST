using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TEST.Server.Models;
using TEST.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TEST.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VoteController : ControllerBase
    {
        private readonly MonConciergeContext _context;

        public VoteController(MonConciergeContext context)
        {
            _context = context;

        }

        // GET: api/<ValuesController>
        [HttpGet]
        public List<Vote> GetPropositions()
        {
            IQueryable<Vote> v = _context.Vote.Select(x => x);
            return v.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        //[Route("Create")]
        public async Task<IActionResult> Add(Vote proposition)
        {
            var response = new ResponseSingle<int>();
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            _context.Add(proposition);
            proposition.CreationDate = DateTime.Now;
            proposition.UserId = currentUserID;
            await _context.SaveChangesAsync();
            return Ok(response);
        }


        [HttpPost]
        //[Route("Create")]
        public async Task<IActionResult> Cast(Vote vote)
        {
            var response = new ResponseSingle<int>();
            _context.Add(vote);
            await _context.SaveChangesAsync();
            //      return NoContent();
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
