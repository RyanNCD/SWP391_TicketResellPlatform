﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Wallet
{
    public int WalletId { get; set; }

    public int? UserId { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}