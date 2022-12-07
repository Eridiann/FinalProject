using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RootRPGAPI.Models;

namespace RootRPGAPI.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class WeaponSkillController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public WeaponSkillController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<List<WeaponSkill>> GetWeaponSkill()
		{
			List<WeaponSkill> WeaponSkillList;
			WeaponSkillList = await context.WeaponSkills.ToListAsync();
			return WeaponSkillList;
		}

		[HttpPut]
		public async Task<ActionResult<bool>> PutWeaponSkill(WeaponSkill weaponSkill)
		{
			var weaponSkillCheck = context.WeaponSkills.FirstOrDefault(x => x.Id == weaponSkill.Id);
			if (weaponSkillCheck == null)
			{
				context.Add(weaponSkill);
				await context.SaveChangesAsync();
				return Ok();
			}

			context.Attach(weaponSkillCheck).State = EntityState.Modified;
			weaponSkillCheck.Name = weaponSkill.Name;
			weaponSkillCheck.Description = weaponSkill.Description;
			weaponSkillCheck.Range = weaponSkill.Range;
			await context.SaveChangesAsync();

			return Ok();
		}
		[HttpDelete("{Id}")]
		public async Task<ActionResult> DeleteWeaponSkill(long Id)
		{
			var weaponSkill = context.WeaponSkills.FirstOrDefault(x => x.Id == Id);
			if (weaponSkill != null)
			{
				context.Remove(weaponSkill);
				await context.SaveChangesAsync();
				return Ok();
			}
			else
			{
				return NotFound();
			}

		}
	}
}
