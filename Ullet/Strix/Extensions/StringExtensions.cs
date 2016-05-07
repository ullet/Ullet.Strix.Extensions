/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.Strix.Extensions
{
  /// <summary>
  /// String extension methods.
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithAnyOf(
      this string s, IEnumerable<string> prefixes)
    {
      return s.MatchesAnyOf((str, prefix) => str.StartsWith(prefix), prefixes);
    }

    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithAnyOf(this string s, params string[] prefixes)
    {
      return s.StartsWithOneOf((IEnumerable<string>)prefixes);
    }

    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithOneOf(
      this string s, IEnumerable<string> prefixes)
    {
      return s.StartsWithAnyOf(prefixes);
    }

    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithOneOf(this string s, params string[] prefixes)
    {
      return s.StartsWithAnyOf(prefixes);
    }

    /// <summary>
    /// Test if string contains any of the specified values. Match is case
    /// sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string contains at least one the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool ContainsAnyOf(this string s, IEnumerable<string> values)
    {
      return s.MatchesAnyOf((str, value) => str.Contains(value), values);
    }

    /// <summary>
    /// Test if string contains any of the specified values. Match is case
    /// sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string contains at least one the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool ContainsAnyOf(this string s, params string[] values)
    {
      return s.ContainsAnyOf((IEnumerable<string>) values);
    }

    /// <summary>
    /// Test if string matches any of the specified values using the specified
    /// predicate function. Match is case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicate">
    /// <![CDATA[Func<string, string, bool>]]> predicate function that performs
    /// the match test.
    /// </param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string matches at least one the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAnyOf<T>(
      this string s,
      Func<string, T, bool> predicate,
      IEnumerable<T> values)
    {
      s = s ?? string.Empty;
      return (values ?? Enumerable.Empty<T>()).Any(v => predicate(s, v));
    }

    /// <summary>
    /// Test if string matches any of the specified values using the specified
    /// predicate function. Match is case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicate">
    /// <![CDATA[Func<string, string, bool>]]> predicate function that performs
    /// the match test.
    /// </param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string matches at least one the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAnyOf<T>(
      this string s,
      Func<string, T, bool> predicate,
      params T[] values)
    {
      return s.MatchesAnyOf(predicate, (IEnumerable<T>) values);
    }
  }
}
