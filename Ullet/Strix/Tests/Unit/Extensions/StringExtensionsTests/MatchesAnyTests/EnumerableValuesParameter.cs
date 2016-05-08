/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.StringExtensionsTests.MatchesAnyTests
{
  public class EnumerableValuesParameter
  {
    [TestFixture]
    public class WhenNoValues
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".MatchesAny<string>(
            (s,v) => true, Enumerable.Empty<string>()),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenNullValues
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".MatchesAny<string>(
            (s, v) => true, (IEnumerable<string>) null),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenSingleValue
    {
      [Test]
      public void TrueIfMatchesOnValue()
      {
        const int value = 10;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length == len;

        Assert.That(
          str.MatchesAny(predicate, (IEnumerable<int>) new[] {value}),
          Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnValue()
      {
        const int value = 0;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length == len;

        Assert.That(
          str.MatchesAny(predicate, (IEnumerable<int>)new[] { value }),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenTwoValues
    {
      [Test]
      public void TrueIfMatchesOnFirstValue()
      {
        const int value = 10;
        const int otherValue = 11;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length == len;

        Assert.That(
          str.MatchesAny(
            predicate, (IEnumerable<int>) new[] {value, otherValue}),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnSecondValue()
      {
        const int value = 9;
        const int otherValue = 10;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length == len;

        Assert.That(
          str.MatchesAny(
            predicate, (IEnumerable<int>)new[] { value, otherValue }),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnBothValue()
      {
        const int value = 10;
        const int otherValue = 11;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length >= len;

        Assert.That(
          str.MatchesAny(
            predicate, (IEnumerable<int>)new[] { value, otherValue }),
          Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnEitherValue()
      {
        const int value = 9;
        const int otherValue = 10;
        const string str = "The String";
        Func<string, int, bool> predicate = (s, len) => s.Length < len;

        Assert.That(
          str.MatchesAny(
            predicate, (IEnumerable<int>)new[] { value, otherValue }),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenMultipleValues
    {
      [TestCase('A')]
      [TestCase('B')]
      [TestCase('C')]
      [TestCase('D')]
      public void TrueIfMatchesOnAnyOfTheValues(char actualValue)
      {
        var str = actualValue + " string";
        Func<string, char, bool> predicate = (s, ch) => s[0] == ch;

        Assert.That(
          str.MatchesAny(
            predicate,
            (IEnumerable<char>) new[] { 'A', 'B', 'C', 'D'}),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnAllOfTheValues()
      {
        const string str = "A String";
        Func<string, int, bool> predicate = (s, len) => s.Length > len;

        Assert.That(
          str.MatchesAny(
          predicate,
            (IEnumerable<int>) new[] {0, 1, 2, 3}),
          Is.True);
      }

      [Test]
      public void FalseIfMatchesOnNoneOfTheValues()
      {
        const string str = "A String";
        Func<string, int, bool> predicate = (s, len) => s.Length <= len;

        Assert.That(
          str.MatchesAny(
          predicate,
            (IEnumerable<int>)new[] { 0, 1, 2, 3 }),
          Is.False);
      }
    }
  }
}
