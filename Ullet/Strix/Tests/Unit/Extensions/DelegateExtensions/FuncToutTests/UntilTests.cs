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
  public class UntilTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyTrue()
    {
      var executed = false;

      ((Func<bool>)(() => executed = true)).Until(x => true);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Func<int>)(() => executionCount++)).Until(x => executionCount >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsLastResultOfExpression()
    {
      var executionCount = 0;

      var result = ((Func<int>)(() =>
      {
        executionCount++;
        return executionCount * 2;
      })).Until(x => executionCount >= 10);

      Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToPredicate()
    {
      var executionCount = 0;

      ((Func<int>)(() => ++executionCount)).Until(x => x >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void PassesDefaultValueToPredicateOnFirstCall()
    {
      var predicateInput = -1;
      ((Func<int>) (() => 42))
        .Until(x => { predicateInput = x; return true; });

      Assert.That(predicateInput, Is.EqualTo(0));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsDefaultValueIfExpressionNeverExecuted()
    {
      var result = ((Func<int>)(() => 42)).Until(x => true);

      Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    [Timeout(1000)]
    public void CanUseSpecificDefaultResultValue()
    {
      const int specificDefault = 83;
      var predicateInput = -1;
      var result = ((Func<int>)(() => 42))
        .Until(specificDefault, x => { predicateInput = x; return true; });

      Assert.That(predicateInput, Is.EqualTo(specificDefault));
      Assert.That(result, Is.EqualTo(specificDefault));
    }
  }
}
