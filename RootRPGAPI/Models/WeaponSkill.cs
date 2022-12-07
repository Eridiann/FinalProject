using System.ComponentModel.DataAnnotations;

namespace RootRPGAPI.Models
{
	public class WeaponSkill
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Range { get; set; }
	}
}
