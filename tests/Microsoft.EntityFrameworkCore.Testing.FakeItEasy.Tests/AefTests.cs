using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Testing.FakeItEasy.Tests
{
    public class AefTests
    {
        [Fact]
        public void Add_ShouldAddItem()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
            var newModel = new TestModel();

            fakeDbSet.Add(newModel);

            Assert.Contains(newModel, fakeDbSet.ToList());
        }

        [Fact]
        public void Add_ShouldNotThrowWhenIteratingThrough()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
            fakeDbSet.Add(new TestModel());

            foreach (var model in fakeDbSet)
            {
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void AddRange_ShouldAddItems()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> {new TestModel(), new TestModel()});
            var newModels = new[] { new TestModel(), new TestModel() };
            
            fakeDbSet.AddRange(newModels);
            
            Assert.True(fakeDbSet.Except(newModels).Count() == 2);
        }

        [Fact]
        public void AddRange_ShouldNotThrowWhenIteratingThrough()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });

            fakeDbSet.AddRange(new[] { new TestModel(), new TestModel() });
            
            foreach (var model in fakeDbSet)
            {
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void Remove_ShouldRemoveItem()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
            var toRemove = fakeDbSet.First();

            fakeDbSet.Remove(toRemove);

            Assert.DoesNotContain(toRemove, fakeDbSet.ToList());
        }

        [Fact]
        public void Remove_ShouldNotThrowWhenIteratingThrough()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel> { new TestModel(), new TestModel() });
            
            fakeDbSet.Remove(fakeDbSet.First());
            
            foreach (var model in fakeDbSet)
            {
                Assert.NotNull(model);
            }
        }

        [Fact]
        public void RemoveRange_ShouldRemoveItems()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel>
            {
                new TestModel {Property = "a"},
                new TestModel {Property = "a"},
                new TestModel {Property = "b"}
            });

            fakeDbSet.RemoveRange(fakeDbSet.Where(x => x.Property == "a"));

            Assert.Equal(1, fakeDbSet.Count());
        }

        [Fact]
        public void Include_ReturnsItSelf()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel>());
            
            Assert.StrictEqual(fakeDbSet, fakeDbSet.Include("asdas"));
        }

        [Fact]
        public void AsNoTracking_ReturnsItSelf()
        {
            var fakeDbSet = Aef.FakeDbSet(new List<TestModel>());

            Assert.StrictEqual(fakeDbSet, fakeDbSet.AsNoTracking());
        }
    }
}