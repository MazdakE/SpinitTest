using System.ComponentModel.DataAnnotations.Schema;

namespace SpinitTest.Entities
{
    public class HistoryLogEntity : StateEntity
    {
        public Guid Id { get; set; }
        public string? OrderBy { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
