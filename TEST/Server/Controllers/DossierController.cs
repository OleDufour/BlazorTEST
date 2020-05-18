using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST.Shared;

namespace TEST.Server.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class DossierController : ControllerBase
    {
        private readonly MonConciergeContext _context;

        public DossierController(MonConciergeContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public List<Dossier> GetDossiers()
        {
            List<Dossier> v = new List<Dossier>();
            v.Add(new Dossier { Title="test" });


//            IQueryable<Dossier> v = _context.Dossier;//.Select(x => x);
            return v.ToList();
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
            _context.Add(dossier);
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
