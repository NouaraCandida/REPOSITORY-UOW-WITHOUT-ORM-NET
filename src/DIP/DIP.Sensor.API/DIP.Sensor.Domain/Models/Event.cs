using System;
using DIP.Core.Domain;

namespace DIP.Domain.Models
{
    public class Event:Entity, IAggregateRoot
    {
        public Int64 TimeStamp { get; set; }
        public string Value { get; set; }
    }
}
