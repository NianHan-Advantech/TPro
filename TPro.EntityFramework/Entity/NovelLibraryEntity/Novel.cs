using System;
using System.Collections.Generic;

namespace TPro.EntityFramework.Entity.NovelLibraryEntity
{
    public class Novel
    {
        public Novel()
        {
            Chapters = new HashSet<NovelChapter>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual NovelAuthor Author { get; set; }
        public virtual ICollection<NovelChapter> Chapters { get; set; }
        public virtual ICollection<NovelTag> Tags { get; set; }
    }

    public class NovelChapter
    {
        public int Id { get; set; }
        public int NovelId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual Novel Novel { get; set; }
    }

    public class NovelAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Novel> Works { get; set; }
    }

    public class NovelTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}