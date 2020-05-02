using System;

namespace NCsv
{
    /// <summary>
    /// 当ライブラリで使用するメッセージです。
    /// 独自のメッセージを使用したい場合はサブクラスでオーバーライドして<see cref="CsvConfig"/>に設定してください。
    /// </summary>
    public class CsvMessage
    {
        /// <summary>
        /// 数値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumericConvertError(string name)
        {
            return $"{name} must be set to a numeric value.";
        }

        /// <summary>
        /// <see cref="DateTime"/>変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetDateTimeConvertError(string name)
        {
            return $"Set {name} to a value that can be converted to a date and time.";
        }

        /// <summary>
        /// 真理値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetBooleanConvertError(string name)
        {
            return $"Set {name} to a value that can be converted to a truth value.";
        }

        /// <summary>
        /// 最大桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="length">桁数。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetMaxLengthError(string name, int length)
        {
            return $"{name} must be set within {length} digits.";
        }

        /// <summary>
        /// 必須エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetRequiredError(string name)
        {
            return $"{name} is required.";
        }

        /// <summary>
        /// 数値範囲エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="minValue">最小値。</param>
        /// <param name="maxValue">最大値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberOutOfRangeError(string name, decimal minValue, decimal maxValue)
        {
            return $"Set {name} to a number from {minValue} to {maxValue}.";
        }

        /// <summary>
        /// 数値最小値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="minValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberMinValueError(string name, decimal minValue)
        {
            return $"{name} must be set to a value greater than or equal to {minValue}.";
        }

        /// <summary>
        /// 数値最大値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="maxValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberMaxValueError(string name, decimal maxValue)
        {
            return $"{name} must be less than or equal to {maxValue}.";
        }

        /// <summary>
        /// 精度エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="precision">精度。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetPrecisionError(string name, int precision)
        {
            return $"The {name} must be set within {precision} digits.";
        }

        /// <summary>
        /// 精度と小数部桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetPrecisionAndScaleError(string name, int precision, int scale)
        {
            return $"The {name} must be within {precision - scale} digits of the integer and {scale} digits of the decimal.";
        }

        /// <summary>
        /// 不正な形式エラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetInvalidFormatError(string name)
        {
            return $"The format of {name} is invalid.";
        }

        /// <summary>
        /// 数字のみエラー時のメッセージを返します。
        /// </summary>
        /// <param name="name">メッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberOnlyError(string name)
        {
            return $"{name} must be set to a number only.";
        }

        /// <summary>
        /// CSVに項目が存在しない場合のメッセージを返します。
        /// </summary>
        /// <returns>メッセージ。</returns>
        public virtual string GetItemNotExistError()
        {
            return "The item does not exist in the CSV.";
        }

        /// <summary>
        /// CSV行が不正な場合のメッセージを返します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetInvalidLineError(int lineNumber)
        {
            return $"The line {lineNumber} is an invalid line.";
        }
    }
}
