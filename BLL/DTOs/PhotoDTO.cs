using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTOs
{
    public class PhotoDTO
    {
        public byte[] ImageData { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public bool Like { get; set; }
        public string PostId { get; set; }
        public PostDTO Post { get; set; }

    }
}
