using System.ComponentModel.DataAnnotations;

namespace RootRPGAPI.Models
{
	public class myUser
	{
		[Requiered]
		public string UserName { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		[Requiered]
		public string Password { get; set; }
	}
}
