using System;
using System.Collections.Generic;
using AopSample.Core.Interfaces.Data;

namespace AopSample.Core.Domain
{
    public class Show : Entity, IAggregateRoot
    {
        public Show()
        {
            Genres = new List<Genre>();
        }

        public virtual string Name { get; protected set; }
        public virtual string ContentRating { get; protected set; }
        public virtual DateTime? FirstAired { get; protected set; }
        public virtual DateTime? LastAired { get; protected set; }
        public virtual string Overview { get; protected set; }
        public virtual int? Runtime { get; protected set; }
        public virtual string Poster { get; protected set; }
        public virtual int? TvdbKey { get; protected set; }
        public virtual string ImdbKey { get; protected set; }

        public virtual IList<Genre> Genres { get; protected set; }

        public static Show Create(string name, string overview)
        {
            var show = new Show {Name = name, Overview = overview };
            return show;
        }

        public virtual void Edit(string name, string overview)
        {
            Name = name;
            Overview = overview;
        }
    }
}