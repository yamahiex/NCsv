namespace NCsv
{
    /// <summary>
    /// コンフィグです。
    /// </summary>
    public class NCsvConfig
    {
        /// <summary>
        /// <see cref="NCsvConfig"/>のインスタンスを取得します。
        /// </summary>
        public static NCsvConfig Current { get; } = new NCsvConfig();

        /// <summary>
        /// <see cref="CsvMessage"/>を取得または設定します。
        /// </summary>
        public CsvMessage Message { get; set; } = new CsvMessage();

        /// <summary>
        /// <see cref="NCsvConfig"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        private NCsvConfig()
        {
        }
    }
}
