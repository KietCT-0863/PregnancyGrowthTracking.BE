﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreateBlogDTO
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<CreateBlogCategoryDTO> CreateBlogCategories { get; set; } = new();

        public class CreateBlogCategoryDTO
        {
            public string CategoryName { get; set; } = null!;
        }
    }
}
