﻿using System;

namespace NCsv
{
    /// <summary>
    /// 当ライブラリで使用するメッセージです。
    /// 独自のメッセージを使用したい場合は当interfaceを実装して<see cref="CsvConfig"/>に設定してください。
    /// </summary>
    public interface ICsvMessage
    {
        /// <summary>
        /// 真理値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetBooleanConvertError(ICsvItemContext context);

        /// <summary>
        /// <see cref="DateTime"/>変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetDateTimeConvertError(ICsvItemContext context);

        /// <summary>
        /// 不正な形式エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetInvalidFormatError(ICsvItemContext context);

        /// <summary>
        /// CSV行が不正な場合のメッセージを返します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <returns>メッセージ。</returns>
        string GetInvalidLineError(int lineNumber);

        /// <summary>
        /// CSVに項目が存在しない場合のメッセージを返します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <returns>メッセージ。</returns>
        string GetItemNotExistError(int lineNumber, string name);

        /// <summary>
        /// 最大桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="length">桁数。</param>
        /// <returns>メッセージ。</returns>
        string GetMaxLengthError(ICsvItemContext context, int length);

        /// <summary>
        /// 数値最大値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="maxValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        string GetNumberMaxValueError(ICsvItemContext context, decimal maxValue);

        /// <summary>
        /// 数値最小値エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <returns>メッセージ。</returns>
        string GetNumberMinValueError(ICsvItemContext context, decimal minValue);

        /// <summary>
        /// 数字のみエラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetNumberOnlyError(ICsvItemContext context);

        /// <summary>
        /// 数値範囲エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="minValue">最小値。</param>
        /// <param name="maxValue">最大値。</param>
        /// <returns>メッセージ。</returns>
        string GetNumberOutOfRangeError(ICsvItemContext context, decimal minValue, decimal maxValue);

        /// <summary>
        /// 数値変換エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetNumericConvertError(ICsvItemContext context);

        /// <summary>
        /// 精度と小数部桁数エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        /// <returns>メッセージ。</returns>
        string GetPrecisionAndScaleError(ICsvItemContext context, int precision, int scale);

        /// <summary>
        /// 精度エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="precision">精度。</param>
        /// <returns>メッセージ。</returns>
        string GetPrecisionError(ICsvItemContext context, int precision);

        /// <summary>
        /// 必須エラー時のメッセージを返します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>メッセージ。</returns>
        string GetRequiredError(ICsvItemContext context);
    }
}