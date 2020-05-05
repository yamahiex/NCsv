namespace NCsv
{
    /// <summary>
    /// コンフィグです。
    /// </summary>
    public class CsvConfig
    {
        /// <summary>
        /// <see cref="CsvConfig"/>のインスタンスを取得します。
        /// </summary>
        public static CsvConfig Current { get; } = new CsvConfig();

        /// <summary>
        /// <see cref="ICsvValidationMessage"/>を取得または設定します。
        /// </summary>
        public ICsvValidationMessage ValidationMessage { get; set; } = new CsvValidationDefaultMessage();

        /// <summary>
        /// <see cref="CsvConfig"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        private CsvConfig()
        {
        }
    }
}
