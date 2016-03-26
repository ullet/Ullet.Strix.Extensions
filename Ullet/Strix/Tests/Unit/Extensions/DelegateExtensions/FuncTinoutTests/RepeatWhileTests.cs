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
  public class RepeatWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatWhilePredicateTrue()
    {
      var output = ((Func<int, int>) (x => x + 1)).RepeatWhile(x => x < 3);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1, 2, 3}));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsEmptyIfPredicateNeverTrue()
    {
      var output = ((Func<int, int>) (x => x + 1)).RepeatWhile(x => false);

      Assert.That(output, Is.Empty);
    }

    [Test]
    [Timeout(1000)]
    public void CanSpecifyInitialInputValue()
    {
      var output = ((Func<int, int>)(x => x + 1)).RepeatWhile(5, x => x < 8);

      Assert.That(output.ToArray(), Is.EqualTo(new[] { 6, 7, 8 }));
    }
  }
}
