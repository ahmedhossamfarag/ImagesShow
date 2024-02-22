using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ImagesShow.Models
{
    internal class FileClass
    {
        public StorageFile File { get; set; }

        public async Task RenameAsync(string name)
        {
            await File.RenameAsync(name);
        }

        public async Task DeleteAsync()
        {
            await File.DeleteAsync();
        }
    }
}
