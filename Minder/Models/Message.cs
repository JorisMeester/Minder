using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minder.Models
{
    public class Message
    {
        public string Id { get; set; }
        [Required]
        public DateTime TimeSent { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        [Required]
        public ApplicationUser Sender { get; set; }
        [Required]
        public ApplicationUser Receiver { get; set; }
        public bool IsEdited { get; set; }
    }
}