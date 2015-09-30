﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_cleansing.net.Models
{
    public class DeduplicateCleansingHistory
    {
        public int Id { get; set; }
        public Nullable<int> UniqueRecords { get; set; }
        public Nullable<int> DuplicateRecords { get; set; }
        public Nullable<int> SubmitedRecords { get; set; }
        public Nullable<System.DateTime> DateSubmited { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}