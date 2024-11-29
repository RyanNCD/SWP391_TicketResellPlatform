using Repository.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Category
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public ICollection<TicketDtos> Tickets { get; set; } = new List<TicketDtos>();
    }
}
