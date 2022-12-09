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
	public class TagController : ControllerBase
	{

		private readonly ApplicationDbContext context;
		
		public TagController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<List<Tag>> GetTags()
		{
			List<Tag> TagList;
			TagList = await context.Tags.ToListAsync();
			return TagList;
		}

		[HttpPut]
		public async Task<ActionResult<bool>> PutTag(Tag tag)
		{
			var tagCheck = context.Tags.FirstOrDefault(x => x.Id == tag.Id);
			if (tagCheck == null)
			{
				context.Add(tag);
				await context.SaveChangesAsync();
				return Ok();
			}

			context.Attach(tagCheck).State = EntityState.Modified;
			tagCheck.Name = tag.Name;
			tagCheck.Effect = tag.Effect;
			tagCheck.Positive = tag.Positive;
			await context.SaveChangesAsync();

			return Ok();
		}
		[HttpDelete("{Id}")]
		public async Task<ActionResult> DeleteTag(long Id)
		{
			var tag = context.Tags.FirstOrDefault(x => x.Id == Id);
			if (tag != null)
			{
				context.Remove(tag);
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
