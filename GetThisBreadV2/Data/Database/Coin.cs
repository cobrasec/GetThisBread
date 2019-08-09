using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GetThisBreadV2.Core.Currency
{
    public class Coin
    {
        [Key]
        public ulong UserId { get; set; }
        public int Amount { get; set; }

    }
}
