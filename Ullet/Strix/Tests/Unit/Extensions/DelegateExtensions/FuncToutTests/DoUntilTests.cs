/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.FuncToutTests
{
  [TestFixture]
  public class DoUntilTests
  {
    [Test]
    [Timeout(1000)]
    public void ExecutesAtLeastOnce()
    {
      var executed = false;

      ((Func<bool>) (() => executed = true)).DoUntil(x => true);

      Assert.That(executed, Is.True);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Func<int>) (() => executionCount++)).DoUntil(x => executionCount >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsLastResultOfExpression()
    {
      var executionCount = 0;

      var result = ((Func<int>) (() =>
      {
        executionCount++;
        return executionCount*2;
      })).DoUntil(x => executionCount >= 10);

      Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToPredicate()
    {
      var executionCount = 0;

      ((Func<int>) (() => ++executionCount)).DoUntil(x => x >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
