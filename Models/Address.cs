using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }
        public int CityId { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

        [Display(Name = "Day of pickup")]
        public string pickUpDay { get; set; }

        [Display(Name = "Day to start no pick up")]
        [DataType(DataType.Date)]
        public DateTime? vacationStart { get; set; }

        [Display(Name = "Day to end no pick up")]
        [DataType(DataType.Date)]
        public DateTime? vacationEnd { get; set; }

        [Display(Name = "Single pick up day")]
        [DataType(DataType.Date)]
        public DateTime? SingleDate { get; set; }


        [Display(Name = "Picked up?")]
        public bool? PickedUp { get; set; }


        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}