using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DyerGame.Models
{
    public class Celeb
    {
        public string Name { get; }
        public CelebState State { get; private set; }

        public Celeb(string name)
        {
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
    }
}
