﻿using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Aggregates
{
    public class UserAggregate
    {
        [Key]
        [Column("Id")]
        public int UserAggregateId { get; set; }
        public User User { get; set; }
        public Roles Role { get; set; }
        public Address Address { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}