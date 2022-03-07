namespace FVCPD.Models {
	public class ConjugatedVerbDTO {
		public int Id { get; set; } = -1;
		public int PronounId { get; set; } = -1;
		public int TenseId { get; set; } = -1;
		public int MoodId { get; set; } = -1;
		public int VerbId { get; set; } = -1;
		public string Conjugation { get; set; } = string.Empty;
	}
}
