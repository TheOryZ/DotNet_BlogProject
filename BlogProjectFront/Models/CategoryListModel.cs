using System;
using System.Diagnostics.CodeAnalysis;

namespace BlogProjectFront.Models
{
    public class CategoryListModel : IEquatable<CategoryListModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals([AllowNull] CategoryListModel other)
        {
            return this.Id == other.Id && this.Name == other.Name;
        }
    }
}