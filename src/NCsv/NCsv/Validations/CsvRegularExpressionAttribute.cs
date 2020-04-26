using System;
using System.Text.RegularExpressions;

namespace NCsv.Validations
{
    /// <summary>
    /// 正規表現を使用した検証属性です。
    /// 指定した正規表現に一致していない場合はエラーにします。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvRegularExpressionAttribute : CsvValidationAttribute
    {
        /// <summary>
        /// 正規表現を取得または設定します。
        /// </summary>
        protected string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="CsvRegularExpressionAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected CsvRegularExpressionAttribute()
        {
        }

        /// <summary>
        /// <see cref="CsvRegularExpressionAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="pattern">この正規表現に一致していない場合はエラーにします。</param>
        public CsvRegularExpressionAttribute(string pattern)
        {
            this.Pattern = pattern;
        }

        /// <inheritdoc/>
        public override bool Validate(string value, string name, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (!Regex.IsMatch(value, this.Pattern))
            {
                errorMessage = GetErrorMessage(name);
                return false;
            }

            return true;
        }

        /// <summary>
        /// エラーメッセージを返します。
        /// </summary>
        /// <param name="name">列名。</param>
        /// <returns>エラーメッセージ。</returns>
        protected virtual string GetErrorMessage(string name)
        {
            return NCsvConfig.Current.Message.GetInvalidFormatError(name);
        }
    }
}
