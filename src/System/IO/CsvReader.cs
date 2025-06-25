namespace System.IO;

/// <summary>
/// Provides lightweight CSV parsing functionality from a <see cref="TextReader"/> source.
/// Supports optional headers and column-based access.
/// </summary>
/// <seealso cref="TextReader"/>
[TypeImpl(TypeImplFlags.Disposable)]
public sealed partial class CsvReader : IDisposable
{
	/// <summary>
	/// Indicates the backing reader object.
	/// </summary>
	[DisposableMember]
	private readonly TextReader _reader;

	/// <summary>
	/// Indicates the header map.
	/// </summary>
	private readonly Dictionary<string, int>? _headerMap;

	/// <summary>
	/// Indicates the expected field count.
	/// </summary>
	private int? _expectedFieldCount;


	/// <inheritdoc cref="CsvReader(string, bool, IEqualityComparer{string}?)"/>
	public CsvReader(string filePath) : this(new StreamReader(filePath), false, null)
	{
	}

	/// <inheritdoc cref="CsvReader(string, bool, IEqualityComparer{string}?)"/>
	public CsvReader(string filePath, bool hasHeader) : this(new StreamReader(filePath), hasHeader, null)
	{
	}

	/// <summary>
	/// Initializes a <see cref="CsvReader"/> instance.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="hasHeader">Indicates whether the file has header line.</param>
	/// <param name="comparer">The comparer that will be used for comparison on header column names.</param>
	public CsvReader(string filePath, bool hasHeader, IEqualityComparer<string>? comparer) :
		this(new StreamReader(filePath), hasHeader, comparer)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CsvReader"/> class.
	/// </summary>
	/// <param name="reader">The <see cref="TextReader"/> to read CSV data from.</param>
	/// <param name="hasHeader">Indicates whether the CSV includes a header row.</param>
	/// <param name="comparer">Optional comparer for header name matching.</param>
	public CsvReader(TextReader reader, bool hasHeader, IEqualityComparer<string>? comparer)
	{
		_reader = reader ?? throw new ArgumentNullException(nameof(reader));
		HasHeader = hasHeader;
		Comparer = comparer ?? StringComparer.OrdinalIgnoreCase;

		if (HasHeader)
		{
			var headerLine = _reader.ReadLine() ?? throw new InvalidOperationException("CSV file has no header line.");
			var headers = ParseCsvLine(headerLine);
			_headerMap = new Dictionary<string, int>(Comparer);
			for (var i = 0; i < headers.Count; i++)
			{
				_headerMap[headers[i]] = i;
			}
			_expectedFieldCount = headers.Count;
		}
	}


	/// <summary>
	/// Indicates whether the file has header line.
	/// </summary>
	[MemberNotNullWhen(true, nameof(_headerMap))]
	public bool HasHeader { get; }

	/// <summary>
	/// Indicates the header column names.
	/// </summary>
	public ReadOnlySpan<KeyValuePair<int, string>> HeaderColumnNames
	{
		get
		{
			if (_headerMap is null)
			{
				throw new NotSupportedException("The reader should enable for header line option.");
			}

			var result = new KeyValuePair<int, string>[_headerMap.Count];
			var i = 0;
			foreach (var (columnName, index) in _headerMap)
			{
				result[i++] = KeyValuePair.Create(index, columnName);
			}
			return result;
		}
	}

	/// <summary>
	/// Indicates the header matching comparer.
	/// </summary>
	public IEqualityComparer<string> Comparer { get; }


	/// <summary>
	/// Reads the next line from the CSV and returns the value of a specified column.
	/// </summary>
	/// <param name="columnName">The name of the column to retrieve.</param>
	/// <returns>The field value, or null if not found or end of input.</returns>
	/// <exception cref="InvalidOperationException">Thrown if headers are not enabled.</exception>
	/// <exception cref="ArgumentException">Thrown if the specified column is not found.</exception>
	public string? ReadColumn(string columnName)
	{
		if (_headerMap is null)
		{
			throw new InvalidOperationException("CSV reader was not initialized with header support.");
		}

		if (!_headerMap.TryGetValue(columnName, out var index))
		{
			throw new ArgumentException($"Column '{columnName}' not found in header.");
		}

		var fields = ReadLine();
		return fields is not null && index < fields.Length ? fields[index] : null;
	}

	/// <summary>
	/// Reads the next line from the CSV and parses it into fields.
	/// </summary>
	/// <returns>List of string fields, or null if end of input.</returns>
	public string[]? ReadLine()
	{
		if (_reader.ReadLine() is not { } line)
		{
			return null;
		}

		var fields = ParseCsvLine(line);
		_expectedFieldCount ??= fields.Count;
		return fields?.ToArray();
	}


	/// <summary>
	/// Validates a CSV file to ensure consistent column count across all rows.
	/// </summary>
	/// <param name="reader">The text reader to validate.</param>
	/// <param name="hasHeader">Indicates whether the CSV includes a header row.</param>
	/// <param name="errorLine">The offending line index if validation fails.</param>
	/// <param name="comparer">Optional comparer for header name matching.</param>
	/// <returns>True if valid; otherwise, false.</returns>
	public static bool Validate(TextReader reader, bool hasHeader, out int errorLine, IEqualityComparer<string>? comparer = null)
	{
		using var csv = new CsvReader(reader, hasHeader, comparer);
		var expected = csv._expectedFieldCount;

		var i = 0;
		string[]? fields;
		while ((fields = csv.ReadLine()) is not null)
		{
			if (fields.Length != expected)
			{
				errorLine = i;
				return false;
			}
			i++;
		}

		errorLine = -1;
		return true;
	}

	/// <summary>
	/// Parses a single CSV line into individual fields, handling quoted values.
	/// </summary>
	/// <param name="line">The CSV line to parse.</param>
	/// <returns>List of parsed string fields.</returns>
	private static List<string> ParseCsvLine(string line)
	{
		var (result, field, inQuotes) = (new List<string>(), new StringBuilder(), false);
		for (var i = 0; i < line.Length; i++)
		{
			var c = line[i];

			if (inQuotes)
			{
				if (c == '"')
				{
					if (i + 1 < line.Length && line[i + 1] == '"')
					{
						field.Append('"');
						i++; // Skip the escaped quote.
					}
					else
					{
						inQuotes = false;
					}
				}
				else
				{
					field.Append(c);
				}
			}
			else
			{
				if (c == ',')
				{
					result.Add(field.ToString());
					field.Clear();
				}
				else if (c == '"')
				{
					inQuotes = true;
				}
				else
				{
					field.Append(c);
				}
			}
		}

		result.Add(field.ToString()); // Add the last field.
		return result;
	}
}
