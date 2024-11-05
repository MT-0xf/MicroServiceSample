using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_app.Models.Entity
{
	/// <summary>
	/// 登録が増テーブル
	/// </summary>
	[Table("t_post_image")]
	public class PostImage
	{
	    /// <summary>
		/// ファイル名
		/// </summary>
		[Key]
		public string FileName { get; set; } = "";

		/// <summary>
		/// 画像タイトル
		/// </summary>
		public string? Title { get; set; }
	}
}
