using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DyerGame.Models;
namespace DyerGame.Views

{
    public class CelebAndGamePageModel
    {
        public Celeb Celeb { get; set; }

        public DyerGame.Models.Game ThisGame { get; set; }
        
    }
}
