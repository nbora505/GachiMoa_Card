// Scripts/Game/CSV.cs
using System.Collections.Generic;
using System.Text;

public static class CSV
{
    // RFC4180 간단 파서(따옴표, 이스케이프, 콤마/개행 처리)
    public static List<string[]> Parse(string text)
    {
        var rows = new List<string[]>();
        var row = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (inQuotes)
            {
                if (c == '"')
                {
                    bool isEscaped = i + 1 < text.Length && text[i + 1] == '"';
                    if (isEscaped) { sb.Append('"'); i++; }
                    else inQuotes = false;
                }
                else sb.Append(c);
            }
            else
            {
                if (c == '"') inQuotes = true;
                else if (c == ',') { row.Add(sb.ToString()); sb.Clear(); }
                else if (c == '\n')
                {
                    // CRLF 또는 LF
                    row.Add(sb.ToString()); sb.Clear();
                    if (row.Count > 0) rows.Add(row.ToArray());
                    row.Clear();
                }
                else if (c == '\r') { /* skip, handle on \n */ }
                else sb.Append(c);
            }
        }
        // 마지막 셀/행
        row.Add(sb.ToString());
        if (row.Count > 1 || (row.Count == 1 && row[0].Length > 0))
            rows.Add(row.ToArray());

        return rows;
    }
}
