﻿namespace Ploomes.Application.Contracts
{
    public class BuyerPostCreateOrderRequest
    {
        public string? BuyerEmail { get; set; }
        public string? ProductUid { get; set; }
    }
}