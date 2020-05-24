using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using TEST.Shared;
using AutoMapper.QueryableExtensions;
using System.Runtime.InteropServices.WindowsRuntime;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TEST.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PropositionController : ControllerBase
    {
        private readonly MonConciergeContext _context;
        private readonly IQuery _query;

        public PropositionController(MonConciergeContext context, IQuery query)
        {
            _context = context;
            _query = query;

        }





        // GET: api/<ValuesController>
        [HttpGet]
        public List<Proposition> GetPropositions()
        {
            IQueryable<Proposition> v = _context.Proposition.Select(x => x);
            return v.ToList();
        }


        [HttpGet("{Id}")]
        public async Task<Response<Proposition>> GetProposition(int Id)
        {
            Proposition vote = await _context.Proposition.Include(x => x.Dossier).SingleAsync(x => x.Id == Id);
            var res = new Response<Proposition> { Data = vote };
            return res;
        }


        [HttpPost]
        //[Route("Create")]
        public async Task<ResponseSingle<int>> CreateProposition(Proposition proposition)
        {
            _context.Add(proposition);
            proposition.CreationDate = DateTime.Now;
            proposition.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _context.SaveChangesAsync();
            ResponseSingle<int> res = new ResponseSingle<int>();
            res.Data = 1;
            return res;
        }

        /// <summary>
        /// Cast a vote for a proposition
        /// </summary>
        /// <param name="userVote"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("Create")]
        public async Task<IActionResult> Cast(VoteCasted userVote)
        {
            var response = new ResponseSingle<int>();
            var currentUserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            userVote.VotedDate = DateTime.Now;
            userVote.UserId = currentUserID;

            _context.Add(userVote);
            await _context.SaveChangesAsync();
            return Ok(response);
        }

        [HttpGet("{propositionId}")]
        public async Task<ResponseList<VoteCasted>> GetVotesCasted(int propositionId)
        {
            ResponseList<VoteCasted> l = new ResponseList<VoteCasted>();
            l.Data = await _query.GetVotesCastedByPropositionId().Where(u => u.PropositionId == propositionId).ToListAsync();
            return l;
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
