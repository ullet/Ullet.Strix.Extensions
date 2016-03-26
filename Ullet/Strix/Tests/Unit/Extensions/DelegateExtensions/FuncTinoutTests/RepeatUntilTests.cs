/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Linq;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.FuncTinoutTests
{
  [TestFixture]
  public class RepeatUntilTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatUntilPredicateTrue()
    {
      var output = ((Func<int, int>) (x => x + 1)).RepeatUntil(x => x >= 3);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1, 2, 3}));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsEmptyIfPredicateNeverFalse()
    {
      var output = ((Func<int, int>) (x => x + 1)).RepeatUntil(x => true);

      Assert.That(output, Is.Empty);
    }

    [Test]
    [Timeout(1000)]
    public void CanSpecifyInitialInputValue()
    {
      var output = ((Func<int, int>) (x => x + 1)).RepeatUntil(5, x => x >= 8);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {6, 7, 8}));
    }
  }
}
