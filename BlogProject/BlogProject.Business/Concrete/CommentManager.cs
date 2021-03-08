using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericDal, ICommentDal commentDal) : base(genericDal)
        {
            _commentDal = commentDal;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            return await _commentDal.GetAllWithSubCommentsAsync(blogId, parentId);
        }
    }
}
