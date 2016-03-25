using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensionsTests
{
  [TestFixture]
  public class FuncTinoutWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyFalse()
    {
      var executed = false;

      ((Func<bool, bool>)(x => executed = true)).While(x => false);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Func<int, int>)(x => executionCount++))
        .While(x => executionCount < 10);

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
      })).While(x => executionCount < 10);

      Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToPredicate()
    {
      var executionCount = 0;

      ((Func<int, int>)(x => ++executionCount)).While(x => x < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void PassesResultOfExpressionToNextCall()
    {
      var result = ((Func<int, int>)(x => x + 1)).While(x => x < 10);

      Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    [Timeout(1000)]
    public void UsesDefaultValueOfTypeForDefaultResult()
    {
      var result = ((Func<string, string>)(s => s ?? "something"))
        .While(x => false);

      Assert.That(result, Is.Null);
    }

    [Test]
    [Timeout(1000)]
    public void UsesDefaultValueOfTypeForInitialValue()
    {
      var result = ((Func<string, string>)(s => s ?? "something"))
        .While(x => x == null);

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
      })).While(initialValue, x => x < 10);

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
      })).While(initialValue, defaultResult, x => x < 10);

      var resultShouldBeDefault = ((Func<int, int>)(x => x + 1))
        .While(initialValue, defaultResult, x => false);

      Assert.That(resultUsingInitialValue, Is.EqualTo(10));
      Assert.That(executionCount, Is.EqualTo(1));
      Assert.That(resultShouldBeDefault, Is.EqualTo(defaultResult));
    }
  }
}
