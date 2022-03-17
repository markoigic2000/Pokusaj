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
    public class IspitController : ControllerBase
    {

        
        public FakultetContext Context { get; set; }
        public IspitController(FakultetContext context)
        {
            Context = context;
        }
        [Route("DodajIspitniRok/{rokID}/{ispitID}/{indeks}/{ocena}")]
        [HttpPost]
        public async Task<ActionResult> DodajRok(int rokID,int ispitID,int indeks,int ocena)
        {
            if(indeks<10000 || indeks>20000)
            {
                return BadRequest("Pogresan indeks");
            }
            if(ocena<5 || ocena>10)
            {
                return BadRequest("Pogresna ocena");
            }
            if(rokID<=0)
            {
                return BadRequest("Pogresan ID roka");
            }
            if(ispitID<=0)
            {
                return BadRequest("Pogresan ID ispita");
            }
           
            try
            {
                var student = Context.Studenti.Where(p=>p.Indeks==indeks).FirstOrDefault();
                var predmet = await Context.Predmeti.FindAsync(ispitID);    
                var rok = await Context.Rokovi.FindAsync(rokID);

                Spoj s = new Spoj 
                {
                    Student = student,
                    Predmet = predmet,
                    IspitniRok = rok,
                    Ocena = ocena
                };

                Context.StudentiPredmeti.Add(s);
                await Context.SaveChangesAsync();

                var podaci = await Context.StudentiPredmeti
                                .Include(p => p.Student)
                                .Include(p=>p.Predmet)
                                .Include(p=>p.IspitniRok)
                                .Where(p => p.Student.Indeks == indeks)
                                .Select( p=> 
                                new
                                {
                                    Indeks = indeks,
                                    Ime = p.Student.Ime,
                                    Prezime = p.Student.Prezime,
                                    Ispit = p.Predmet.Naziv,
                                    IspitniRok = p.IspitniRok.Naziv,
                                    Ocena = p.Ocena
                                }).ToListAsync();
                return Ok(podaci);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IspitniRokovi")]
        [HttpGet]
        public async Task<ActionResult> IspitniRokovi()
        {
            return Ok
            (
                await Context.Rokovi.Select(p=>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv
                }).ToListAsync()
            );
        }
        
    }
}