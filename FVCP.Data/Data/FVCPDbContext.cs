using FVCPD.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace FVCPD.Data {
	public class FVCPDbContext : DbContext {
		public FVCPDbContext(DbContextOptions options) : base(options) {
		
		}

		public FVCPDbContext(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder.Options) {
		
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			
		}

		#region Tables
		public DbSet<Pronoun> Pronouns => Set<Pronoun>();
		public DbSet<Tense> Tenses => Set<Tense>();
		public DbSet<Mood> Moods => Set<Mood>();
		public DbSet<Verb> Verbs => Set<Verb>();
		public DbSet<ConjugatedVerb> ConjugatedVerbs => Set<ConjugatedVerb>(); 
		#endregion

	}
}
