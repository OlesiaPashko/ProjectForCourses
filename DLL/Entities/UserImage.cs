using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DLL.Entities
{
    public class UserImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }

    }
}
