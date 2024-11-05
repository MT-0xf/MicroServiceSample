using dotnet_app.Logic;
using Grpc.Core;

namespace dotnet_app.Services;

/// <summary>
/// 主要業務サービス
/// </summary>
public class MainBusinessService : MainBusiness.MainBusinessBase
{
	/// <summary>
	/// 主要業務ロジック
	/// </summary>
	private MainBusinessLogic mainBusinessLogic;

	/// <summary>
	/// コンストラクタ
	/// </summary>
    public MainBusinessService()
    {
		mainBusinessLogic = new MainBusinessLogic();
	}

	/// <summary>
	/// ログイン
	/// </summary>
	/// <param name="request">リクエスト</param>
	/// <param name="context">コンテキスト</param>
	/// <returns>ログイン可否</returns>
	public override Task<LoginReply> login(LoginRequest request, ServerCallContext context)
    {
		return mainBusinessLogic.login(request, context);
	}

	/// <summary>
	/// 画像アップロード
	/// </summary>
	/// <param name="request">リクエストparam>
	/// <param name="context">コンテキスト</param>
	/// <returns>アップロード可否</returns>
	public override async Task<PostImageReply> PostImage(PostImageRequest request, ServerCallContext context)
	{
		return await mainBusinessLogic.PostImage(request, context);
	}

	/// <summary>
	/// 画像一覧取得
	/// </summary>
	/// <param name="requrest">リクエスト</param>
	/// <param name="stream">ストリーム</param>
	/// <param name="context">コンテキスト</param>
	/// <returns>画像一覧</returns>
	public override async Task getPostImageList(EmptyRequest requrest, IServerStreamWriter<GetPostImageListReply> stream, ServerCallContext context)
	{
		await mainBusinessLogic.getPostImageList(requrest, stream, context);
	}
}
