using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            return context.Comments.ToList();
        }

        public Comment GetCommentById(int commentId)
        {
            return context.Comments.Find(commentId);
        }

        public void InsertComment(Comment comment)
        {
            context.Comments.Add(comment);
        }

        public void DeleteComment(int commentId)
        {
            Comment comment = context.Comments.Find(commentId);
            context.Comments.Find(comment);
        }

        public void UpdateComment(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
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