using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        /// CSVの行から項目を分割するための正規表現です。
        /// </summary>
        private static readonly Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

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
            var items = regex.Split(RemoveNewLineString());
            return new CsvItems(RemoveDoubleQuotation(items).ToArray());
        }

        /// <summary>
        /// CSV文字列を返します。
        /// </summary>
        /// <returns>CSV文字列。</returns>
        public override string ToString()
        {
            return this.value;
        }

        /// <summary>
        /// 末尾にある改行コードを削除します。
        /// </summary>
        /// <returns>改行コードを削除した値。</returns>
        private string RemoveNewLineString()
        {
            return this.value.TrimEnd('\r', '\n');
        }

        /// <summary>
        /// 両端のダブルクォーテーションを削除します。
        /// </summary>
        /// <param name="items">>CSV項目の配列。</param>
        /// <returns>両端のダブルクォーテーションを削除した結果。</returns>
        private IEnumerable<string> RemoveDoubleQuotation(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                yield return RemoveDoubleQuotation(item);
            }
        }

        /// <summary>
        /// 両端のダブルクォーテーションを削除します。
        /// </summary>
        /// <param name="item">>CSV項目。</param>
        /// <returns>両端のダブルクォーテーションを削除した結果。</returns>
        private string RemoveDoubleQuotation(string item)
        {
            var s = item.Trim();
            if (DoubleQuotationSurrounded(s))
            {
                return s.TrimStart('\"').TrimEnd('\"');
            }
            else
            {
                return item;
            }
        }

        /// <summary>
        /// ダブルクォーテーションで囲まれているかどうかを返します。
        /// </summary>
        /// <param name="item">CSV項目。</param>
        /// <returns>囲まれている場合にtrue。</returns>
        private bool DoubleQuotationSurrounded(string item)
        {
            return item.StartsWith("\"") && item.EndsWith("\"");
        }
    }
}
