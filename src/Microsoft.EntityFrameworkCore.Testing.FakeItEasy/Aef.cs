﻿using System.Collections.Generic;
using System.Linq;
using FakeItEasy;

namespace Microsoft.EntityFrameworkCore.Testing.FakeItEasy
{
    public static class Aef
    {
        public static DbSet<T> FakeDbSet<T>(IList<T> data) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(b =>
            {
                b.Implements(typeof(IQueryable<T>));
                b.Implements(typeof(IAsyncEnumerable<T>));
            });

            QueryableSetUp(fakeDbSet, data);
            CollectionSetUp(fakeDbSet, data);
            DbQuerySetUp(fakeDbSet);

            return fakeDbSet;
        }

        public static DbSet<T> FakeDbSet<T>(IEnumerable<T> data) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(b =>
            {
                b.Implements(typeof(IAsyncEnumerable<T>));
                b.Implements(typeof(IQueryable<T>));
            });

            QueryableSetUp(fakeDbSet, data);

            return fakeDbSet;
        }

        public static DbSet<T> FakeDbSet<T>(int numberOfFakes) where T : class
        {
            var data = A.CollectionOfFake<T>(numberOfFakes);
            return FakeDbSet(data);
        }

        public static DbSet<T> FakeDbSet<T>() where T : class
        {
            return FakeDbSet(new List<T>());
        }

        private static void QueryableSetUp<T>(IQueryable<T> fakeDbSet, IEnumerable<T> data) where T : class
        {
            A.CallTo(() => fakeDbSet.Provider)
                .ReturnsLazily(l => (new TestAsyncQueryProvider<T>(data.AsQueryable().Provider)));
            A.CallTo(() => fakeDbSet.Expression).ReturnsLazily(l => data.AsQueryable().Expression);
            A.CallTo(() => fakeDbSet.ElementType).ReturnsLazily(l => data.AsQueryable().ElementType);
            A.CallTo(() => fakeDbSet.GetEnumerator()).ReturnsLazily(l => data.AsQueryable().GetEnumerator());
            A.CallTo(() => ((IAsyncEnumerable<T>) fakeDbSet).GetEnumerator())
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));
        }


        private static void CollectionSetUp<T>(DbSet<T> fakeDbSet, ICollection<T> data) where T : class
        {
            A.CallTo(() => fakeDbSet.Add(A<T>._)).Invokes((T item) => data.Add(item));
            A.CallTo(() => fakeDbSet.AddRange(A<IEnumerable<T>>._)).Invokes((IEnumerable<T> items) =>
            {
                foreach (var item in items)
                    data.Add(item);
            });
            A.CallTo(() => fakeDbSet.Remove(A<T>._)).Invokes((T item) => data.Remove(item));
            A.CallTo(() => fakeDbSet.RemoveRange(A<IEnumerable<T>>._)).Invokes((IEnumerable<T> items) =>
            {
                foreach (var item in items.ToList())
                    data.Remove(item);
            });
        }

        private static void DbQuerySetUp<T>(IQueryable<T> fakeDbQuery) where T : class
        {
        }
    }
}