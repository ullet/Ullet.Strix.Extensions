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
  public class WhileTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyFalse()
    {
      var executed = false;

      ((Func<bool>)(() => executed = true)).While(x => false);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesWhileConditionTrue()
    {
      var executionCount = 0;

      ((Func<int>)(() => executionCount++)).While(x => executionCount < 10);

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
      })).While(x => executionCount < 10);

      Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToPredicate()
    {
      var executionCount = 0;

      ((Func<int>)(() => ++executionCount)).While(x => x < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void PassesDefaultValueToPredicateOnFirstCall()
    {
      var predicateInput = -1;
      ((Func<int>) (() => 42))
        .While(x => { predicateInput = x; return false; });

      Assert.That(predicateInput, Is.EqualTo(0));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsDefaultValueIfExpressionNeverExecuted()
    {
      var result = ((Func<int>)(() => 42)).While(x => false);

      Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    [Timeout(1000)]
    public void CanUseSpecificDefaultResultValue()
    {
      const int specificDefault = 83;
      var predicateInput = -1;
      var result = ((Func<int>)(() => 42))
        .While(specificDefault, x => { predicateInput = x; return false; });

      Assert.That(predicateInput, Is.EqualTo(specificDefault));
      Assert.That(result, Is.EqualTo(specificDefault));
    }
  }
}
