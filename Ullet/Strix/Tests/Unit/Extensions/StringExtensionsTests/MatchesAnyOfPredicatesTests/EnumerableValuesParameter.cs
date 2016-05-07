/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.
  StringExtensionsTests.MatchesAnyOfPredicatesTests
{
  public class EnumerablePredicatesParameter
  {
    [TestFixture]
    public class WhenNoPredicates
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            Enumerable.Empty<Func<string, bool>>()),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenNullPredicates
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".MatchesAnyOf((IEnumerable<Func<string, bool>>) null),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenSinglePredicate
    {
      [Test]
      public void FalseIfPredicateIsNull()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new Func<string, bool>[] {null}),
          Is.False);
      }

      [Test]
      public void TrueIfMatchesOnPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)new[] { predicate }),
          Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 0;

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)new[] { predicate }),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenTwoPredicates
    {
      [Test]
      public void FalseIfBothPredicatesAreNull()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)
              new Func<string, bool>[] {null, null}),
          Is.False);
      }

      [Test]
      public void NullForFirstPredicateIsIgnored()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)
              new Func<string, bool>[] { null, s => true }),
          Is.True);
      }

      [Test]
      public void NullForSecondPredicateIsIgnored()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)
              new Func<string, bool>[] { s => true, null }),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnFirstPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;
        Func<string, bool> otherPredicate = s => s.Length == 11;

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new[]
            {
              predicate, otherPredicate
            }),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnSecondPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 9;
        Func<string, bool> otherPredicate = s => s.Length == 10;

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new[]
            {
              predicate, otherPredicate
            }),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnBothPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 10;
        Func<string, bool> otherPredicate = s => s.StartsWith("The");

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new[]
            {
              predicate, otherPredicate
            }),
          Is.True);
      }

      [Test]
      public void FalseIfDoesNotMatchOnEitherPredicate()
      {
        const string str = "The String";
        Func<string, bool> predicate = s => s.Length == 9;
        Func<string, bool> otherPredicate = s => s.StartsWith("A");

        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new[]
            {
              predicate, otherPredicate
            }),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenMultiplePredicates
    {
      [Test]
      public void FalseIfAllPredicatesAreNull()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)
              new Func<string, bool>[] {null, null, null, null}),
          Is.False);
      }

      [Test]
      public void AnyNullPredicatesAreIgnored()
      {
        Assert.That(
          "Any old string".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)
              new Func<string, bool>[] { null, s => true, null, null }),
          Is.True);
      }

      [TestCase("Absolutely")]
      [TestCase("Thermal")]
      [TestCase("Keep on trying")]
      [TestCase("Pea Soup")]
      public void TrueIfMatchesOnAnyOfThePredicates(string str)
      {
        Assert.That(
          str.MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)new Func<string, bool>[]
            {
              s => s.Length == 10,
              s => s.StartsWith("The"),
              s => s.EndsWith("ing"),
              s => s.Split(' ').Length == 2
            }),
          Is.True);
      }

      [Test]
      public void TrueIfMatchesOnAllOfThePredicates()
      {
        Assert.That(
          "The String".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>)new Func<string, bool>[]
            {
              s => s.Length == 10,
              s => s.StartsWith("The"),
              s => s.EndsWith("ing"),
              s => s.Split(' ').Length == 2
            }),
          Is.True);
      }

      [Test]
      public void FalseIfMatchesOnNoneOfThePredicates()
      {
        Assert.That(
          "Throwin'".MatchesAnyOf(
            (IEnumerable<Func<string, bool>>) new Func<string, bool>[]
            {
              s => s.Length == 10,
              s => s.StartsWith("The"),
              s => s.EndsWith("ing"),
              s => s.Split(' ').Length == 2
            }),
          Is.False);
      }
    }
  }
}
