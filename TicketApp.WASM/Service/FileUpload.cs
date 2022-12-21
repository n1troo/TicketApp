using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;
using TicketApp_UI.WASM.Contracts;
namespace TicketApp_UI.WASM.Service;

//public class FileUpload : IFileUpload
//{
    //private readonly IWebHostEnvironment _env;
    //public FileUpload(IWebHostEnvironment env)
    //{
    //    _env = env;
    //}

    //public void RemoveFile(string picName)
    //{
    //    var path = $"{_env.WebRootPath}\\uploads\\{picName}";
    //    if (File.Exists(path)) File.Delete(path);
    //}

    //public async Task UploadFile(IFileListEntry file, string picName)
    //{
    //    var ms = new MemoryStream();
    //    await file.Data.CopyToAsync(ms);

    //    var path = $"{_env.WebRootPath}\\uploads\\{picName}";

    //    using (var fs = new FileStream(path, FileMode.Create))
    //    {
    //        ms.WriteTo(fs);
    //    }
    //}

    //public void UploadFile(IFileListEntry file, MemoryStream msFile, string picName)
    //{
    //    var path = $"{_env.WebRootPath}\\uploads\\{picName}";

    //    using (var fs = new FileStream(path, FileMode.Create))
    //    {
    //        msFile.WriteTo(fs);
    //    }
    //}
//}