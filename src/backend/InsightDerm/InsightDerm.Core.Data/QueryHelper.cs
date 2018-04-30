using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace InsightDerm.Core.Data
{
    public static class QueryHelper
    {
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> query, string filter)
        {
            if (filter == null || string.IsNullOrEmpty(filter)) return query;
            
            var filterTree = JArray.Parse(filter);
            var filterQuery = ReadExpression(filterTree);
            var q = query.Where(filterQuery);
            return q;
        }

        private static string ReadExpression(JArray array)
        {
            var query = string.Empty;
            if (array[0].Type == JTokenType.String && array.Count == 3)
            {
                query += FilterQuery(array[0].ToString(),
                                        array[1].ToString(),
                                        array[2].ToString());
            }
            else
            {
                foreach (var t in array)
                {
                    switch (t.Type)
                    {
                        case JTokenType.String:
                            query += t;
                            break;
                        case JTokenType.Array when t.HasValues && t[0].Type == JTokenType.String:
                            query += FilterQuery(t[0].ToString(),
                                         t[1].ToString(),
                                         t[2].ToString()) + " ";
                            break;
                        case JTokenType.Array:
                            query += ReadExpression((JArray)t);
                            break;
                    }
                }
            }

            return query;
        }

        private static string FilterQuery(string columnName, string clause, string value)
        {
            switch (clause)
            {
                case "=":                    
                    return $"{columnName} == {value}";
                case "eq":
                    value = string.Format("\"{0}\"", value);
                    return $"{columnName} == {value}";
                case "contains":
                    return string.Format(columnName + ".Contains(\"{0}\")", value);
                case "<>":
                    if (int.TryParse(value, out _))
                    {
                        return string.Format("{0} <> {1}", columnName, value);
                    }
                    return string.Format("!{0}.StartsWith(\"{1}\")", columnName, value);
                case ">=":                    
                    var d = Convert.ToDateTime(value);
                    return $"{columnName} >= DateTime({d.Year},{d.Month},{d.Day})";
                case "<":                    
                    var d2 = Convert.ToDateTime(value);
                    return $"{columnName} < DateTime({d2.Year},{d2.Month},{d2.Day})";
                default:
                    return string.Empty;
            }
        }
    }
}