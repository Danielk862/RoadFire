using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public List<TransactionDetailDto> TransactionDetailDtos { get; set; } = new List<TransactionDetailDto>();

        public TransactionDto()
        {
            TransactionDetailDtos = new List<TransactionDetailDto>();
        }
    }
}
