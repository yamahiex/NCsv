using System.Collections.Generic;

namespace NCsv
{
    /// <summary>
    /// CSV項目のファーストクラスコレクションです。
    /// </summary>
    internal class CsvItems
    {
        /// <summary>
        /// CSV項目のリストです。
        /// </summary>
        private readonly IReadOnlyList<string> items;

        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public long LineNumber { get; private set; }

        /// <summary>
        /// <see cref="CsvItems"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="items">CSV項目のリスト。</param>
        /// <param name="lineNumber">行番号。</param>
        public CsvItems(IReadOnlyList<string> items, long lineNumber)
        {
            this.items = items;
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// 指定した<paramref name="index"/>に一致するCSV項目の取得を試みます。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="csvItem">CSV項目。</param>
        /// <returns>取得できた場合にtrue。</returns>
        public bool TryGetItem(int index, out string csvItem)
        {
            csvItem = string.Empty;

            if (items.Count <= index)
            {
                return false;
            }

            csvItem = items[index];
            return true;
        }
    }
}
