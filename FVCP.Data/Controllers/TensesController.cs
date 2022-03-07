#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FVCPD.Data;
using FVCPD.Models;
using AutoMapper;
using FVCPD.Data.Models;
using AutoMapper.QueryableExtensions;

namespace FVCPD.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TensesController : ControllerBase {
		private readonly FVCPDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMapper _mapper_ignores_id;

		public TensesController(FVCPDbContext context) {
			_context = context;
			_mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Tense, TenseDTO>(); }).CreateMapper();
			_mapper_ignores_id = new MapperConfiguration(cfg => {
				cfg
					.CreateMap<TenseDTO, Tense>()
					.ForMember(dst => dst.Id, act => act.Ignore());
			}).CreateMapper();
		}

		// GET: api/Tenses
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TenseDTO>>> GetTenseDTO() {
			return await _context.Tenses.ProjectTo<TenseDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/Tenses/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TenseDTO>> GetTense(int id) {
			var tense = await _context.Tenses.FindAsync(id);

			if (tense == null) {
				return NotFound();
			}

			return _mapper.Map<TenseDTO>(tense);
		}

		// PUT: api/Tenses/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTense(int id, TenseDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}

			var tense = await _context.Tenses.FindAsync(id);
			if (tense == null) { return NotFound(); }

			tense = _mapper_ignores_id.Map<Tense>(dto);
			_context.Entry(tense).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!TenseExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Tenses
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<TenseDTO>> PostTense(TenseDTO dto) {
			var tense = _mapper_ignores_id.Map<Tense>(dto);
			_context.Tenses.Add(tense);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetTenseDTO", new { id = tense.Id }, _mapper.Map<TenseDTO>(tense));
			} catch (Exception ex) {
				ModelState.AddModelError(ex.Source, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
				return UnprocessableEntity(ModelState);
			}
		}

		// DELETE: api/Tenses/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTense(int id) {
			var tenseDTO = await _context.Tenses.FindAsync(id);
			if (tenseDTO == null) {
				return NotFound();
			}

			_context.Tenses.Remove(tenseDTO);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TenseExists(int id) {
			return _context.Tenses.Any(e => e.Id == id);
		}
	}
}
