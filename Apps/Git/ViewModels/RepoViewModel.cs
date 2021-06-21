﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels
{
    public class RepoViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CommitsCount { get; set; }
    }
}
