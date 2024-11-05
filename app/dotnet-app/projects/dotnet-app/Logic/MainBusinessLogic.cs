using dotnet_app.Infra;
using dotnet_app.Models.Entity;
using dotnet_app.Services;
using Grpc.Core;

namespace dotnet_app.Logic
{
	/// <summary>
	/// 主要業務ロジック
	/// </summary>
	public class MainBusinessLogic
	{
		/// <summary>
		/// ログイン
		/// </summary>
		/// <param name="request">リクエスト</param>
		/// <param name="context">コンテキスト</param>
		/// <returns>ログイン可否</returns>
		public Task<LoginReply> login(LoginRequest request, ServerCallContext context)
		{
			using (var mainBusinessContext = new MainBusinessContext())
			{
				// ログインユーザ取得
				var targetUser = mainBusinessContext.Users.Where(user =>
					user.Name == request.Name && user.Password == request.Password
				).ToList();

				// ログイン成功
				if (targetUser.Count > 0)
				{
					User user = targetUser[0];

					return Task.FromResult(new LoginReply
					{
						Message = "Hello " + user.Name
					});
				}
				// ログイン失敗
				else
				{
					return Task.FromResult(new LoginReply
					{
						Message = "Login Failed"
					});
				}
			}
		}

		/// <summary>
		/// 画像アップロード
		/// </summary>
		/// <param name="request">リクエスト</param>
		/// <param name="context">コンテキスト</param>
		/// <returns>アップロード可否</returns>
		public async Task<PostImageReply> PostImage(PostImageRequest request, ServerCallContext context)
		{
			// Base64デコード
			byte[] bytes = Convert.FromBase64String(request.ImageFile.Split(",")[1]);

			// ファイルストリーム取得
			var fileStream = new MemoryStream(bytes);

			// Guid生成
			Guid guid = Guid.NewGuid();

			// ファイル名作成
			string fileName = guid.ToString() + ".jpg";

			// 画像アップロード
			await AzureBlobService.UploadAsync(fileStream, fileName);

			// DB登録
			using (var mainBusinessContext = new MainBusinessContext())
			{
				var postImage = new PostImage
				{
					FileName = fileName,
					Title = request.Title
				};
				mainBusinessContext.PostImages.Add(postImage);
				mainBusinessContext.SaveChanges();
			}

			// レスポンス返却
			return new PostImageReply
			{
				Message = "Post Image Complete"
			};
		}

		public async Task getPostImageList(EmptyRequest requrest, IServerStreamWriter<GetPostImageListReply> stream, ServerCallContext context)
		{
			using (var mainBusinessContext = new MainBusinessContext())
			{
				// DB登録画像取得
				var postImageList = mainBusinessContext.PostImages.ToList();

				foreach (var postImage in postImageList)
				{
					// ファイル取得
					var file = await AzureBlobService.DownloadAsync(postImage.FileName);

					if (file == null) {
						continue;
					}

					if (file.Content != null)
					{
						// Base64エンコード
						string base64String = Convert.ToBase64String(file.Content);

						// 画像ファイル
						PostImageFile postImageFile = new PostImageFile();
						postImageFile.Content = base64String;
						postImageFile.ContentType = "jpg";

						// レスポンス作成
						var result = new GetPostImageListReply();
						result.PostImageList = postImageFile;

						// ストリーム返却
						await stream.WriteAsync(result);
					}
				}
			}
		}
	}
}
