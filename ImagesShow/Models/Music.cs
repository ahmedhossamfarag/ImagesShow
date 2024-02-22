using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Storage;

namespace ImagesShow.Models
{
    internal class Music : FileClass
    {

        public MediaSource Source { get; set; }

        public void CreateSource() 
        { 
            Source  = MediaSource.CreateFromStorageFile(File);
        }

    }
}
