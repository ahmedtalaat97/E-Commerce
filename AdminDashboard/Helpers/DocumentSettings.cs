namespace AdminDashboard.Helpers
{
	public class DocumentSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			//1- file location path

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

			// 2- get file name and make it unique 

			var fileName = $"{Guid.NewGuid().ToString()}-{Path.GetFileName(file.FileName)}";

			//3- get file path

			var filePath= Path.Combine(folderPath, fileName);

			// 4 - use file stream to make a copy

			using var fileStream = new FileStream(filePath, FileMode.Create);
			
			file.CopyTo(fileStream);

			return Path.Combine("images\\products",fileName);


		}


		public static bool DeleteFile(string imageUrl, string folderName)
		{
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folderName);

			var filePath=Path.Combine(folderPath , imageUrl);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				return true;
			}
		
			return false;

		}
	}
}
