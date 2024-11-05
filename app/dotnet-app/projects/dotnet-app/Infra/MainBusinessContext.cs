using dotnet_app.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace dotnet_app.Infra
{
	/// <summary>
	/// 主要業務コンテキスト
	/// </summary>
	public class MainBusinessContext : DbContext
	{
		/// <summary>
		/// ユーザ
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// 登録画像
		/// </summary>
		public DbSet<PostImage> PostImages { get; set; }

		/// <summary>
		/// コンフィグ
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"");
		}
	}
}
