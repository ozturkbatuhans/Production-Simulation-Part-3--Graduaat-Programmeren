using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Productie_deel3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DispatcherTimer oTimerGUI = new DispatcherTimer();
        List<Product> oProducten = new List<Product>();
        List<Machine> oMachines = new List<Machine>();
        Queue<Machine> oWachtrijMachines;
        Product HuidigProduct;
        Machine ActieveMachine;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LaadGegevens();
            oTimerGUI.Interval = TimeSpan.FromSeconds(1);
            oTimerGUI.Tick += TimerGUI_Tick;
        }

        private void btnProduceer_Click(object sender, RoutedEventArgs e)
        {
            Produceer();
        }

        private void btnVolgendeMachine_Click(object sender, RoutedEventArgs e)
        {
            if (HuidigProduct != null)
            {
                if (ActieveMachine != null)
                {
                    if (HuidigProduct.NogResterendeMachines != false)
                    {
                        Machine VolgendeMachine = HuidigProduct.VolgendeMachine(ActieveMachine);
                        if (VolgendeMachine != null)
                        {
                            VolgendeMachine.PlanIn(HuidigProduct);
                            ActieveMachine = VolgendeMachine;
                        }
                    }
                    else
                    {
                        HuidigProduct.Afwerken();
                        ActieveMachine.ProductMomenteelVerwerkt = null;
                        ActieveMachine = null;
                        HuidigProduct = null;
                        lstMagazijn.Items.Add(HuidigProduct);
                    }
                }
                else
                {
                    MessageBox.Show("er is al een product actief");
                }
            }
            else
            {
                MessageBox.Show("selecteer en start eerst een product");
            }
        }

        private void TimerGUI_Tick(object sender, EventArgs e)
        {
            RefreshGui();
        }


        //methode om de verschillende machines die een product moet doorlopen instellen
        private void LaadGegevens()
        {
            oMachines = new List<Machine>();
            oProducten = new List<Product>();

            //aanmaken van de lijst van machines
            oMachines.Add(new Machine("Machine 1"));
            oMachines.Add(new Machine("Machine 2"));
            oMachines.Add(new Machine("Machine 3"));
            oMachines.Add(new Machine("Machine 4"));
            oMachines.Add(new Machine("Machine 5"));

            //volgorde van machines voor product 1
            oWachtrijMachines = new Queue<Machine>();
            oWachtrijMachines.Enqueue(oMachines[2]);
            oWachtrijMachines.Enqueue(oMachines[0]);
            oWachtrijMachines.Enqueue(oMachines[4]);
            oProducten.Add(new Product("Product 1", oWachtrijMachines));

            //volgorde van machines voor product 2
            oWachtrijMachines = new Queue<Machine>();
            oWachtrijMachines.Enqueue(oMachines[1]);
            oWachtrijMachines.Enqueue(oMachines[0]);
            oWachtrijMachines.Enqueue(oMachines[3]);
            oProducten.Add(new Product("Product 2", oWachtrijMachines));

            oTimerGUI.Start();
        }

        //produceren van een product
        //overlopen van de machines van dit product
        private void Produceer()
        {

            if (lstProducten.SelectedIndex > -1 && ActieveMachine == null)
            {
                HuidigProduct = ((Product)lstProducten.SelectedItem);
                ActieveMachine = HuidigProduct.Start();
            }
            else if (HuidigProduct == null)
            {
                MessageBox.Show("selecteer product");
            }
            else if (ActieveMachine != null)
            {
                MessageBox.Show("werk eerst het huidig product af");
            }

        }

        //update van de listboxen
        //producten die nog moeten afgewerkt worden
        //producten die al afgewerkt zijn en in magazijn zijn
        private void RefreshGui()
        {
            int iHulp = lstProducten.SelectedIndex;

            lstMagazijn.Items.Clear();
            lstProducten.Items.Clear();

            foreach (Product prod in oProducten)
            {
                if (!prod.ProductGestart)
                {
                    lstProducten.Items.Add(prod);
                }
                else if (prod.ProductAfgewerkt)
                {
                    lstMagazijn.Items.Add(prod);
                }

            }

            if ((lstProducten.Items.Count > iHulp))
            {
                lstProducten.SelectedIndex = iHulp;
            }


            KleurPanels();
            InvullenNaamMachine();
        }

        private void InvullenNaamMachine()
        {
            lbl1.Content = oMachines[0].NaamMachine;
            lbl2.Content = oMachines[1].NaamMachine;
            lbl3.Content = oMachines[2].NaamMachine;
            lbl4.Content = oMachines[3].NaamMachine;
            lbl5.Content = oMachines[4].NaamMachine;
        }

        //kleuren van de machines aanpassen
        //wanneer een machine in actief is, wordt de machine in het groen voorgesteld
        //anders wordt de machine in het rood voorgesteld.
        private void KleurPanels()
        {
            if (oMachines[0].Actief)
            {
                rectMachine1.Fill = Brushes.Green;
            }
            else
            {
                rectMachine1.Fill = Brushes.Red;
            }

            if (oMachines[1].Actief)
            {
                rectMachine2.Fill = Brushes.Green;
            }
            else
            {
                rectMachine2.Fill = Brushes.Red;
            }

            if (oMachines[2].Actief)
            {
                rectMachine3.Fill = Brushes.Green;
            }
            else
            {
                rectMachine3.Fill = Brushes.Red;
            }

            if (oMachines[3].Actief)
            {
                rectMachine4.Fill = Brushes.Green;
            }
            else
            {
                rectMachine4.Fill = Brushes.Red;
            }

            if (oMachines[4].Actief)
            {
                rectMachine5.Fill = Brushes.Green;
            }
            else
            {
                rectMachine5.Fill = Brushes.Red;
            }
        }
    }
}