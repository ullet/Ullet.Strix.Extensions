/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Extensions
{
  public static partial class DelegateExtensions
  {
    /// <summary>
    /// Convert <see cref="Func{T, Tbool}"/> to <see cref="Predicate{T}"/>.
    /// </summary>
    public static Predicate<T> ToPredicate<T>(this Func<T, bool> f)
    {
      return t => f(t);
    }
  }
}
