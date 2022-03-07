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
	public class MoodsController : ControllerBase {
		private readonly FVCPDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMapper _mapper_ignores_id;

		public MoodsController(FVCPDbContext context) {
			_context = context;
			_mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Mood, MoodDTO>(); }).CreateMapper();
			_mapper_ignores_id = new MapperConfiguration(cfg => {
				cfg
					.CreateMap<MoodDTO, Mood>()
					.ForMember(dest => dest.Id, act => act.Ignore());
			}).CreateMapper();
		}

		// GET: api/Moods
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MoodDTO>>> GetMoods() {
			return await _context.Moods.ProjectTo<MoodDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		// GET: api/Moods/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MoodDTO>> GetMood(int id) {
			var mood = await _context.Moods.FindAsync(id);

			if (mood == null) {
				return NotFound();
			}

			return _mapper.Map<MoodDTO>(mood);
		}

		// PUT: api/Moods/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMood(int id, MoodDTO dto) {
			if (id != dto.Id) {
				return BadRequest();
			}
			var mood = await _context.Moods.FindAsync(id);
			if (mood == null) { return NotFound(); }
			
			mood = _mapper_ignores_id.Map<Mood>(dto);

			_context.Entry(mood).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!MoodExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Moods
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<MoodDTO>> PostMood(MoodDTO dto) {
			var mood = _mapper_ignores_id.Map<Mood>(dto);
			_context.Moods.Add(mood);
			try {
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetMood", new { id = mood.Id }, _mapper.Map<MoodDTO>(mood));
			} catch (Exception ex) {
				ModelState.AddModelError(ex.Source, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
				return UnprocessableEntity(ModelState);
			}
		}

		// DELETE: api/Moods/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMood(int id) {
			var mood = await _context.Moods.FindAsync(id);
			if (mood == null) {
				return NotFound();
			}

			_context.Moods.Remove(mood);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool MoodExists(int id) {
			return _context.Moods.Any(e => e.Id == id);
		}
	}
}
