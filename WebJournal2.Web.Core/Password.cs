using System.ComponentModel.DataAnnotations;

namespace WebJournal2.Web.Core
{
	public class Password
	{
		[Required(ErrorMessage = "Please enter the password!")]
		[MinLength(5, ErrorMessage = "The password must be at least 5 characters long!")]
		[MaxLength(30, ErrorMessage = "The password cannot be longer than 30 characters!")]
		public string Content {get; set;}
		public Password()
		{
			Content = string.Empty;
		}
	}
}