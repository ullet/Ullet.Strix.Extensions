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
    /// <c>true</c> if string starts with any one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithAny(
      this string s, IEnumerable<string> prefixes)
    {
      s = s ?? string.Empty;
      return s.MatchesAny((str, prefix) => str.StartsWith(prefix), prefixes);
    }

    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with any one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithAny(this string s, params string[] prefixes)
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
    /// <c>true</c> if string starts with any one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithOneOf(
      this string s, IEnumerable<string> prefixes)
    {
      return s.StartsWithAny(prefixes);
    }

    /// <summary>
    /// Test if string starts with any one of the specified prefixes. Match is
    /// case sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="prefixes">Prefixes to search for.</param>
    /// <returns>
    /// <c>true</c> if string starts with any one of the prefixes;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool StartsWithOneOf(this string s, params string[] prefixes)
    {
      return s.StartsWithAny(prefixes);
    }

    /// <summary>
    /// Test if string contains any of the specified values. Match is case
    /// sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string contains at least one of the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool ContainsAny(this string s, IEnumerable<string> values)
    {
      s = s ?? string.Empty;
      return s.MatchesAny((str, value) => str.Contains(value), values);
    }

    /// <summary>
    /// Test if string contains any of the specified values. Match is case
    /// sensitive.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string contains at least one of the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool ContainsAny(this string s, params string[] values)
    {
      return s.ContainsAny((IEnumerable<string>) values);
    }

    /// <summary>
    /// Test if string matches any of the specified values using the specified
    /// predicate function.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicate">
    /// <![CDATA[Func<string, string, bool>]]> predicate function that performs
    /// the match test.
    /// </param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string matches at least one of the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny<T>(
      this string s,
      Func<string, T, bool> predicate,
      IEnumerable<T> values)
    {
      return s.MatchesAny(
        (values ?? Enumerable.Empty<T>())
          .Select<T, Func<string, bool>>(v => (str => predicate(str, v))));
    }

    /// <summary>
    /// Test if string matches any of the specified values using the specified
    /// predicate function.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicate">
    /// <![CDATA[Func<string, string, bool>]]> predicate function that performs
    /// the match test.
    /// </param>
    /// <param name="values">Values to search for.</param>
    /// <returns>
    /// <c>true</c> if string matches at least one of the values;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny<T>(
      this string s,
      Func<string, T, bool> predicate,
      params T[] values)
    {
      return s.MatchesAny(predicate, (IEnumerable<T>) values);
    }

    /// <summary>
    /// Test if string matches any of the specified predicates.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicates">
    /// <![CDATA[Func<string, bool>]]> predicate functions that perform the
    /// match tests.
    /// </param>
    /// <returns>
    /// <c>true</c> if string matches using at least one of the predicates;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny(
      this string s, IEnumerable<Func<string, bool>> predicates)
    {
      return (predicates ?? Enumerable.Empty<Func<string, bool>>())
        .Any(p => (p ?? (_ => false))(s));
    }

    /// <summary>
    /// Test if string matches any of the specified predicates.
    /// </summary>
    /// <param name="s">String to search.</param>
    /// <param name="predicates">
    /// <![CDATA[Func<string, bool>]]> predicate functions that perform the
    /// match tests.
    /// </param>
    /// <returns>
    /// <c>true</c> if string matches using at least one of the predicates;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny(
      this string s, params Func<string, bool>[] predicates)
    {
      return s.MatchesAny((IEnumerable<Func<string, bool>>)predicates);
    }
  }
}
