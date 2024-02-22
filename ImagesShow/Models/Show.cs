using ImagesShow.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace ImagesShow.Models
{
    internal class Show : FileClass
    {
        public Collection Collection { get; set; }

        public List<Music> Musics { get; } = new List<Music>();
        
        public async Task LoadShowAsync()
        {
            try
            {
               if (Collection is null)
                {
                    var csfolder = await Folders.GetCollectionsAsync();
                    var cfolder = await csfolder.GetFolderAsync(File.Name);
                    Collection = new Collection { Folder = cfolder };
                    await Collection.LoadImagesAsync();
                }
                var mfolder = await Folders.GetMusicsAsync();
                var musics = await FileIO.ReadLinesAsync(File);
                foreach (var music in musics)
                {
                    try
                    {
                        var mfile = await mfolder.GetFileAsync(music);
                        if (mfile != null)
                        {
                            var m = new Music { File = mfile };
                            m.CreateSource();
                            Musics.Add(m);
                        }
                    }catch (Exception) { }
                }
            }catch (Exception) { }
        }
    }
}
