using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Ticket
{
    public class TicketDtos
    {

        public string Barcode { get; set; } = null!;

        public decimal Price { get; set; }

        public int? Quantity { get; set; }

        public string? SeatNumber { get; set; }

        public int? SellerId { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? File { get; set; }

        public string? Description { get; set; }
    }
}
