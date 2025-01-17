using NCsv.Resources;
using System;

namespace NCsv
{
    /// <summary>
    /// リソースファイルから取得した文字列内のプレースホルダを変換して完全なメッセージを返します。
    /// </summary>
    internal static class CsvMessages
    {
        /// <summary>
        /// 数値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public static string GetNumericConvertError(ICsvItemContext context)
        {
            return string.Format(Messages.NumericConvertError, context.Name);
        }

        /// <summary>
        /// <see cref="DateTime"/>変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public static string GetDateTimeConvertError(ICsvItemContext context)
        {
            return string.Format(Messages.DateTimeConvertError, context.Name);
        }

        /// <summary>
        /// <see cref="DateTime"/>書式エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="format">書式。</param>
        /// <returns>メッセージ。</returns>
        public static string GetDateTimeFormatError(ICsvItemContext context, string format)
        {
            return string.Format(Messages.DateTimeFormatError, context.Name, format);
        }

        /// <summary>
        /// 真理値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public static string GetBooleanConvertError(ICsvItemContext context)
        {
            return string.Format(Messages.BooleanConvertError, context.Name);
        }

        /// <summary>
        /// 最大桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="length">桁数。</param>
        /// <returns>メッセージ。</returns>
        public static string GetMaxLengthError(ICsvItemContext context, int length)
        {
            return string.Format(Messages.MaxLengthError, context.Name, length);
        }

        /// <summary>
        /// 必須エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public static string GetRequiredError(ICsvItemContext context)
        {
            return string.Format(Messages.RequiredError, context.Name);
        }

        /// <summary>
        /// 数値範囲エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <param name="maxValue">最大値。</param>
        /// <returns>メッセージ。</returns>
        public static string GetNumberOutOfRangeError(ICsvItemContext context, decimal minValue, decimal maxValue)
        {
            return string.Format(Messages.NumberOutOfRangeError, context.Name, minValue, maxValue);
        }

        /// <summary>
        /// 数値最小値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public static string GetNumberMinValueError(ICsvItemContext context, decimal minValue)
        {
            return string.Format(Messages.NumberMinValueError, context.Name, minValue);
        }

        /// <summary>
        /// 数値最大値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="maxValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        public static string GetNumberMaxValueError(ICsvItemContext context, decimal maxValue)
        {
            return string.Format(Messages.NumberMaxValueError, context.Name, maxValue);
        }

        /// <summary>
        /// 精度エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <returns>メッセージ。</returns>
        public static string GetPrecisionError(ICsvItemContext context, int precision)
        {
            return string.Format(Messages.PrecisionError, context.Name, precision);
        }

        /// <summary>
        /// 精度と小数部桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        /// <returns>メッセージ。</returns>
        public static string GetPrecisionAndScaleError(ICsvItemContext context, int precision, int scale)
        {
            return string.Format(Messages.PrecisionAndScaleError, context.Name, precision - scale, scale);
        }

        /// <summary>
        /// 不正な形式エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        public static string GetInvalidFormatError(ICsvItemContext context)
        {
            return string.Format(Messages.InvalidFormatError, context.Name);
        }

        /// <summary>
        /// CSVに項目が存在しない場合のメッセージを返します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        public static string GetItemNotExistError(long lineNumber, string name)
        {
            return Messages.ItemNotExistError;
        }
    }
}
