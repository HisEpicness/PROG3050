﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    public class UserModel
    {
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string mailAddress { get; set; }
        public string shipAddress { get; set; }
        public decimal age { get; set; }
        public Nullable<bool> employee { get; set; }
    }
}