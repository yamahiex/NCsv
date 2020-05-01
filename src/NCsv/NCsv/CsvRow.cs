using NotVisualBasic.FileIO;
using System.IO;

namespace NCsv
{
    /// <summary>
    /// CSVの行です。
    /// </summary>
    internal class CsvRow
    {
        /// <summary>
        /// 値です。
        /// </summary>
        private readonly string value;

        /// <summary>
        /// <see cref="CsvRow"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">値。</param>
        public CsvRow(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// <see cref="CsvItems"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvItems"/>。</returns>
        public CsvItems ToCsvItems()
        {
            using var parser = new CsvTextFieldParser(new StringReader(this.value));
            return new CsvItems(parser.ReadFields());
        }

        /// <summary>
        /// CSV文字列を返します。
        /// </summary>
        /// <returns>CSV文字列。</returns>
        public override string ToString()
        {
            return this.value;
        }
    }
}
