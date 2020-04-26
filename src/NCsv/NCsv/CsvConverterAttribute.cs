using System;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvConverter"/>を付与するための属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvConverterAttribute : Attribute
    {
        private readonly Type converterType;

        /// <summary>
        /// <see cref="CsvConverterAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="converterType"></param>
        public CsvConverterAttribute(Type converterType)
        {
            this.converterType = converterType;
        }

        /// <summary>
        /// <see cref="CsvConverter"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvConverter"/>。</returns>
        internal CsvConverter CreateConverter()
        {
            if (!(Activator.CreateInstance(this.converterType) is CsvConverter converter))
            {
                throw new InvalidOperationException("CsvConverter is not inherited; please pass a Type that inherits CsvConverter.");
            }

            return converter;
        }
    }
}
