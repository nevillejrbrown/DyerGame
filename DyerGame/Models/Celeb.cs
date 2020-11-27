﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DyerGame.Models
{

    public enum CelebState
    {
        IN_HAT,
        GUESSED,
        BURNED
    }

    public class Celeb
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CelebState State { get; set; }

        public Celeb(string name, int id = -1)
        {
            this.Id = id;
            this.Name = name;
            State = CelebState.IN_HAT;
        }

        public void Burn()
        {
            this.State = CelebState.BURNED;
        }

        public void Guess()
        {
            if (this.State == CelebState.BURNED || this.State == CelebState.GUESSED)
            {
                throw new InvalidOperationException("Celeb must be IN_HAT to be guessed");
            }
            this.State = CelebState.GUESSED;
        }

        public void PutBackIntoHat()
        {
            if (this.State == CelebState.BURNED || this.State == CelebState.IN_HAT)
            {
                throw new InvalidOperationException("Celeb must be GUESSED to be put back into hat");
            }
            this.State = CelebState.IN_HAT;
        }

        public override bool Equals(object obj)
        {
            var celeb = obj as Celeb;
            return celeb == null ? false : celeb.Id == this.Id;
        }
    }
}