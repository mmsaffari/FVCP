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

namespace FVCPD.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PronounsController : ControllerBase {
		private readonly FVCPDbContext _context;

		public PronounsController(FVCPDbContext context) {
			_context = context;
		}

		// GET: api/Pronouns
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Pronoun>>> GetPronouns() {
			return await _context.Pronouns.ToListAsync();
		}

		// GET: api/Pronouns/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Pronoun>> GetPronoun(int id) {
			var pronoun = await _context.Pronouns.FindAsync(id);

			if (pronoun == null) {
				return NotFound();
			}

			return pronoun;
		}

		// PUT: api/Pronouns/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPronoun(int id, Pronoun pronoun) {
			if (id != pronoun.Id) {
				return BadRequest();
			}

			_context.Entry(pronoun).State = EntityState.Modified;

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
		public async Task<ActionResult<Pronoun>> PostPronoun(Pronoun pronoun) {
			_context.Pronouns.Add(pronoun);
			await _context.SaveChangesAsync();

			//return CreatedAtAction("GetPronoun", new { id = pronoun.Id }, pronoun);
			return CreatedAtAction(nameof(GetPronoun), new { id = pronoun.Id }, pronoun);
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
