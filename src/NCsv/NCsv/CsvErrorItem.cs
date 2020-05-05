namespace NCsv
{
    /// <summary>
    /// エラー項目です。
    /// </summary>
    public class CsvErrorItem
    {
        /// <summary>
        /// エラーメッセージを取得します。
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// <see cref="ICsvItemContext"/>を取得します。
        /// </summary>
        public ICsvItemContext Context { get; private set; }

        /// <summary>
        /// <see cref="CsvErrorItem"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        public CsvErrorItem(string errorMessage, ICsvItemContext context)
        {
            this.Context = context;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// オブジェクトの内容を表す文字列を返します。
        /// </summary>
        /// <returns>オブジェクトの内容を表す文字列。</returns>
        public override string ToString()
        {
            return $"LineNumber={this.Context.LineNumber}, ErrorMessage={this.ErrorMessage}";
        }
    }
}
