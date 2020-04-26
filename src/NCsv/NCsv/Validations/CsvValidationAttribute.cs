using System;

namespace NCsv.Validations
{
    /// <summary>
    /// 検証属性です。
    /// </summary>
    /// <remarks>
    /// 当クラスを継承した属性をプロパティに付与することによって検証可能にします。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class CsvValidationAttribute : Attribute
    {
        /// <summary>
        /// 検証します。
        /// </summary>
        /// <param name="value">検証する値。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>検証成功の場合にtrue。</returns>
        public abstract bool Validate(string value, string name, out string errorMessage);
    }
}
