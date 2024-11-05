using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_app.Models.Entity
{
	/// <summary>
	/// ユーザマスタ
	/// </summary>
	[Table("m_user")]
	public class User
	{
		/// <summary>
		/// ユーザID
		/// </summary>
		public string? Id { get; set; }

		/// <summary>
		/// ユーザ名
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string? Password { get; set; }
	}
}
