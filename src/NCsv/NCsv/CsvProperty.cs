using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NCsv
{
    /// <summary>
    /// CSVプロパティです。
    /// </summary>
    public class CsvProperty
    {
        /// <summary>
        /// <see cref="PropertyInfo"/>です。
        /// </summary>
        private readonly PropertyInfo property;

        /// <summary>
        /// 属性をキャッシュします。
        /// </summary>
        private readonly Dictionary<Type, List<Attribute>> attributeCache = new Dictionary<Type, List<Attribute>>();

        /// <summary>
        /// 名前を取得します。
        /// </summary>
        public string Name => this.property.Name;

        /// <summary>
        /// <see cref="CsvProperty"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/>。</param>
        public CsvProperty(PropertyInfo property)
        {
            this.property = property;
        }

        /// <summary>
        /// 指定した型のカスタム属性を取得します。
        /// </summary>
        /// <typeparam name="T">検索対象とするカスタム属性の型。</typeparam>
        /// <returns>指定した型のカスタム属性。</returns>
        public T GetCustomAttribute<T>() where T : Attribute
        {
            List<Attribute> attributes;

            if (this.attributeCache.ContainsKey(typeof(T)))
            {
                attributes = this.attributeCache[typeof(T)];
            }
            else
            {
                attributes = new List<Attribute>
                {
                    this.property.GetCustomAttribute<T>()
                };

                this.attributeCache.Add(typeof(T), attributes);
            }

            return (T)attributes.FirstOrDefault();
        }

        /// <summary>
        /// 指定した型のカスタム属性を取得します。
        /// </summary>
        /// <typeparam name="T">検索対象とするカスタム属性の型。</typeparam>
        /// <returns>指定した型のカスタム属性。</returns>
        public IEnumerable<T> GetCustomAttributes<T>() where T : Attribute
        {
            List<Attribute> attributes;

            if (this.attributeCache.ContainsKey(typeof(T)))
            {
                attributes = this.attributeCache[typeof(T)];
            }
            else
            {
                attributes = new List<Attribute>();
                attributes.AddRange(this.property.GetCustomAttributes<T>());

                this.attributeCache.Add(typeof(T), attributes);
            }

            foreach (var a in attributes)
            {
                yield return (T)a;
            }
        }

        /// <summary>
        /// 指定したオブジェクトのプロパティ値を設定します。
        /// </summary>
        /// <param name="obj">プロパティ値を設定するオブジェクト。</param>
        /// <param name="value">プロパティ値。</param>
        internal void SetValue(object obj, object value)
        {
            this.property.SetValue(obj, value);
        }

        /// <summary>
        /// プロパティ値を返します。
        /// </summary>
        /// <param name="obj">プロパティ値を取得するオブジェクト。</param>
        /// <returns>プロパティ値。</returns>
        internal object GetValue(object obj)
        {
            return this.property.GetValue(obj);
        }

        /// <summary>
        /// CSV項目を検証します。
        /// </summary>
        /// <param name="context"><see cref="CsvValidationContext"/>。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns></returns>
        internal bool Validate(CsvValidationContext context, out string errorMessage)
        {
            errorMessage = string.Empty;

            foreach (var v in GetCustomAttributes<CsvValidationAttribute>())
            {
                if (!v.Validate(context, out errorMessage))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
