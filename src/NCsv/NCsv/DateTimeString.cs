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
            result = DateTime.MinValue;

            if (DateTime.TryParse(value, out result))
            {
                return true;
            }

            if (DateTime.TryParseExact(value, "yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }

            if (DateTime.TryParseExact(value, "yyyy/MM/dd", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }

            if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }

            if (DateTime.TryParseExact(value, "yyyy/MM", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }

            if (DateTime.TryParseExact(value, "yyyyMM", CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return true;
            }

            return false;
        }
    }
}
