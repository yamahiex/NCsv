using NotVisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;

namespace NCsv
{
    /// <summary>
    /// オブジェクトからCSVへのシリアル化およびCSVからオブジェクトへの逆シリアル化を行います。 
    /// <para>
    /// Usage see https://github.com/yamahix/NCsv
    /// </para>
    /// </summary>
    /// <typeparam name="T">シリアル化する型です。</typeparam>
    public class CsvSerializer<T> where T : new()
    {
        /// <summary>
        /// <see cref="CsvParser{T}"/>です。
        /// </summary>
        private readonly CsvParser<T> csvParser = null;

        /// <summary>
        /// ヘッダがあるかどうかを取得または設定します。
        /// </summary>
        public bool HasHeader { get; set; } = false;

        /// <summary>
        /// 区切り文字を取得または設定します。
        /// </summary>
        public string Delimiter { get; set; } = ",";

        /// <summary>
        /// <see cref="CsvSerializer{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CsvSerializer()
        {
            var builder = CsvParserBuilder<T>.FromType();
            this.csvParser = builder.ToCsvParser();
        }

        /// <summary>
        /// <see cref="CsvSerializer{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="csvParser"><see cref="CsvParser{T}"/></param>
        internal CsvSerializer(CsvParser<T> csvParser)
        {
            this.csvParser = csvParser;
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
        /// 指定した<paramref name="objects"/>をシリアル化し、生成されたCSVを<paramref name="writer"/>に書き込みます。
        /// </summary>
        /// <param name="writer">CSVを書き込む<see cref="TextWriter"/>。</param>
        /// <param name="objects">オブジェクト。</param>
        public void Serialize(TextWriter writer, params T[] objects)
        {
            if (this.HasHeader)
            {
                writer.WriteLine(this.csvParser.CreateHeader(this.Delimiter));
            }

            foreach (var row in objects)
            {
                writer.WriteLine(this.csvParser.CreateCsvLine(row, this.Delimiter));
            }
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
        /// 指定した<paramref name="reader"/>に格納されているCSVを逆シリアル化します。
        /// </summary>
        /// <param name="reader">CSVを読み取る<see cref="TextReader"/>。</param>
        /// <returns>オブジェクト。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        /// <exception cref="CsvValidationException">CSVの検証に失敗しました。</exception>
        public List<T> Deserialize(TextReader reader)
        {
            var result = new List<T>();
            var lineNumber = 0L;
            var fieldParser = CreateCsvTextFieldParser(reader);

            while (!fieldParser.EndOfData)
            {
                lineNumber++;

                try
                {
                    var csvItems = new CsvItems(fieldParser.ReadFields(), lineNumber);

                    if (this.HasHeader && lineNumber == 1)
                    {
                        continue;
                    }

                    result.Add(this.csvParser.CreateObject(csvItems));
                }
                catch (CsvMalformedLineException ex)
                {
                    throw new CsvParseException(ex.Message, ex.LineNumber, fieldParser.ErrorLine, ex);
                }
            }

            return result;
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
        /// 指定した<paramref name="parser"/>で解析したCSVを検証して全てのエラーを返します。
        /// </summary>
        /// <param name="parser">CSVを解析する<see cref="CsvTextFieldParser"/>。</param>
        /// <returns>検証で発生したエラー。</returns>
        /// <exception cref="CsvParseException">CSVの解析に失敗しました。</exception>
        public List<CsvErrorItem> GetErrors(CsvTextFieldParser parser)
        {
            var result = new List<CsvErrorItem>();
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

                    result.AddRange(this.csvParser.GetErrors(csvItems));
                }
                catch (CsvMalformedLineException ex)
                {
                    throw new CsvParseException(ex.Message, ex.LineNumber, parser.ErrorLine, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// <see cref="CsvTextFieldParser"/>を作成します。
        /// </summary>
        /// <param name="reader"><see cref="TextReader"/>。</param>
        /// <returns><see cref="CsvTextFieldParser"/>。</returns>
        private CsvTextFieldParser CreateCsvTextFieldParser(TextReader reader)
        {
            return new CsvTextFieldParser(reader)
            {
                Delimiters = new string[] { this.Delimiter },
            };
        }
    }
}
