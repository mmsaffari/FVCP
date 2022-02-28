using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FVCPD.Data.Models {
	[Index("PronounId", "TenseId", "MoodId", "VerbId", IsUnique =true, Name ="UK_ConjugatedVerb")]
	public class ConjugatedVerb {
		#region Columns
		[Key]
		public int Id { get; set; }
		public int PronounId { get; set; }
		public int TenseId { get; set; }
		public int MoodId { get; set; }
		public int VerbId { get; set; }
		public string Conjugation { get; set; } 
		#endregion

		#region Foreign Keys
		[ForeignKey(nameof(PronounId))]
		public virtual Pronoun? Pronoun { get; set; }
		[ForeignKey(nameof(TenseId))]
		public virtual Tense? Tense { get; set; }
		[ForeignKey(nameof(MoodId))]
		public virtual Mood? Mood { get; set; }
		[ForeignKey(nameof(VerbId))]
		public virtual Verb? Verb { get; set; }
		#endregion

		public ConjugatedVerb() {
			Id = 0;
			PronounId = 0;
			TenseId = 0;
			MoodId = 0;
			VerbId = 0;
			Conjugation = string.Empty;
		}
	}
}
