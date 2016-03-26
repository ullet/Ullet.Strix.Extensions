/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.ActionTests
{
  [TestFixture]
  public class RepeatTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatOnce()
    {
      var callCount = 0;

      ((Action) (() => ++callCount)).Repeat(1);

      Assert.That(callCount, Is.EqualTo(1));
    }

    [Test]
    [Timeout(1000)]
    public void CanRepeatMultipleTimes()
    {
      const int numberOfTimes = 16;
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(numberOfTimes);

      Assert.That(callCount, Is.EqualTo(numberOfTimes));
    }

    [Test]
    [Timeout(1000)]
    public void CanRepeatZeroTimes()
    {
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(0);

      Assert.That(callCount, Is.EqualTo(0));
    }

    [Test]
    [Timeout(1000)]
    public void NegativeRepeatsTreatedAsZeroRepeats()
    {
      var callCount = 0;

      ((Action)(() => ++callCount)).Repeat(-21);

      Assert.That(callCount, Is.EqualTo(0));
    }
  }
}
