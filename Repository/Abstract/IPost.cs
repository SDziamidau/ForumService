﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumRepository
{
    public interface IPost
    {
        int Id { get; set; }
        string Body { get; set; }
    }
}
