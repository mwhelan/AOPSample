﻿using System;
using AopSample.Core.Interfaces.Commands;

namespace AopSample.Core.Commands.Shows.EditShow
{
    public class EditShowCommand : ICommand
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContentRating { get; set; }
        public virtual DateTime? FirstAired { get; set; }
        public virtual DateTime? LastAired { get; set; }
        public virtual string Overview { get; set; }
        public virtual int? Runtime { get; set; }
    }
}
