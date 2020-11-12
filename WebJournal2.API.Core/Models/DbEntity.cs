using System.ComponentModel.DataAnnotations;

namespace WebJournal2.Core.Models
{
	public abstract class DbEntity
	{
		[Key]
		public uint Id { get; set; }
	}
}
