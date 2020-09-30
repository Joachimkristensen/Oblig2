using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Comment : BlogEntity
    {
        public virtual IdentityUser Owner { get; set; }
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        // Navigational properties
        public string PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
