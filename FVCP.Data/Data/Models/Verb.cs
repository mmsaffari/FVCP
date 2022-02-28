using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace FVCPD.Data.Models {
	[Index("Infinitive", IsUnique = true, Name = "UK_Verb_Infinitive")]
	public class Verb {
		#region Columns
		[Key]
		public int Id { get; set; }
		public string Infinitive { get; set; }
		public string PastParticiple { get; set; }
		public string Group { get; set; }
		public string Remarks { get; set; }
		#endregion

		#region Foreign Keys
		public virtual ICollection<ConjugatedVerb>? ConjugatedVerbs { get; set; }
		#endregion

		public Verb() {
			Id = 0;
			Infinitive =string.Empty;
			PastParticiple = string.Empty;
			Group = string.Empty;
			Remarks = string.Empty;
		}
	}
}
