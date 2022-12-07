using System.ComponentModel.DataAnnotations;

namespace RootRPGAPI.Models
{
	public class Equipment
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		public long Load { get; set; }
		[Required]
		public long Value { get; set; }
		[Required]
		public string Range { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public long MaxWear { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public long Wear { get; set; }
		[Required]
		public string HarmType { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public long HarmValue { get; set; }

		public List<Tag>? Tags { get; set; }
		public List<WeaponSkill>? WeaponSkills { get; set; }
	}
}
