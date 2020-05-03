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
        public int LineNumber { get; private set; }

        /// <summary>
        /// <see cref="CsvItems"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="items">CSV項目のリスト。</param>
        /// <param name="lineNumber">行番号。</param>
        public CsvItems(IReadOnlyList<string> items, int lineNumber)
        {
            this.items = items;
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// 指定した<paramref name="index"/>に一致するCSV項目を返します。存在しない場合は例外をスローします。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <returns>CSV項目。</returns>
        public string GetItem(int index, string name)
        {
            if (items.Count <= index)
            {
                throw new CsvParseException(CsvConfig.Current.Message.GetItemNotExistError(this.LineNumber, name));
            }

            return items[index];
        }
    }
}
