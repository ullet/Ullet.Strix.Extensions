/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.GeneralExtensionsTests.MatchesAnyTests
{
  public class ParamsValuesParameter
  {
    [TestFixture]
    public class WhenNoPredicates
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(new object().MatchesAny(), Is.False);
      }
    }

    [TestFixture]
    public class WhenNullPredicates
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".MatchesAny((Func<string, bool>[]) null), Is.False);
      }
    }

    [TestFixture]
    public class WhenSinglePredicate
    {
      [Test]
      public void FalseIfPredicateIsNull()
      {
        Assert.That(7.0891m.MatchesAny((Func<decimal, bool>) null), Is.False);
      }

      [Test]
      public void TrueIfMatchesOnPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;

        Assert.That(str.MatchesAny(predicate), Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnPredicate()
      {
        Func<int, bool> even = x => x % 2 == 0;

        Assert.That(41.MatchesAny(even), Is.False);
      }
    }

    [TestFixture]
    public class WhenTwoPredicates
    {
      [Test]
      public void FalseIfBothPredicatesAreNull()
      {
        Assert.That(
          "Any old string".MatchesAny(
            (Func<string, bool>) null, (Func<string, bool>) null),
          Is.False);
      }

      [Test]
      public void NullForFirstPredicateIsIgnored()
      {
        Assert.That(
          "Any old string".MatchesAny(
            (Func<string, bool>)null, (Func<string, bool>)(s => true)),
          Is.True);
      }

      [Test]
      public void NullForSecondPredicateIsIgnored()
      {
        Assert.That(
          "Any old string".MatchesAny(
            (Func<string, bool>)(s => true), (Func<string, bool>)null),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnFirstPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;
        Func<string, bool> otherPredicate = s => s.Length == 11;

        Assert.That(str.MatchesAny(predicate, otherPredicate), Is.True);
      }

      [Test]
      public void TrueIfMatchesOnSecondPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 9;
        Func<string, bool> otherPredicate = s => s.Length == 10;

        Assert.That(str.MatchesAny(predicate, otherPredicate), Is.True);
      }

      [Test]
      public void TrueIfMatchesOnBothPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;
        Func<string, bool> otherPredicate = s => s.StartsWith("The");

        Assert.That(str.MatchesAny(predicate, otherPredicate), Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnEitherPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 9;
        Func<string, bool> otherPredicate = s => s.StartsWith("A");

        Assert.That(str.MatchesAny(predicate, otherPredicate), Is.False);
      }
    }

    [TestFixture]
    public class WhenMultiplePredicates
    {
      [Test]
      public void FalseIfAllPredicatesAreNull()
      {
        Assert.That(
          "Any old string".MatchesAny(
            (Func<string, bool>) null,
            (Func<string, bool>) null,
            (Func<string, bool>) null,
            (Func<string, bool>) null),
          Is.False);
      }

      [Test]
      public void AnyNullPredicatesAreIgnored()
      {
        Assert.That(
          "Any old string".MatchesAny(
            (Func<string, bool>)null,
            (Func<string, bool>)null,
            (Func<string, bool>)(s => true),
            (Func<string, bool>)null),
          Is.True);
      }

      [TestCase("Absolutely")]
      [TestCase("Thermal")]
      [TestCase("Keep on trying")]
      [TestCase("Pea Soup")]
      public void TrueIfMatchesOnAnyOfThePredicates(string str)
      {
        Assert.That(
          str.MatchesAny(
            s => s.Length == 10,
            s => s.StartsWith("The"),
            s => s.EndsWith("ing"),
            s => s.Split(' ').Length == 2),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnAllOfThePredicates()
      {
        Assert.That(
          "The String".MatchesAny(
            s => s.Length == 10,
            s => s.StartsWith("The"),
            s => s.EndsWith("ing"),
            s => s.Split(' ').Length == 2),
          Is.True);
      }

      [Test]
      public void FalseIfMatchesOnNoneOfThePredicates()
      {
        Assert.That(
          "Throwin'".MatchesAny(
            s => s.Length == 10,
            s => s.StartsWith("The"),
            s => s.EndsWith("ing"),
            s => s.Split(' ').Length == 2),
          Is.False);
      }
    }
  }
}
