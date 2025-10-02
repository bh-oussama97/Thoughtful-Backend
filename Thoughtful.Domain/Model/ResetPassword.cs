﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughtful.Domain.Model
{
    public class ResetPassword
    {
        [Key]
        public int Id { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(5000)]
        public string Token { get; set; }

        [StringLength(10)]
        public string ResetCode { get; set; }

        public DateTime InsertDateTimeUTC { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}
