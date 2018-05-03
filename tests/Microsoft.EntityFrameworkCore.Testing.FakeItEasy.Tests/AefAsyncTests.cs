using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Testing.FakeItEasy.Tests
{
    public class AefAsyncTests
    {
        [Fact]
        public async Task WhereAsync_ShouldReturnFilteredItems()
        {
            var fakeDbSet = Aef.FakeDbSet(
                new List<TestModel> {
                    new TestModel { Id = 1, Property = "apple" },
                    new TestModel { Id = 2, Property = "banana" },
                    new TestModel { Id = 3, Property = "Cherry" }
                });

            var items = await fakeDbSet.Where(e => e.Property.Contains("a")).ToListAsync();
            
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnFilteredSinlgeItem()
        {
            var fakeDbSet = Aef.FakeDbSet(
                new List<TestModel> {
                    new TestModel { Id = 1, Property = "apple" },
                    new TestModel { Id = 2, Property = "banana" },
                    new TestModel { Id = 3, Property = "Cherry" }
                });

            var items = await fakeDbSet.FirstOrDefaultAsync(e => e.Property.Equals("Cherry"));
            
            Assert.Equal("Cherry", items.Property);
        }

        [Fact]
        public async Task AnyAsync_ShouldReturnTrue()
        {
            var fakeDbSet = Aef.FakeDbSet(
                new List<TestModel> {
                    new TestModel { Id = 1, Property = "apple" },
                    new TestModel { Id = 2, Property = "banana" },
                    new TestModel { Id = 3, Property = "Grape" }
                });

            var result = await fakeDbSet.AnyAsync(e => e.Property.Contains("n"));

            Assert.True(result);
        }

        [Fact]
        public async Task AllAsync_ShouldReturnTrue()
        {
            var fakeDbSet = Aef.FakeDbSet(
                new List<TestModel> {
                    new TestModel { Id = 1, Property = "apple" },
                    new TestModel { Id = 2, Property = "banana" },
                    new TestModel { Id = 3, Property = "Grape" }
                });

            var result = await fakeDbSet.AllAsync(e => e.Property.Contains("a"));

            Assert.True(result);
        }

        [Fact]
        public async Task CountAsync_ShouldReturnListCount()
        {
            var fakeDbSet = Aef.FakeDbSet(
                new List<TestModel> {
                    new TestModel { Id = 1, Property = "apple" },
                    new TestModel { Id = 2, Property = "banana" },
                    new TestModel { Id = 3, Property = "Grape" }
                });

            var result = await fakeDbSet.CountAsync();
            
            Assert.Equal(3, result);
        }
    }
}