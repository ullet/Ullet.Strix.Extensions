/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Linq;
using FsCheck;

namespace Ullet.Strix.Extensions.Tests.Unit.Support
{
  public static class GenExt
  {
    public static Gen<TValue[]> NonEmptyArrayOf<TValue>(Gen<TValue> gen)
    {
      return NotEmpty(Gen.ArrayOf(gen));
    }

    public static Gen<TValue[]> NotEmpty<TValue>(Gen<TValue[]> gen)
    {
      return Gen.SuchThat(FsFunc.From((TValue[] a) => a.Any()), gen);
    }
  }
}
