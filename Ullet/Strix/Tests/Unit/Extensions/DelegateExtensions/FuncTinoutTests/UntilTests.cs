/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.FuncTinoutTests
{
  [TestFixture]
  public class UntilTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyTrue()
    {
      var executed = false;

      ((Func<bool, bool>)(x => executed = true)).Until(x => true);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Func<int, int>)(x => executionCount++))
        .Until(x => executionCount >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsLastResultOfExpression()
    {
      var executionCount = 0;

      var result = ((Func<int, int>)(x =>
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

      ((Func<int, int>)(x => ++executionCount)).Until(x => x >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToNextCall()
    {
      var result = ((Func<int, int>)(x => x + 1)).Until(x => x >= 10);

      Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void UsesDefaultValueOfTypeForDefaultResult()
    {
      var result = ((Func<string, string>)(s => s ?? "something"))
        .Until(x => true);

      Assert.That(result, Is.Null);
    }

    [Test]
    [Timeout(1000)]
    public void UsesDefaultValueOfTypeForInitialValue()
    {
      var result = ((Func<string, string>)(s => s ?? "something"))
        .Until(x => x != null);

      Assert.That(result, Is.EqualTo("something"));
    }

    [Test]
    [Timeout(1000)]
    public void CanSpecifyValueUsedForInitialValue()
    {
      const int initialValue = 9;
      var executionCount = 0;
      var result = ((Func<int, int>)(x =>
      {
        executionCount++;
        return x + 1;
      })).Until(initialValue, x => x >= 10);

      Assert.That(executionCount, Is.EqualTo(1));
      Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void CanSpecifySeparateInitialValueAndDefaultResult()
    {
      const int initialValue = 9;
      const int defaultResult = -99;
      var executionCount = 0;

      var resultUsingInitialValue = ((Func<int, int>)(x =>
      {
        executionCount++;
        return x + 1;
      })).Until(initialValue, defaultResult, x => x >= 10);

      var resultShouldBeDefault = ((Func<int, int>)(x => x + 1))
        .Until(initialValue, defaultResult, x => true);

      Assert.That(resultUsingInitialValue, Is.EqualTo(10));
      Assert.That(executionCount, Is.EqualTo(1));
      Assert.That(resultShouldBeDefault, Is.EqualTo(defaultResult));
    }
  }
}
