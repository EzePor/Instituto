using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstitutoBack.DataContext;
using InstitutoServices.Models.MesasExamenes;

namespace InstitutoBack.Controllers.MesasExamenes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiInscripcionesExamenController : ControllerBase
    {
        private readonly InstitutoContext _context;

        public ApiInscripcionesExamenController(InstitutoContext context)
        {
            _context = context;
        }

        // GET: api/ApiInscripcionesExamen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionExamen>>> Getinscripcionesexamenes()
        {
            return await _context.inscripcionesexamenes
                .Include(i => i.Alumno)
                .Include(i => i.Carrera)
                .Include(i => i.TurnoExamen).ToListAsync();
        }

        // GET: api/ApiInscripcionesExamen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionExamen>> GetInscripcionExamen(int id)
        {
            var inscripcionExamen = await _context.inscripcionesexamenes.FindAsync(id);

            if (inscripcionExamen == null)
            {
                return NotFound();
            }

            return inscripcionExamen;
        }

        // PUT: api/ApiInscripcionesExamen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcionExamen(int id, InscripcionExamen inscripcionExamen)
        {
            if (id != inscripcionExamen.Id)
            {
                return BadRequest();
            }

            _context.Entry(inscripcionExamen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExamenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiInscripcionesExamen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InscripcionExamen>> PostInscripcionExamen(InscripcionExamen inscripcionExamen)
        {
            _context.inscripcionesexamenes.Add(inscripcionExamen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscripcionExamen", new { id = inscripcionExamen.Id }, inscripcionExamen);
        }

        // DELETE: api/ApiInscripcionesExamen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcionExamen(int id)
        {
            var inscripcionExamen = await _context.inscripcionesexamenes.FindAsync(id);
            if (inscripcionExamen == null)
            {
                return NotFound();
            }

            _context.inscripcionesexamenes.Remove(inscripcionExamen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscripcionExamenExists(int id)
        {
            return _context.inscripcionesexamenes.Any(e => e.Id == id);
        }
    }
}
