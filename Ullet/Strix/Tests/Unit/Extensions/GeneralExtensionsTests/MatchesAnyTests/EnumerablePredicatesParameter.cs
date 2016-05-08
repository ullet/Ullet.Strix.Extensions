/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using NUnit.Framework;
using Ullet.Strix.Extensions.Tests.Unit.Support;

namespace
  Ullet.Strix.Extensions.Tests.Unit.GeneralExtensionsTests.MatchesAnyTests
{
  public abstract class EnumerablePredicatesParameter
  {
    [TestFixture(typeof(int))]
    [TestFixture(typeof(string))]
    [TestFixture(typeof(decimal))]
    private abstract class Fixture
    {
    }

    private class WhenEmptyPredicates<T> : Fixture
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop
          .ForAll<T>(x => !x.MatchesAny(Enumerable.Empty<Func<T, bool>>()))
          .QuickCheckThrowOnFailure();
      }
    }

    private class WhenNullPredicates<T> : Fixture
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop
          .ForAll<T>(x => !x.MatchesAny((IEnumerable<Func<T, bool>>)null))
          .QuickCheckThrowOnFailure();
      }
    }

    private class WhenSomePredicates<T> : Fixture
    {
      [Test]
      public void FalseIfAllPredicatesAreNull()
      {
        var nullPredicateGen = Gen.Constant(Predicates<T>.Null);
        var allNullPredicatesGen = GenExt.NonEmptyArrayOf(nullPredicateGen);

        Prop
          .ForAll<T>(
            t => Prop.ForAll(
              Arb.From(allNullPredicatesGen),
              predicates =>
                !t.MatchesAny((IEnumerable<Func<T, bool>>)predicates)))
          .QuickCheckThrowOnFailure();
      }

      [Test]
      public void AnyNullPredicatesAreIgnored()
      {
        var possiblyNullPredicateGen =
          Gen.Elements(Predicates<T>.True, Predicates<T>.Null);
        var predicateArrayGen =
          GenExt.NonEmptyArrayOf(possiblyNullPredicateGen);
        Func<Func<T, bool>[], bool> someNull = ps => ps.Any(p => p == null);
        Func<Func<T, bool>[], bool> someNotNull = ps => ps.Any(p => p != null);
        var mixOfNullAndNotNullPredicatesGen = Gen.SuchThat(
          FsFunc.From((Func<T, bool>[] ps) => someNull(ps) && someNotNull(ps)),
          predicateArrayGen);

        Prop
          .ForAll<T>(
            t => Prop.ForAll(
              Arb.From(mixOfNullAndNotNullPredicatesGen),
              predicates =>
                t.MatchesAny((IEnumerable<Func<T, bool>>)predicates)))
          .QuickCheckThrowOnFailure();
      }

      [Test]
      public void TrueIfMatchesOnAnyOfThePredicates()
      {
        var trueOrFalsePredicateGen =
          Gen.Elements(Predicates<T>.True, Predicates<T>.False);
        var predicateArrayGen = GenExt.NonEmptyArrayOf(trueOrFalsePredicateGen);
        Func<Func<T, bool>[], bool> someTrue = ps => ps.Any(p => p(default(T)));
        Func<Func<T, bool>[], bool>
          someFalse = ps => ps.Any(p => !p(default(T)));
        var mixOfTrueAndFalsePredicatesGen = Gen.SuchThat(
          FsFunc.From((Func<T, bool>[] ps) => someTrue(ps) && someFalse(ps)),
          predicateArrayGen);

        Prop
          .ForAll<T>(
            t => Prop.ForAll(
              Arb.From(mixOfTrueAndFalsePredicatesGen),
              predicates =>
                t.MatchesAny((IEnumerable<Func<T, bool>>)predicates)))
          .QuickCheckThrowOnFailure();
      }

      [Test]
      public void TrueIfMatchesOnAllOfThePredicates()
      {
        var truePredicateGen = Gen.Constant(Predicates<T>.True);
        var allTruePredicatesGen = GenExt.NonEmptyArrayOf(truePredicateGen);

        Prop
          .ForAll<T>(
            t => Prop.ForAll(
              Arb.From(allTruePredicatesGen),
              predicates =>
                t.MatchesAny((IEnumerable<Func<T, bool>>)predicates)))
          .QuickCheckThrowOnFailure();
      }

      [Test]
      public void FalseIfMatchesOnNoneOfThePredicates()
      {
        var falsePredicateGen = Gen.Constant(Predicates<T>.False);
        var allFalsePredicatesGen = GenExt.NonEmptyArrayOf(falsePredicateGen);

        Prop
          .ForAll<T>(
            t => Prop.ForAll(
              Arb.From(allFalsePredicatesGen),
              predicates =>
                !t.MatchesAny((IEnumerable<Func<T, bool>>)predicates)))
          .QuickCheckThrowOnFailure();
      }
    }
  }
}
