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
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace FVCPD.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PronounsController : ControllerBase {
		private readonly FVCPDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMapper _mapper_ignores_id;

		public PronounsController(FVCPDbContext context) {
			_context = context;
			_mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Pronoun, PronounDTO>(); }).CreateMapper();
			_mapper_ignores_id = new MapperConfiguration(cfg => {
				cfg
					.CreateMap<PronounDTO, Pronoun>()
					.ForMember(dest => dest.Id, act => act.Ignore());
			}).CreateMapper();
		}

		// GET: api/Pronouns
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PronounDTO>>> GetPronouns() {
			return await _context.Pronouns.ProjectTo<PronounDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/Pronouns/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PronounDTO>> GetPronoun(int id) {
			var pronoun = await _context.Pronouns.FindAsync(id);

			if (pronoun == null) {
				return NotFound();
			}

			return _mapper.Map<PronounDTO>(pronoun);
		}

		// PUT: api/Pronouns/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPronoun(int id, PronounDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}

			var pronoun = await _context.Pronouns.FindAsync(id);
			if (pronoun == null) { return NotFound(); }

			pronoun = _mapper_ignores_id.Map<Pronoun>(dto);

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
		public async Task<ActionResult<PronounDTO>> PostPronoun(PronounDTO dto) {
			var pronoun = _mapper_ignores_id.Map<Pronoun>(dto);
			_context.Pronouns.Add(pronoun);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetPronoun), new { id = pronoun.Id }, _mapper.Map<PronounDTO>(pronoun));
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
