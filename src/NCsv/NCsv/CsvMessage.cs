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
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumericConvertError(ICsvItemContext context)
        {
            return $"{context.Name} must be set to a numeric value.";
        }

        /// <summary>
        /// <see cref="DateTime"/>変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetDateTimeConvertError(ICsvItemContext context)
        {
            return $"Set {context.Name} to a value that can be converted to a date and time.";
        }

        /// <summary>
        /// 真理値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetBooleanConvertError(ICsvItemContext context)
        {
            return $"Set {context.Name} to a value that can be converted to a truth value.";
        }

        /// <summary>
        /// 最大桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="length">桁数。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetMaxLengthError(ICsvItemContext context, int length)
        {
            return $"{context.Name} must be set within {length} digits.";
        }

        /// <summary>
        /// 必須エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetRequiredError(ICsvItemContext context)
        {
            return $"{context.Name} is required.";
        }

        /// <summary>
        /// 数値範囲エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <param name="maxValue">最大値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberOutOfRangeError(ICsvItemContext context, decimal minValue, decimal maxValue)
        {
            return $"Set {context.Name} to a number from {minValue} to {maxValue}.";
        }

        /// <summary>
        /// 数値最小値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberMinValueError(ICsvItemContext context, decimal minValue)
        {
            return $"{context.Name} must be set to a value greater than or equal to {minValue}.";
        }

        /// <summary>
        /// 数値最大値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="maxValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberMaxValueError(ICsvItemContext context, decimal maxValue)
        {
            return $"{context.Name} must be less than or equal to {maxValue}.";
        }

        /// <summary>
        /// 精度エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetPrecisionError(ICsvItemContext context, int precision)
        {
            return $"The {context.Name} must be set within {precision} digits.";
        }

        /// <summary>
        /// 精度と小数部桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetPrecisionAndScaleError(ICsvItemContext context, int precision, int scale)
        {
            return $"The {context.Name} must be within {precision - scale} digits of the integer and {scale} digits of the decimal.";
        }

        /// <summary>
        /// 不正な形式エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetInvalidFormatError(ICsvItemContext context)
        {
            return $"The format of {context.Name} is invalid.";
        }

        /// <summary>
        /// 数字のみエラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetNumberOnlyError(ICsvItemContext context)
        {
            return $"{context.Name} must be set to a number only.";
        }

        /// <summary>
        /// CSVに項目が存在しない場合のメッセージを返します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public virtual string GetItemNotExistError(int lineNumber, string name)
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
