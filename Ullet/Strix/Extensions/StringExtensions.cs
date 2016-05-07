/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

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
      s = s ?? string.Empty;
      return (prefixes ?? Enumerable.Empty<string>()).Any(s.StartsWith);
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
  }
}
