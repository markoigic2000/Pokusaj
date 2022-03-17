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
    public class StudentController : ControllerBase
    {

        
        public FakultetContext Context { get; set; }
        public StudentController(FakultetContext context)
        {
            Context = context;
        }
        [Route("Preuzmi")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi([FromQuery] int[] rokIDs)
        {
            //return Ok(Context.Studenti);

            //Lazy loading
            //Eager loading
            //Explicit loading

            var studenti =  Context.Studenti
                        .Include(p=>p.StudentPredmet)
                        .ThenInclude(p=>p.IspitniRok)
                        .Include(p=>p.StudentPredmet)
                        .ThenInclude(p=>p.Predmet);

            var student = await studenti/*.Where(p => p.Indeks==17640)*/.ToListAsync();
            return Ok
            (
                student.Select(p=>
                new 
                {
                    Indeks = p.Indeks,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Predmeti = p.StudentPredmet.Where(q=>rokIDs.Contains(q.IspitniRok.ID)).Select(q => 
                    new
                    {
                        Predmet = q.Predmet.Naziv,
                        Rok = q.IspitniRok.Naziv,
                        Ocena = q.Ocena
                    })
                }).ToList()
            );

        }
        [Route("DodajStudenta")]
        [HttpPost]
        public async Task<ActionResult> DodajStudenta([FromBody] Student student)
        {
            if(student.Indeks<10000 || student.Indeks>20000)
            {
                return BadRequest("Pogresan indeks !");
            }
            if(string.IsNullOrWhiteSpace(student.Ime) || student.Ime.Length>50)
            {
                return BadRequest("Pogresno ime !");
            }
            if(string.IsNullOrWhiteSpace(student.Prezime) || student.Prezime.Length>50)
            {
                return BadRequest("Pogresno prezime !");
            }
            try
            {
                await Context.Studenti.AddAsync(student);
                await Context.SaveChangesAsync();
                return Ok($"Dodaj je student sa ID-em {student.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromeniStudenta/{indeks}/{ime}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniStudenta(int indeks,string ime,string prezime)
        {
            if(indeks<10000 || indeks>20000)
            {
                return BadRequest("Pogresan indeks !");
            }
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>50)
            {
                return BadRequest("Pogresno ime !");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>50)
            {
                return BadRequest("Pogresno prezime !");
            }
            try
            {
                var student = Context.Studenti.Where(p => p.Indeks==indeks).FirstOrDefault();
                if(student!= null)
                {
                    student.Ime = ime;
                    student.Prezime = prezime;
                    await Context.SaveChangesAsync();
                    return Ok($"Student sa ID-em {student.ID} je uspesno promenjen");
                }
                else
                {
                    return BadRequest("Student nije pronadjen !");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromeniFromBody")]
        [HttpPut]
        public async Task<ActionResult> PrmeniFromBody([FromBody] Student student)
        {
            if(student.ID<=0)
            {
                return BadRequest("Nevalidan ID");
            }
            if(student.Indeks<10000 || student.Indeks>20000)
            {
                return BadRequest("Nevalidan indeks");
            }
            if(string.IsNullOrWhiteSpace(student.Ime) || student.Ime.Length>50)
            {
                return BadRequest("Nevalidno ime");
            }
            if(string.IsNullOrWhiteSpace(student.Prezime) || student.Prezime.Length>50)
            {
                return BadRequest("Nevalidno prezime");
            }
            try
            {
                var StudentZaPromenu = await Context.Studenti.FindAsync(student.ID);
                StudentZaPromenu.Indeks = student.Indeks;
                StudentZaPromenu.Ime = student.Ime;
                StudentZaPromenu.Prezime = student.Prezime;

                //Context.Studenti.Update(student);


                await Context.SaveChangesAsync();
                return Ok($"Student sa ID-em {StudentZaPromenu.ID} je izmenjen");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("ObrisiStudenta/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiStudenta(int id)
        {
            if(id<=0)
            {
                return BadRequest("Nevalidan ID");
            }
            try
            {
                var studentZaBrisanje = await Context.Studenti.FindAsync(id);
                int indeks = studentZaBrisanje.Indeks;
                Context.Studenti.Remove(studentZaBrisanje);
                await Context.SaveChangesAsync();

                return BadRequest($"Obrisan student sa indeksom {indeks}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    




       
        
       
    }
}
