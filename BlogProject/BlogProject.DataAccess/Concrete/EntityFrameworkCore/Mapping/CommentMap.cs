using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(I => I.AuthorEmail).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Description).HasMaxLength(500).IsRequired();

            builder.HasOne(I => I.ParentComment).WithMany(I => I.SubCumments).HasForeignKey(I => I.ParentCommentId);
            //Example : builder.HasMany(I => I.SubCumments).WithOne(I => I.ParentComment).HasForeignKey(I => I.ParentCommentId);
        }
    }
}
