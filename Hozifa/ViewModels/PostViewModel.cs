using Hozifa.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostDesc { get; set; }
        public ApplicationUser PostAuthor { get; set; }
    }
}
