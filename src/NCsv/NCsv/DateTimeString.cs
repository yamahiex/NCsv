using System;
using System.Globalization;

namespace NCsv
{
    /// <summary>
    /// 日時文字列です。
    /// </summary>
    internal class DateTimeString
    {
        /// <summary>
        /// 値です。
        /// </summary>
        private readonly string value = string.Empty;

        /// <summary>
        /// <see cref="DateTimeString"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value"></param>
        public DateTimeString(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// <see cref="DateTime"/>への変換を試みます。
        /// </summary>
        /// <param name="result">変換結果。</param>
        /// <returns>変換できた場合にtrue。</returns>
        public bool TryParse(out DateTime result)
        {
            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="DateTime"/>への変換を試みます。
        /// </summary>
        /// <param name="format">書式。</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryParse(string format, out DateTime result)
        {
            if (DateTime.TryParseExact(this.value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return true;
            }

            return false;
        }
    }
}
