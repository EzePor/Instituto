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
    public class ApiDetallesInscripcionesExamenController : ControllerBase
    {
        private readonly InstitutoContext _context;

        public ApiDetallesInscripcionesExamenController(InstitutoContext context)
        {
            _context = context;
        }

        // GET: api/ApiDetallesInscripcionesExamen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleInscripcionExamen>>> Getdetallesinscripcionesexamenes()
        {
            return await _context.detallesinscripcionesexamenes.Include(d=>d.Materia).ToListAsync();
        }

        // GET: api/ApiDetallesInscripcionesExamen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleInscripcionExamen>> GetDetalleInscripcionExamen(int id)
        {
            var detalleInscripcionExamen = await _context.detallesinscripcionesexamenes.FindAsync(id);

            if (detalleInscripcionExamen == null)
            {
                return NotFound();
            }

            return detalleInscripcionExamen;
        }

        // PUT: api/ApiDetallesInscripcionesExamen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleInscripcionExamen(int id, DetalleInscripcionExamen detalleInscripcionExamen)
        {
            if (id != detalleInscripcionExamen.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleInscripcionExamen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleInscripcionExamenExists(id))
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

        // POST: api/ApiDetallesInscripcionesExamen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleInscripcionExamen>> PostDetalleInscripcionExamen(DetalleInscripcionExamen detalleInscripcionExamen)
        {
            _context.detallesinscripcionesexamenes.Add(detalleInscripcionExamen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleInscripcionExamen", new { id = detalleInscripcionExamen.Id }, detalleInscripcionExamen);
        }

        // DELETE: api/ApiDetallesInscripcionesExamen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleInscripcionExamen(int id)
        {
            var detalleInscripcionExamen = await _context.detallesinscripcionesexamenes.FindAsync(id);
            if (detalleInscripcionExamen == null)
            {
                return NotFound();
            }

            _context.detallesinscripcionesexamenes.Remove(detalleInscripcionExamen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleInscripcionExamenExists(int id)
        {
            return _context.detallesinscripcionesexamenes.Any(e => e.Id == id);
        }
    }
}
