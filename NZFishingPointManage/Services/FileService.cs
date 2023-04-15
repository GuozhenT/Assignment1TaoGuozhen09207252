namespace NZFishingPointManage.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment environment;

    public FileService(IWebHostEnvironment env)
    {
        environment = env;
    }

    /*
     *Generic method for saving images
     */
    public Tuple<int, string> SaveImage(IFormFile imageFile)
    {
        try
        {
            var wwwPath = environment.WebRootPath;
            var path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            // Check the allowed extenstions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                var msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                return new Tuple<int, string>(0, msg);
            }

            var uniqueString = Guid.NewGuid().ToString();
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            imageFile.CopyTo(stream);
            stream.Close();
            return new Tuple<int, string>(1, newFileName);
            ;
        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }
    }

    /*
     *  Generic method for deleting images
     */
    public bool DeleteImage(string imageFileName)
    {
        try
        {
            var wwwPath = environment.WebRootPath;
            var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}