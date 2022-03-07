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
using AutoMapper;
using FVCPD.Models;
using AutoMapper.QueryableExtensions;

namespace FVCPD.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ConjugatedVerbsController : ControllerBase {
		private readonly FVCPDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMapper _mapper_ignores_id;

		public ConjugatedVerbsController(FVCPDbContext context) {
			_context = context;
			_mapper = new MapperConfiguration(cfg => { cfg.CreateMap<ConjugatedVerb, ConjugatedVerbDTO>(); }).CreateMapper();
			_mapper_ignores_id = new MapperConfiguration(cfg => {
				cfg
					.CreateMap<ConjugatedVerbDTO, ConjugatedVerb>()
					.ForMember(dst => dst.Id, act => act.Ignore());
			}).CreateMapper();
		}

		// GET: api/ConjugatedVerbs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ConjugatedVerbDTO>>> GetConjugatedVerbs() {
			return await _context.ConjugatedVerbs.ProjectTo<ConjugatedVerbDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/ConjugatedVerbs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ConjugatedVerbDTO>> GetConjugatedVerb(int id) {
			var conjugatedVerb = await _context.ConjugatedVerbs.FindAsync(id);

			if (conjugatedVerb == null) {
				return NotFound();
			}

			return _mapper.Map<ConjugatedVerbDTO>(conjugatedVerb);
		}

		// PUT: api/ConjugatedVerbs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutConjugatedVerb(int id, ConjugatedVerbDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}

			var conjugatedVerb = await _context.ConjugatedVerbs.FindAsync(id);
			if (conjugatedVerb == null) { return NotFound(); }

			conjugatedVerb = _mapper_ignores_id.Map<ConjugatedVerb>(dto);

			_context.Entry(conjugatedVerb).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ConjugatedVerbExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/ConjugatedVerbs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ConjugatedVerbDTO>> PostConjugatedVerb(ConjugatedVerbDTO dto) {
			var conjugatedVerb = _mapper_ignores_id.Map<ConjugatedVerb>(dto);
			_context.ConjugatedVerbs.Add(conjugatedVerb);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetConjugatedVerb", new { id = conjugatedVerb.Id }, _mapper.Map<ConjugatedVerbDTO>(conjugatedVerb));
			} catch (Exception ex) {
				ModelState.AddModelError(ex.Source, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
				return UnprocessableEntity(ModelState);
			}
		}

		// DELETE: api/ConjugatedVerbs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteConjugatedVerb(int id) {
			var conjugatedVerb = await _context.ConjugatedVerbs.FindAsync(id);
			if (conjugatedVerb == null) {
				return NotFound();
			}

			_context.ConjugatedVerbs.Remove(conjugatedVerb);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ConjugatedVerbExists(int id) {
			return _context.ConjugatedVerbs.Any(e => e.Id == id);
		}
	}
}
