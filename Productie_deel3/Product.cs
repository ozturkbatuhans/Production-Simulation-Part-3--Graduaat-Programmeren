using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Productie_deel3
{
    public class Product
    {
        private string _sNaam;
        private Queue<Machine> _oMachines;
        private bool _xProductGestart;
        private bool _xProductAfgewerkt;

        public Product(string naam, Queue<Machine> machines)

        {
            _sNaam = naam;
            _oMachines = new Queue<Machine>(machines);
            _xProductAfgewerkt = false;
            _xProductGestart = false;
        }


        public string Naam
        {
            get { return _sNaam; }
        }

        public Queue<Machine> Machines
        {
            get { return _oMachines; }

        }

        public bool ProductGestart
        {
            get { return _xProductGestart; }
        }

        public bool ProductAfgewerkt
        {
            get { return _xProductAfgewerkt; }
        }


        public bool NogResterendeMachines
        {
            get { return _oMachines.Count > 0; }
        }


        //eerste machine toewijzen bij start
        public Machine Start()
        {
            _xProductGestart = true;
            Machine eerste = _oMachines.Dequeue();
            eerste.PlanIn(this);
            return eerste;
        }

        //volgende machine teruggeveb
        public Machine VolgendeMachine(Machine huidigeMachine)
        {
            huidigeMachine.ProductMomenteelVerwerkt = null;

            if (_oMachines.Count > 0)
            {
                return _oMachines.Dequeue();
            }

            return null;
        }

        //markeer als afgewerkt
        public void Afwerken()
        {
            _xProductAfgewerkt = false;
        }

        //lijst met naam
        public override string ToString()
        {
            return _sNaam;
        }


    }
}
