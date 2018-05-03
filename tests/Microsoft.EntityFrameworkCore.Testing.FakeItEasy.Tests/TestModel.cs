using System.ComponentModel.DataAnnotations;

namespace Microsoft.EntityFrameworkCore.Testing.FakeItEasy.Tests
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }

        public string Property { get; set; }
    }
}