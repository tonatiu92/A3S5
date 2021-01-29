using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class MaPizzeria
    {

        List<Commis> employe_c;
        List<Livreur> employe_l;
        SortedList<int, Commande> historique;
        SortedList<string, Client> fichier_client;
        List<Produit> menu;
        public event EventHandler<Commande> NouvelleCommandeAppproved;

        public MaPizzeria(string fichier_c, string fichier_l, string commande, string fichier_cli, string Menu)
        {
            employe_c = new List<Commis>();
            employe_l = new List<Livreur>();
            historique = new SortedList<int, Commande>();
            fichier_client = new SortedList<string, Client>();
            menu = new List<Produit>();
            Lecture_Fichier_Commis(fichier_c);
            Lecture_Fichier_Livreur(fichier_l);
            Lecture_Fichier_Commande(commande);
            Lecture_Fichier_Client(fichier_cli);
            Lecture_Fichier_Menu(Menu);

        }

        #region Propriété
        public List<Commis> EmployeC
        {
            get
            {
                return employe_c;
            }
            set
            {
                employe_c = value;
            }
        }
        public List<Livreur> EmployeL
        {
            get
            {
                return employe_l;
            }
            set
            {
                employe_l = value;
            }
        }
        public SortedList<int, Commande> Historique
        {
            get
            {
                return historique;
            }
            set
            {
                historique = value;
            }
        }
        public SortedList<string, Client> FichierClient
        {
            get
            {
                return fichier_client;
            }
            set
            {
                fichier_client = value;
            }
        }
        public List<Produit> Menu
        {
            get
            {
                return menu;
            }
            set
            {
                menu = value;
            }
        }
        #endregion

        #region LectureFichier

        /// <summary>
        /// Lit un fichier csv pour les commis
        /// </summary>
        /// <param nom du fichier="fichier"></param>
        public void Lecture_Fichier_Commis(string fichier)
        {
            StreamReader st = null;

            try
            {
                st = new StreamReader(fichier);
                string line = null;

                while ((line = st.ReadLine()) != null)

                {
                    string[] com = line.Split(';');
                    bool conge = false;
                    if (com[4] != "surplace")
                    {
                        conge = true;
                    }
                    Commis comTemp = new Commis(com[0], com[1], conge, com[2], com[3], Convert.ToDateTime(com[5]));
                    employe_c.Add(comTemp);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            { if (st != null) st.Close(); }

        }

        /// <summary>
        /// Lit un fichier pour les livreurs
        /// </summary>
        /// <param nom du fichier ="fichier"></param>
        public void Lecture_Fichier_Livreur(string fichier)
        {
            StreamReader st = null;

            try
            {
                st = new StreamReader(fichier);
                string line = null;

                while ((line = st.ReadLine()) != null)

                {
                    string[] liv = line.Split(';');
                    bool conge = false;
                    bool route = false;
                    if (liv[4] == "enconges")
                    {
                        conge = true;
                    }
                    else if (liv[4] == "enlivraison")
                    {
                        route = true;
                    }
                    Livreur livTemp = new Livreur(liv[0], liv[1], conge, liv[2], liv[3], route, liv[5]);
                    employe_l.Add(livTemp);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            { if (st != null) st.Close(); }

        }
       
        /// <summary>
        /// Lit un fichier pour les commandes
        /// </summary>
        /// <param nom du fichier="fichier"></param>
        public void Lecture_Fichier_Commande(string fichier)
        {

            StreamReader st = null;

            try
            {
                st = new StreamReader(fichier);
                string line = null;
                int i = 0;
                while ((line = st.ReadLine()) != null)
                {
                    string[] com = line.Split(';');
                    if (i != 0)
                    {
                        com[1] = com[1].ToUpper();
                        Commande commandeTemp = new Commande(Convert.ToInt32(com[0]), Convert.ToInt32(com[1].Replace("H", String.Empty)), Convert.ToDateTime(com[2]), com[3], com[4], com[5], com[6], com[7]);
                        historique.Add(Convert.ToInt32(com[0]), commandeTemp);
                    }
                    i++;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            { if (st != null) st.Close(); }

        }

        /// <summary>
        /// Lit un fichier pour les clients
        /// </summary>
        /// <param nom du fichier ="fichier"></param>
        public void Lecture_Fichier_Client(string fichier)
        {
            StreamReader st = null;

            try
            {
                st = new StreamReader(fichier);
                string line = null;
                while ((line = st.ReadLine()) != null)
                {
                    string[] cli = line.Split(';');
                    Client clientTemp = new Client(Convert.ToInt32(cli[0]), cli[1], cli[2], cli[3], cli[4], DateTime.Now);
                    fichier_client.Add(cli[4], clientTemp);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            { if (st != null) st.Close(); }

        }

        /// <summary>
        /// Lit un fichier pour le Menu
        /// </summary>
        /// <param nom du fichier ="fichier"></param>
        public void Lecture_Fichier_Menu(string fichier)
        {
            StreamReader st = null;

            try
            {
                st = new StreamReader(fichier);
                string line = null;

                while ((line = st.ReadLine()) != null)

                {
                    string[] produit = line.Split(';');
                    Produit disponible;
                    if (produit[0] == "pizza")
                    {
                        disponible = new Pizza(produit[0], Convert.ToInt64(produit[2]), produit[1]);
                    }
                    else
                    {
                        disponible = new Boisson(produit[0], Convert.ToInt64(produit[2]), produit[1]);
                    }
                    menu.Add(disponible);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            { if (st != null) st.Close(); }
        }
        #endregion

        public string NouvelleCommande(string commis, List<Produit> commande, Client traite)
        {
            Commis respo = EmployeC.Find(x => x.ToString() == commis);
            List<Livreur> dispo = EmployeL.FindAll(x => (x.Etat_Conge == false) && (x.Route == false));
            if ((dispo.Count == 0) || (dispo == null))
            {
                    return "Aucun Livreur Disponible";
            }
            else
            {
                if (respo != null)
                {
                    Facture delivre = new Facture(commande);
                    int last = MainWindow.creation.Historique.Values[MainWindow.creation.Historique.Count - 1].Numero;
                    Commande nouvelle = new Commande(last + 1, DateTime.Now.Hour, DateTime.Now, traite.Numero, respo.Nom, dispo[0].Nom, "en cours de preparation", "attente de paiement", delivre);
                    MainWindow.creation.Historique.Add(last + 1, nouvelle);
                    // MainWindow.MainRefresh();
                    NouvelleCommandeAppproved?.Invoke(this, nouvelle);
                    return " La somme a payée est: " + nouvelle.Referente.Solde + " et le responsable est: " + respo.Nom;



                }
                else
                {
                    return "Renseignez le nom du commis responsable par la commande";
                }
            }
        }
        public Pizza RechercheMenuPizza(string nom)
        {
            foreach (Produit element in menu)
            {
                if(element is Pizza)
                {
                    if(element.ToString() == nom)
                    {
                        return (Pizza) element;
                    }
                }
            }
            return null;
        }
        public Boisson RechercheMenuBoisson(string nom)
        {
            foreach (Produit element in menu)
            {
                if (element is Boisson)
                {
                    if (element.ToString() == nom)
                    {
                        return (Boisson)element;
                    }
                }
            }
            return null;
        }
    }
}
