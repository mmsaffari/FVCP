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
	public class VerbsController : ControllerBase {
		private readonly FVCPDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMapper _mapper_ignores_id;

		public VerbsController(FVCPDbContext context) {
			_context = context;
			_mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Verb, VerbDTO>(); }).CreateMapper();
			_mapper_ignores_id = new MapperConfiguration(cfg => {
				cfg
					.CreateMap<VerbDTO, Verb>()
					.ForMember(dst => dst.Id, act => act.Ignore());
			}).CreateMapper();
		}

		// GET: api/Verbs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<VerbDTO>>> GetVerbs() {
			return await _context.Verbs.ProjectTo<VerbDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/Verbs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<VerbDTO>> GetVerb(int id) {
			var verb = await _context.Verbs.FindAsync(id);

			if (verb == null) {
				return NotFound();
			}

			return _mapper.Map<VerbDTO>(verb);
		}

		// PUT: api/Verbs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutVerb(int id, VerbDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}
			
			var verb = await _context.Verbs.FindAsync(id);
			if (verb == null) { return NotFound(); }

			verb = _mapper_ignores_id.Map<Verb>(dto);

			_context.Entry(dto).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!VerbExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Verbs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<VerbDTO>> PostVerb(VerbDTO dto) {
			var verb = _mapper_ignores_id.Map<Verb>(dto);
			_context.Verbs.Add(verb);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetVerb", new { id = verb.Id }, _mapper.Map<VerbDTO>(verb));
			} catch (Exception ex) {
				ModelState.AddModelError(ex.Source, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
				return UnprocessableEntity(ModelState);
			}
		}

		// DELETE: api/Verbs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVerb(int id) {
			var verb = await _context.Verbs.FindAsync(id);
			if (verb == null) {
				return NotFound();
			}

			_context.Verbs.Remove(verb);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool VerbExists(int id) {
			return _context.Verbs.Any(e => e.Id == id);
		}
	}
}
