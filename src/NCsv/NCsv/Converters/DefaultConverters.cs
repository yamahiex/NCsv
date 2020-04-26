using System;
using System.Collections.Generic;

namespace NCsv.Converters
{
    /// <summary>
    /// 既定のコンバーターです。
    /// </summary>
    internal static class DefaultConverters
    {
        /// <summary>
        /// <see cref="CsvConverter"/>のディクショナリです。
        /// </summary>
        private static readonly Dictionary<Type, CsvConverter> converters = CreateConverters();

        /// <summary>
        /// 指定した<paramref name="type"/>の既定<see cref="CsvConverter"/>を返します。
        /// 存在しない場合はNullを返します。
        /// </summary>
        /// <param name="type"><see cref="Type"/>。</param>
        /// <returns><see cref="CsvConverter"/></returns>
        public static CsvConverter? GetOrNull(Type type)
        {
            if (converters.ContainsKey(type))
            {
                return converters[type];
            }

            return null;
        }

        /// <summary>
        /// <see cref="CsvConverter"/>のディクショナリを作成します。
        /// </summary>
        /// <returns><see cref="CsvConverter"/>のディクショナリ。</returns>
        private static Dictionary<Type, CsvConverter> CreateConverters()
        {
            return new Dictionary<Type, CsvConverter>()
            {
                { typeof(string), new StringConverter() },
                { typeof(DateTime), new DateTimeConverter() },
                { typeof(decimal), new DecimalConverter() },
                { typeof(int), new IntConverter() },
                { typeof(bool), new BoolConverter() },
                { typeof(DateTime?), new NullableDateTimeConverter() },
                { typeof(decimal?), new NullableDecimalConverter() },
                { typeof(int?), new NullableIntConverter() },
                { typeof(double), new DoubleConverter() },
                { typeof(double?), new NullableDoubleConverter() },
            };
        }
    }
}
