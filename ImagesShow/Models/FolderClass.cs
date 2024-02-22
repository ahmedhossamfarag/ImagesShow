using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ImagesShow.Models
{
    internal class FolderClass
    {
        public StorageFolder Folder { get; set; }


        public async Task RenameAsync(string name)
        {
            await Folder.RenameAsync(name,NameCollisionOption.GenerateUniqueName);
        }

        public async Task DeleteAsync()
        {
            await Folder.DeleteAsync();
        }
    }
}
