using NCsv.Converters;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvColumn"/>を構築します。
    /// </summary>
    /// <typeparam name="T">対象の型です。</typeparam>
    public class CsvColumnBuilder<T>
    {
        /// <summary>
        /// インデックスです。
        /// </summary>
        private readonly int index = 0;

        /// <summary>
        /// プロパティ名です。
        /// </summary>
        private readonly string propertyName = string.Empty;

        /// <summary>
        /// 名前です。
        /// </summary>
        private string name = null;

        /// <summary>
        /// <see cref="CsvFormatAttribute"/>です。
        /// </summary>
        private CsvFormatAttribute format = null;

        /// <summary>
        /// <see cref="CsvValidationAttribute"/>のリストです。
        /// </summary>
        private readonly List<CsvValidationAttribute> validations = new List<CsvValidationAttribute>();

        /// <summary>
        /// <see cref="CsvConverter"/>です。
        /// </summary>
        private CsvConverter converter = null;

        /// <summary>
        /// <see cref="CsvColumnBuilder{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="propertyName">プロパティ名。</param>
        internal CsvColumnBuilder(int index, string propertyName)
        {
            this.index = index;
            this.propertyName = propertyName;
        }

        /// <summary>
        /// このインスタンスの値を<see cref="CsvColumn"/>に変換します。
        /// </summary>
        /// <returns><see cref="CsvColumn"/>。</returns>
        internal CsvColumn ToCsvColumn()
        {
            Validate();

            var property = typeof(T).GetProperty(this.propertyName);

            if (property == null)
            {
                throw new InvalidOperationException($"The property name {this.propertyName} is invalid.");
            }

            var attribute = new CsvColumnAttribute(this.index) { Name = this.name };
            var csvProperty = new CsvProperty(property, CreateAttributesDictionary());
            var converter = GetConverter(property);
            return new CsvColumn(attribute, csvProperty, converter);
        }

        /// <summary>
        /// 名前を設定します。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// 書式を設定します。
        /// </summary>
        /// <param name="format">書式。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> Format(string format)
        {
            this.format = new CsvFormatAttribute(format);
            return this;
        }

        /// <summary>
        /// <see cref="CsvConverter"/>を設定します。
        /// </summary>
        /// <param name="converter"><see cref="CsvConverter"/>。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> Converter(CsvConverter converter)
        {
            this.converter = converter;
            return this;
        }

        /// <summary>
        /// <see cref="CsvValidationAttribute"/>を設定します。
        /// </summary>
        /// <param name="validation"><see cref="CsvValidationAttribute"/>。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> AddValidation(CsvValidationAttribute validation)
        {
            this.validations.Add(validation);
            return this;
        }

        /// <summary>
        /// <see cref="Attribute"/>のディクショナリを作成します。
        /// </summary>
        /// <returns><see cref="Attribute"/>のディクショナリ。</returns>
        private Dictionary<Type, List<Attribute>> CreateAttributesDictionary()
        {
            return new Dictionary<Type, List<Attribute>>()
            {
                { typeof(CsvFormatAttribute), new List<Attribute>() { this.format } },
                { typeof(CsvValidationAttribute), new List<Attribute>(this.validations) },
            };
        }

        /// <summary>
        /// <see cref="CsvConverter"/>を返します。<see cref="CsvConverter"/>が存在しない場合は例外をスローします。
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/>。</param>
        /// <returns><see cref="CsvConverter"/>。</returns>
        private CsvConverter GetConverter(PropertyInfo property)
        {
            if (this.converter != null)
            {
                return this.converter;
            }

            var converter = DefaultConverters.GetOrNull(property.PropertyType);

            if (converter != null)
            {
                return converter;
            }

            throw new InvalidOperationException("I couldn't find a converter for the property type.");
        }

        /// <summary>
        /// 検証します。検証に失敗した場合は例外をスローします。
        /// </summary>
        private void Validate()
        {
            if (HasValidationMultipleError())
            {
                throw new InvalidOperationException("Multiple CsvValidationAttributes are set with AllowMultiple set to false.");
            }
        }

        /// <summary>
        /// 複数設定できないCsvValidationAttributesが複数設定されているかどうかを返します。
        /// </summary>
        /// <returns>複数設定されている場合にtrue。</returns>
        private bool HasValidationMultipleError()
        {
            foreach (var v in this.validations)
            {
                var a = v.GetType().GetCustomAttribute<AttributeUsageAttribute>();

                if (a == null)
                {
                    continue;
                }

                if (a.AllowMultiple)
                {
                    continue;
                }

                if (this.validations.Where(x => x != v && x.GetType() == v.GetType()).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
