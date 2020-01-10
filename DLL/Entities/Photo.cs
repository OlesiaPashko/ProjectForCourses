using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Entities
{
    public class Photo
    {
        public byte[] ImageData { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public bool Like { get; set; }
        public string PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

    }
}
