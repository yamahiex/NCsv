using NotVisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NCsv
{
    /// <summary>
    /// オブジェクトからCSVへのシリアル化およびCSVからオブジェクトへの逆シリアル化を行います。 
    /// <para>
    /// Usage see https://github.com/yamahix/NCsv
    /// </para>
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
        /// 非同期的に指定した<paramref name="objects"/>をシリアル化し、生成されたCSVを返します。
        /// </summary>
        /// <param name="objects">オブジェクト。</param>
        /// <returns>CSV文字列。</returns>
        public Task<string> SerializeAsync(params T[] objects)
        {
            return Task.Run(() => Serialize(objects));
        }

        /// <summary>
        /// 指定した<paramref name="objects"/>をシリアル化し、生成されたCSVを返します。
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
        /// 非同期的に指定した<paramref name="objects"/>をシリアル化し、生成されたCSVを<paramref name="writer"/>に書き込みます。
        /// </summary>
        /// <param name="writer">CSVを書き込む<see cref="TextWriter"/>。</param>
        /// <param name="objects">オブジェクト。</param>
        public Task SerializeAsync(TextWriter writer, params T[] objects)
        {
            return Task.Run(() => Serialize(writer, objects));
        }

        /// <summary>
        /// 指定した<paramref name="objects"/>をシリアル化し、生成されたCSVを<paramref name="writer"/>に書き込みます。
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
                writer.WriteLine(this.columns.CreateCsvLine(row));
            }
        }

        /// <summary>
        /// 非同期的にCSVを逆シリアル化します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public Task<List<T>> DeserializeAsync(string csv)
        {
            return Task.Run(() => Deserialize(csv));
        }

        /// <summary>
        /// CSVを逆シリアル化します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public List<T> Deserialize(string csv)
        {
            return Deserialize(new StringReader(csv));
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="reader"/>に格納されているCSVを逆シリアル化します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public Task<List<T>> DeserializeAsync(TextReader reader)
        {
            return Task.Run(() => Deserialize(reader));
        }

        /// <summary>
        /// 指定した<paramref name="reader"/>に格納されているCSVを逆シリアル化します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public List<T> Deserialize(TextReader reader)
        {
            return Deserialize(new CsvTextFieldParser(reader));
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="parser"/>で解析したCSVを逆シリアル化します。
        /// </summary>
        /// <param name="parser">CSVを解析する<see cref="CsvTextFieldParser"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public Task<List<T>> DeserializeAsync(CsvTextFieldParser parser)
        {
            return Task.Run(() => Deserialize(parser));
        }

        /// <summary>
        /// 指定した<paramref name="parser"/>で解析したCSVを逆シリアル化します。
        /// </summary>
        /// <param name="parser">CSVを解析する<see cref="CsvTextFieldParser"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public List<T> Deserialize(CsvTextFieldParser parser)
        {
            var result = new List<T>();
            var lineNumber = 0L;

            while (!parser.EndOfData)
            {
                lineNumber++;

                try
                {
                    var csvItems = new CsvItems(parser.ReadFields(), lineNumber);

                    if (this.HasHeader && lineNumber == 1)
                    {
                        continue;
                    }

                    result.Add(this.columns.CreateObject(csvItems));
                }
                catch (CsvMalformedLineException ex)
                {
                    throw new CsvParseException(ex.Message, ex.LineNumber, parser.ErrorLine, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// 非同期的にCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public Task<List<CsvErrorItem>> GetErrorsAsync(string csv)
        {
            return Task.Run(() => GetErrors(csv));
        }

        /// <summary>
        /// CSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="csv">CSV文字列。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public List<CsvErrorItem> GetErrors(string csv)
        {
            return GetErrors(new StringReader(csv));
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="reader"/>に格納されているCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public Task<List<CsvErrorItem>> GetErrorsAsync(TextReader reader)
        {
            return Task.Run(() => GetErrors(reader));
        }

        /// <summary>
        /// 指定した<paramref name="reader"/>に格納されているCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public List<CsvErrorItem> GetErrors(TextReader reader)
        {
            return GetErrors(new CsvTextFieldParser(reader));
        }

        /// <summary>
        /// 非同期的に指定した<paramref name="parser"/>で解析したCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="parser">CSVを解析する<see cref="CsvTextFieldParser"/>。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public Task<List<CsvErrorItem>> GetErrorsAsync(CsvTextFieldParser parser)
        {
            return Task.Run(() => GetErrors(parser));
        }

        /// <summary>
        /// 指定した<paramref name="parser"/>で解析したCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="parser">CSVを解析する<see cref="CsvTextFieldParser"/>。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public List<CsvErrorItem> GetErrors(CsvTextFieldParser parser)
        {
            var result = new List<CsvErrorItem>();
            var lineNumber = 0;

            while (!parser.EndOfData)
            {
                lineNumber++;

                try
                {
                    var csvItems = new CsvItems(parser.ReadFields(), lineNumber);

                    if (this.HasHeader && lineNumber == 1)
                    {
                        continue;
                    }

                    result.AddRange(this.columns.GetErrors(csvItems));
                }
                catch (CsvMalformedLineException ex)
                {
                    throw new CsvParseException(ex.Message, ex.LineNumber, parser.ErrorLine, ex);
                }
            }

            return result;
        }
    }
}
