using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentId, List<Comment> result)
        {
            using var context = new BlogContext();
            var comments = await context.Comments.Where(I => I.BlogId == blogId && I.ParentCommentId == parentId)
                .OrderByDescending(I => I.PostedTime).ToListAsync();
            if(comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if(comment.SubCumments == null)
                        comment.SubCumments = new List<Comment>();
                    await GetComments(comment.BlogId, comment.Id, comment.SubCumments);
                    if(!result.Contains(comment))
                        result.Add(comment);
                }
            }
        }
    }
}
