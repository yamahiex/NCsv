using System;

namespace NCsv
{
    /// <summary>
    /// CSVカラム属性です。
    /// この属性が付与されているプロパティを対象にします。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvColumnAttribute : Attribute
    {
        /// <summary>
        /// インデックスを取得します。
        /// </summary>
        internal int Index { get; private set; }

        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// <see cref="CsvColumnAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        public CsvColumnAttribute(int index)
        {
            this.Index = index;
        }
    }
}
