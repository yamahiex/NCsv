﻿namespace NCsv
{
    /// <summary>
    /// コンフィグです。
    /// </summary>
    public class CsvConfig
    {
        /// <summary>
        /// <see cref="CsvConfig"/>のインスタンスを取得します。
        /// </summary>
        public static CsvConfig Current { get; } = new CsvConfig();

        /// <summary>
        /// <see cref="CsvMessage"/>を取得または設定します。
        /// </summary>
        public CsvMessage Message { get; set; } = new CsvMessage();

        /// <summary>
        /// <see cref="CsvConfig"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        private CsvConfig()
        {
        }
    }
}