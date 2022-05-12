using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp5.Models.Base
{
    public class ModelBase
    {
        public int Id { get; set; }

        // Constructeur vide requis pour la désérialisation
        public ModelBase()
        {
        }

        public ModelBase(int id)
        {
            Id = id;
        }
    }
}
