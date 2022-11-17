using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Interfaces
{
    public interface Interface_hlab_web_gallery
    {
        IEnumerable<hlab_web_gallery> GetAllImages(int service_id);
        hlab_web_gallery GetImageInfo(int id);

        bool AddNewItem(hlab_web_gallery item);
        bool UpdateItem(hlab_web_gallery item);
        bool DeleteItem(hlab_web_gallery service_info);
    }
}
