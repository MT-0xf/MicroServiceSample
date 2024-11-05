namespace Models.DTO
{
    /// <summary>
    /// 画像ファイル
    /// </summary>
    public class ImageFile
    {
        /// <summary>
        /// コンテンツ
        /// </summary>
        // ファイルデータのバイト配列
        public byte[]? Content { get; set; }

        /// <summary>
        /// 拡張子
        /// </summary>
        public string? ContentType { get; set; }
    }
}
