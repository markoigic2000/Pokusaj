using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredmetController : ControllerBase
    {

        
        public FakultetContext Context { get; set; }
        public PredmetController(FakultetContext context)
        {
            Context = context;
        }
        [Route("Preuzmi")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            return Ok(await Context.Predmeti.Select(p=> new{ p.ID, p.Naziv}).ToListAsync());
        }
    }
}