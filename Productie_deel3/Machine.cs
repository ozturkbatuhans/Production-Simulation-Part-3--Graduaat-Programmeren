using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productie_deel3;

namespace Productie_deel3
{
    public class Machine
    {
        private string _sNaamMachine;
        private Product _oProductMomenteelVerwerkt;

        public Machine(string sNaamMachine)
        {
            _sNaamMachine = sNaamMachine;
            _oProductMomenteelVerwerkt = null;
        }
        
        public string NaamMachine
        {
            get { return _sNaamMachine; }
        }

        public Product ProductMomenteelVerwerkt
        {
            get { return _oProductMomenteelVerwerkt; }
            set { _oProductMomenteelVerwerkt = value; }
        }

        public bool Actief
        {
            get { return _oProductMomenteelVerwerkt != null; }
        }

        public void PlanIn(Product prod)
        {
            _oProductMomenteelVerwerkt = prod;
        }
    }
}
