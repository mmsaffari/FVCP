using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace FVCPD.Data.Models {
	[Index("Name", IsUnique = true, Name = "UK_Tense_Name")]
	public class Tense {
		#region Columns
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public string Remarks { get; set; }
		#endregion

		#region Foreign Keys
		public virtual ICollection<ConjugatedVerb>? ConjugatedVerbs { get; set; }
		#endregion

		public Tense() {
			Id = 0;
			Name = string.Empty;
			Enabled = false;
			Remarks = string.Empty;
		}
	}
}
