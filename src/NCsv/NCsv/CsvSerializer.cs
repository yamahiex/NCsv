using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NCsv
{
    /// <summary>
    /// オブジェクトをCSVシリアル化します。
    /// </summary>
    /// <typeparam name="T">CSVシリアル化する型です。</typeparam>
    public class CsvSerializer<T>
    {
        /// <summary>
        /// <see cref="CsvColumns{T}"/>です。
        /// </summary>
        private readonly CsvColumns<T> columns = new CsvColumns<T>();

        /// <summary>
        /// ヘッダがあるかどうかを取得または設定します。
        /// </summary>
        public bool HasHeader { get; set; } = false;

        /// <summary>
        /// 非同期的に指定した<paramref name="objects"/>をCSVシリアル化します。
        /// </summary>
        /// <param name="objects">オブジェクト。</param>
        /// <returns>CSV文字列。</returns>
        public async Task<string> SerializeAsync(params T[] objects)
        {
            return await Task.Run(() => Serialize(objects));
        }

        /// <summary>
        /// 指定した<paramref name="objects"/>をCSVシリアル化します。
        /// </summary>
        /// <param name="objects">オブジェクト。</param>
        /// <returns>CSV文字列。</returns>
        public string Serialize(params T[] objects)
        {
            var writer = new StringWriter();
            Serialize(writer, objects);

            return writer.ToString();
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="objects"/>をCSVシリアル化します。
        /// </summary>
        /// <param name="writer">CSVを書き込む<see cref="TextWriter"/>。</param>
        /// <param name="objects">オブジェクト。</param>
        public async Task SerializeAsync(TextWriter writer, params T[] objects)
        {
            await Task.Run(() => Serialize(writer, objects));
        }

        /// <summary>
        /// 指定した<paramref name="objects"/>をCSVシリアル化します。
        /// </summary>
        /// <param name="writer">CSVを書き込む<see cref="TextWriter"/>。</param>
        /// <param name="objects">オブジェクト。</param>
        public void Serialize(TextWriter writer, params T[] objects)
        {
            if (this.HasHeader)
            {
                writer.WriteLine(this.columns.CreateHeader());
            }

            foreach (var row in objects)
            {
                writer.WriteLine(this.columns.CreateCsvRow(row));
            }
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="csv"/>を逆シリアル化します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvDeserializeException">逆シリアル化時にエラーが発生した場合にスローされます。</exception>
        public async Task<List<T>> DeserializeAsync(string csv)
        {
            return await Task.Run(() => Deserialize(csv));
        }

        /// <summary>
        /// 指定した<paramref name="csv"/>を逆シリアル化します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvDeserializeException">逆シリアル化時にエラーが発生した場合にスローされます。</exception>
        public List<T> Deserialize(string csv)
        {
            return Deserialize(new StringReader(csv));
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="reader"/>からCSVを読み取り逆シリアル化します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvDeserializeException">逆シリアル化時にエラーが発生した場合にスローされます。</exception>
        public async Task<List<T>> DeserializeAsync(TextReader reader)
        {
            return await Task.Run(() => Deserialize(reader));
        }

        /// <summary>
        /// 指定した<paramref name="reader"/>からCSVを読み取り逆シリアル化します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvDeserializeException">逆シリアル化時にエラーが発生した場合にスローされます。</exception>
        public List<T> Deserialize(TextReader reader)
        {
            var result = new List<T>();

            var parser = new CsvTextFieldParser(reader);
            var lineNumber = 0;

            while (!parser.EndOfData)
            {
                lineNumber++;

                try
                {
                    var items = parser.ReadFields();

                    if (this.HasHeader && lineNumber == 1)
                    {
                        continue;
                    }

                    result.Add(this.columns.CreateObject(items));
                }
                catch (Exception ex) when (ex is CsvMalformedLineException || ex is CsvParseException)
                {
                    throw new CsvDeserializeException(CsvConfig.Current.Message.GetInvalidLineError(lineNumber), lineNumber, ex);
                }
            }

            return result;
        }
    }
}
