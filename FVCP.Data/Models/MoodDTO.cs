namespace FVCPD.Models {
	public class MoodDTO {
		public int Id { get; set; } = -1;
		public string Name { get; set; } = string.Empty;
		public bool Enabled { get; set; } = false;
		public string Remarks { get; set; }=string.Empty;
	}
}
