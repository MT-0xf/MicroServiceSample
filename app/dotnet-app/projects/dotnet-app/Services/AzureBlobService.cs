using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Models.DTO;

namespace dotnet_app.Services
{
	/// <summary>
	/// AzureBlobサービス
	/// </summary>
	public class AzureBlobService
	{
		/// <summary>
		/// 拡張子取得
		/// </summary>
		/// <param name="filename">ファイル名</param>
		/// <returns></returns>
		private static string GetContentType(string filename)
		{
			switch (Path.GetExtension(filename).ToLower())
			{
				// Text
				case ".txt":
					return "text/plain";
				// Image
				case ".pdf":
					return "application/pdf";
				case ".jpg":
				case ".jpeg":
					return "image/jpeg";
				case ".png":
					return "image/png";
				// Zip
				case ".zip":
					return "application/zip";
				// Others
				default:
					return "";
			}
		}

		/// <summary>
		/// BlobClientを作成する
		/// </summary>
		/// <param name="blobName">blob名</param>
		/// <returns></returns>
		public static BlobClient CreateBlobClient(string blobName)
		{
			string connectionString = "";
			string blobContainerName = "";

			return new BlobClient(connectionString, blobContainerName, blobName);
		}

		/// <summary>
		/// 画像アップロード
		/// </summary>
		/// <param name="fileStream">画像ファイル</param>
		/// <param name="fileName">ファイル名</param>
		/// <returns></returns>
		public static async Task UploadAsync(MemoryStream fileStream, string fileName)
		{
			BlobClient blobClient = CreateBlobClient(fileName);
			BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders()
			{
				ContentType = GetContentType(fileName)
			};
			// !ATTENTION! 同名のファイルがあった場合、上書きする処理です。
			await blobClient.UploadAsync(fileStream, blobHttpHeaders);
		}

		/// <summary>
		/// 画像ダウンロード
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>画像ファイル</returns>
		/// <exception cref="Exception"></exception>
		public static async Task<ImageFile> DownloadAsync(string fileName)
		{
			BlobClient blobClient = CreateBlobClient(fileName);

			// ファイル名が一致するものがあるかどうかを確認
			Azure.Response<bool> isBlobExists = await blobClient.ExistsAsync();
			if (!isBlobExists.Value)
			{
				throw new Exception("ファイルが見つかりません。");
			}

			// ファイルをダウンロードする
			Azure.Response<BlobDownloadResult> blobDownloadResult = await blobClient.DownloadContentAsync();
			if (blobDownloadResult.GetRawResponse().IsError)
			{
				throw new Exception("ファイルのダウンロードに失敗しました。");
			}

			// ファイルのデータを取り出して返す
			ImageFile fileInfo = new ImageFile();
			fileInfo.Content = blobDownloadResult.Value.Content.ToArray();
			fileInfo.ContentType = blobDownloadResult.Value.Details.ContentType;
			return fileInfo;
		}
	}
}
