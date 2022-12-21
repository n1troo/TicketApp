using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace TicketApp_UI.WASM.Contracts;

public interface IFileUpload
{
    public Task UploadFile(IFileListEntry file, string picName);
    public void UploadFile(IFileListEntry file, MemoryStream msFile, string picName);
    public void RemoveFile(string picName);
}