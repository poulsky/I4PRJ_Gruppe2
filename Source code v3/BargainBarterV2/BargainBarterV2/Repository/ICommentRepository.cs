using System;
using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    public interface ICommentRepository : IDisposable
    {
        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int commentId);
        void InsertComment(Comment comment);
        void DeleteComment(int commentId);
        void UpdateComment(Comment comment);
        void Save();
    }
}