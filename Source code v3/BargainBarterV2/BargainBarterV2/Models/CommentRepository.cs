using System;
using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    class CommentRepository : ICommentRepository, IDisposable
    {
        private ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Comment> GetComments()
        {
            throw new NotImplementedException();
        }

        public Address GetCommentById(int commentId)
        {
            throw new NotImplementedException();
        }

        public void InsertComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}