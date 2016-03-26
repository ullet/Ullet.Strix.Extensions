/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Linq;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensionsTests
{
  [TestFixture]
  public class RepeatTests
  {
    [Test]
    public void CanRepeatActionOnce()
    {
      var callCount = 0;

      ((Action) (() => ++callCount)).Repeat(1);

      Assert.That(callCount, Is.EqualTo(1));
    }

    [Test]
    public void CanRepeatActionMultipleTimes()
    {
      const int numberOfTimes = 16;
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(numberOfTimes);

      Assert.That(callCount, Is.EqualTo(numberOfTimes));
    }

    [Test]
    public void CanRepeatActionZeroTimes()
    {
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(0);

      Assert.That(callCount, Is.EqualTo(0));
    }

    [Test]
    public void NegativeActionRepeatsTreatedAsZeroRepeats()
    {
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(-21);

      Assert.That(callCount, Is.EqualTo(0));
    }

    [Test]
    public void CanRepeatFunctionOnce()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(1);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1}));
    }

    [Test]
    public void CanRepeatFunctionMultipleTimes()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(4);

      Assert.That(output.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4 }));
    }

    [Test]
    public void CanRepeatFunctionZeroTimes()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(0);

      Assert.That(output.ToArray(), Is.Empty);
    }

    [Test]
    public void NegativeFunctionRepeatsTreatedAsZeroRepeats()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(-12);

      Assert.That(output.ToArray(), Is.Empty);
    }
  }
}
