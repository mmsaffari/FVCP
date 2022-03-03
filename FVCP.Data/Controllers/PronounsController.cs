#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FVCPD.Data;
using FVCPD.Data.Models;
using FVCPD.Models;

namespace FVCPD.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PronounsController : ControllerBase {
		private readonly FVCPDbContext _context;


		public PronounsController(FVCPDbContext context) {
			_context = context;
			//https://docs.automapper.org/en/latest/Getting-started.html
			var mapper = (new MapperConfiguration);
		}

		// GET: api/Pronouns
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PronounDTO>>> GetPronouns() {
			return (await _context.Pronouns.ToListAsync()).Select(item => ItemToDTO(item)).ToList();
		}

		// GET: api/Pronouns/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PronounDTO>> GetPronoun(int id) {
			var pronoun = await _context.Pronouns.FindAsync(id);

			if (pronoun == null) {
				return NotFound();
			}

			return ItemToDTO(pronoun);
		}

		// PUT: api/Pronouns/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPronoun(int id, PronounDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}
			
			var item = await _context.Pronouns.FindAsync(id);
			if (item == null) { return NotFound(); }

			item.Name = dto.Name;
			item.Enabled = dto.Enabled;
			item.Remarks = dto.Remarks;
			
			_context.Entry(item).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!PronounExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Pronouns
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<PronounDTO>> PostPronoun(PronounDTO dto) {
			var item = DTOToItem(dto);
			_context.Pronouns.Add(item);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetPronoun), new { id = item.Id }, ItemToDTO(item));
			} catch (Exception ex) {
				ModelState.AddModelError(ex.Source, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
				return UnprocessableEntity(ModelState);
			}
		}

		// DELETE: api/Pronouns/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePronoun(int id) {
			var pronoun = await _context.Pronouns.FindAsync(id);
			if (pronoun == null) {
				return NotFound();
			}

			_context.Pronouns.Remove(pronoun);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool PronounExists(int id) {
			return _context.Pronouns.Any(e => e.Id == id);
		}

	}
}
