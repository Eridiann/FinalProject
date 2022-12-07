using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RootRPGAPI.Models;

namespace RootRPGAPI.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class EquipmentController : ControllerBase
	{

		private readonly ApplicationDbContext context;

		public EquipmentController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<List<Equipment>> GetEquipment()
		{
			List<Equipment> EquipmentList;
			EquipmentList = await context.Equipment.ToListAsync();
			return EquipmentList;
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<Equipment>> GetEquipmentDetail(long Id)
		{
			var equipment = new Equipment();
			equipment = await context.Equipment.FirstOrDefaultAsync(x => x.Id == Id);
			if (equipment == null) return NotFound();
			return equipment;
		}

		[HttpPut]
		public async Task<ActionResult<bool>> PutEquipment(Equipment equipment)
		{
			var equipmentCheck = context.Equipment.FirstOrDefault(x => x.Id == equipment.Id);
			if (equipmentCheck == null)
			{
				context.Add(equipment);
				await context.SaveChangesAsync();
				return Ok();
			}

			context.Attach(equipmentCheck).State = EntityState.Modified;
			equipmentCheck.Name = equipment.Name;
			equipmentCheck.Description = equipment.Description;
			equipmentCheck.Load = equipment.Load;
			equipmentCheck.Value = equipment.Value;
			equipmentCheck.Range = equipment.Range;
			equipmentCheck.MaxWear = equipment.MaxWear;
			equipmentCheck.Wear = equipment.Wear;
			equipmentCheck.HarmType = equipment.HarmType;
			equipmentCheck.HarmValue = equipment.HarmValue;
			equipmentCheck.Tags = equipment.Tags;
			equipmentCheck.WeaponSkills = equipment.WeaponSkills;
			await context.SaveChangesAsync();

			return Ok();
		}
		[HttpDelete("{Id}")]
		public async Task<ActionResult> DeleteEquipment(long Id)
		{
			var equipment = context.Equipment.FirstOrDefault(x => x.Id == Id);
			if (equipment != null)
			{
				context.Remove(equipment);
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
