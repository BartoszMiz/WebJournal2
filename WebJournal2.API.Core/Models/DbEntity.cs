using System.ComponentModel.DataAnnotations;

namespace WebJournal2.API.Core.Models
{
	public abstract class DbEntity
	{
		[Key]
		public uint Id { get; set; }
	}
}
