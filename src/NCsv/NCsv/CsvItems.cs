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
        /// <see cref="CsvItems"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="items">CSV項目のリスト。</param>
        public CsvItems(IReadOnlyList<string> items)
        {
            this.items = items;
        }

        /// <summary>
        /// 指定した<paramref name="index"/>に一致するCSV項目を返します。存在しない場合は例外をスローします。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>CSV項目。</returns>
        public string GetItem(int index)
        {
            if (items.Count <= index)
            {
                throw new CsvParseException(NCsvConfig.Current.Message.GetItemNotExistError());
            }

            return items[index];
        }
    }
}
