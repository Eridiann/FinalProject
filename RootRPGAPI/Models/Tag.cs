using System.ComponentModel.DataAnnotations;

namespace RootRPGAPI.Models
{
	public class Tag
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Effect { get; set; }
		[Required]
		public bool Positive { get; set; }
	}
}
