/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using Microsoft.FSharp.Core;

namespace Ullet.Strix.Extensions.Tests.Unit.Support
{
  public static class FsFunc
  {
    public static FSharpFunc<TFrom, TTo> From<TFrom, TTo>(
      Converter<TFrom, TTo> csFn)
    {
      return FSharpFunc<TFrom, TTo>.FromConverter(csFn);
    }
  }
}
