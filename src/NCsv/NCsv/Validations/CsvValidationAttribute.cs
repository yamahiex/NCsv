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
        /// <param name="context"><see cref="CsvValidationContext"/>。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>検証成功の場合にtrue。</returns>
        public abstract bool Validate(CsvValidationContext context, out string errorMessage);
    }
}
