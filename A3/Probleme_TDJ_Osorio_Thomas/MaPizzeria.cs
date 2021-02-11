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
        #region attribut
        List<Commis> employe_c;
        List<Livreur> employe_l;
        SortedList<int, Commande> historique;
        SortedList<string, Client> fichier_client;
        List<Produit> menu;
        List<Produit> stock;
        List<Fournisseur> partenaire;
        #endregion

        #region Events
        public event EventHandler<Commande> NouvelleCommandeAppproved;
        public event EventHandler<Client> NouveauClientApproved;
        public event EventHandler<Commis> NouveauCommisApproved;
        public event EventHandler<Commis> SuppressionCommis;
        public event EventHandler<Livreur> NouveauLivreurApproved;
        public event EventHandler<Livreur> SuppressionLivreur;
        public event EventHandler<Client> SuppressionClient;
        #endregion

        /// <summary>
        /// Initialise la pizzeria
        /// </summary>
        /// <param fichier commis="fichier_c"></param>
        /// <param fichier livreur="fichier_l"></param>
        /// <param commande="commande"></param>
        /// <param fichier client="fichier_cli"></param>
        /// <param menu ="Menu"></param>
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
            Initialisation_Stock();

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
        public List<Fournisseur> Partenaire
        {
            get
            {
                return partenaire;
            }
            set
            {
                partenaire = value;
            }
        }

        public List<Produit> Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
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
                Random r = new Random();
                foreach(Commande element in historique.Values)
                {
                    element.Referente = new Facture();
                    element.Referente.Solde = r.Next(0, 100);
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
                        disponible = new Pizza( Convert.ToInt64(produit[2]), produit[1]);
                    }
                    else
                    {
                        disponible = new Boisson(Convert.ToInt64(produit[2]), produit[1]);
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

        /// <summary>
        /// Initialise le stock;
        /// </summary>
        public void Initialisation_Stock()
        {
            string nom1 = "Boucher";
            string nom2 = "Epicier";
            string nom3 = "Fromager";

            Viande jambon = new Viande(10, "jambon", 10);
            Viande hache = new Viande(10, "viande hache", 10);
            Viande peperoni = new Viande(10, "peperoni", 10);
            List<Produit> viande = new List<Produit> { jambon, hache, peperoni };
            Legume tomate = new Legume(10, "tomate", 10);
            Legume poivron = new Legume(5, "poivron", 10);
            Legume olive = new Legume(5, "olive", 10);
            List<Produit> legume = new List<Produit> { tomate, poivron, olive };
            Fromage gruyere = new Fromage(20,"gruyere" ,10) ;
            Fromage parmesan = new Fromage(20, "parmesan", 10);
            Fromage mozzarela = new Fromage(20, "mozzarela", 10);
            List<Produit> fromage = new List<Produit> { gruyere, parmesan, mozzarela };

            stock = new List<Produit> { new Viande(10, "jambon", 100), new Viande(10, "viande hache", 100), new Viande(10, "peperoni", 100),
                                        new Legume(10, "tomate", 100), new Legume(5, "poivron", 100), new Legume(5, "olive", 100),
                                        new Fromage(20,"gruyere" ,100), new Fromage(200, "parmesan", 100), new Fromage(20, "mozzarela", 100)};
            Fournisseur boucher = new Fournisseur(nom1, viande, "viande");
            Fournisseur epicier = new Fournisseur(nom2, legume, "legume");
            Fournisseur fromager = new Fournisseur(nom3, fromage, "fromager");

            boucher.Vendu.ForEach(x => x.Quantite = 10000);
            epicier.Vendu.ForEach(x => x.Quantite = 10000);
            fromager.Vendu.ForEach(x => x.Quantite = 100000);

            partenaire = new List<Fournisseur> { boucher, epicier, fromager };

        }
        #endregion

        #region Gestion Client

        /// <summary>
        /// Création d'un nouveau Client
        /// </summary>
        /// <param nom du client="nom"></param>
        /// <param prénom du client="prenom"></param>
        /// <param adresse ="adresse"></param>
        /// <param numéro du client="numero"></param>
        /// <returns>le résultat de la tentative de création</returns>
        public string NouveauClient(string nom, string prenom, string adresse, string numero)
        {
            int last = this.fichier_client.Values.Last().Id;
            Client cree = new Client(last + 1, nom, prenom, adresse, numero, DateTime.Now);
            NouveauClientApproved?.Invoke(this, cree);
            return "nouveau client enregistré";
        }

        /// <summary>
        /// Recherche un client dans le fichier dédié à partir de son nom
        /// </summary>
        /// <param nom du client="nom"></param>
        /// <returns>le client recherché</returns>
        public Client RechercheClient(string nom)
        {
            foreach(Client element in fichier_client.Values)
            {
                if(element.ToString() == nom)
                {
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Supprimer le client du fichier
        /// </summary>
        /// <param le client="a"></param>
        /// <returns>le résultat de la suppression du client</returns>
        public string SupprimeClient(Client a)
        {
            SuppressionClient?.Invoke(this, a);
            return "Client supprimer";
        }

        /// <summary>
        /// Calcul la moyenne des montants cumulés
        /// </summary>
        /// <returns>la moyenne</returns>
        public float MoyenneMontants()
        {
            float somme = 0f;
            foreach (Client element in fichier_client.Values)
            {
                somme += element.Depense;
            }
            return somme / ((float)fichier_client.Count);
        }
        #endregion

        #region Gestion Commande

        /// <summary>
        /// Une nouvelle commande est enregistré.(Un livreur tant qu'il n'est pas parti en Livraison peut recevoir des commandes)
        /// </summary>
        /// <param nom  du commis responsable="commis"></param>
        /// <param la liste des produits commandés="commande"></param>
        /// <param le client traité="traite"></param>
        /// <returns>le message de validation ou non</returns>
        public string NouvelleCommande(string commis, List<Produit> commande, Client traite)
        {
            Commis respo = EmployeC.Find(x => x.ToString() == commis);
            List<Livreur> dispo = EmployeL.FindAll(x => (x.Etat_Conge == false) && (x.Route == false));
            if ((dispo.Count == 0) || (dispo == null))
            {
                    return "Aucun Livreur Disponible, engagé de nouveau livreur";
            }
            else
            {
                if (respo != null)
                {
                   
                    Facture delivre = new Facture(commande);
                    int last = this.historique.Keys.Last();
                    respo.NbCommandes+=1;
                    Commande nouvelle = new Commande(last + 1, DateTime.Now.Hour, DateTime.Now, traite.Numero, respo.Nom, dispo[0].Nom, "en cours de preparation", "attente de paiement", delivre);
                    dispo[0].Traite = nouvelle;
                    NouvelleCommandeAppproved?.Invoke(this, nouvelle);
                    traite.Depense += nouvelle.Referente.Solde;
                    stock.ForEach(x => x.Quantite -= nouvelle.Referente.CounterPizza()) ; 
                    return " La somme a payée est: " + nouvelle.Referente.Solde + ", le responsable est: " + respo.Nom + " et le livreur est: " + dispo[0].Nom ;
                }
                else
                {
                    return "Renseignez le nom du commis responsable par la commande";
                }
            }
        }

        /// <summary>
        /// Renvoie une commande à partir d'un string correspondant à son affichafe
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        /// 
        public Commande RechercheCommande(string select)
        {
            foreach(Commande element in historique.Values)
            {
                if(element.ToString() == select)
                {
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Ajoute une pizza dans le panier de la commande
        /// </summary>
        /// <param l'item selectionné="select"></param>
        /// <param la taille de l'item ="taille"></param>
        /// <param la liste du panier="commande"></param>
        /// <returns>la pizza ajoute</returns>
        public Pizza AjoutePizza(string select, string taille, List<Produit> commande)
        {
            Pizza recher = new Pizza(menu.Find(x => x.ToString() == select), (Pizza)menu.Find(x => x.ToString() == select));
            if (recher.ToString() == select)
            {
                recher.Taille = Convert.ToInt32(taille);
                recher.CalculPrix();
                Pizza exist = (Pizza)commande.Find(x => { Pizza val = null; if (x is Pizza) { val = (Pizza)x; } if (val != null) { return (val.Type == recher.Type)&&(val.Taille == recher.Taille); } else { return false; } }); 
                if (exist is null)
                {
                    commande.Add(recher);
                    return (Pizza)commande.Last();
                }
                else
                {
                    int i = commande.IndexOf(exist);
                    commande[i].Quantite += 1;
                    commande[i].Prix += recher.Prix;
                    return (Pizza)commande[i];
                }

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ajoute une boisson dans le panier de la commande
        /// </summary>
        /// <param le type de boisson="select"></param>
        /// <param la taille de la boisson="taille"></param>
        /// <param la liste du panier="commande"></param>
        /// <returns>la boisson ajoutée</returns>
        public Boisson AjouteBoisson(string select, string taille, List<Produit> commande)
        {
            Boisson recher = new Boisson(menu.Find(x => x.ToString() == select), (Boisson)menu.Find(x => x.ToString() == select));
            if (recher.ToString() == select)
            {
                recher.Taille = Convert.ToInt32(taille);
                recher.CalculPrix();
                Boisson exist = (Boisson)commande.Find(x => { Boisson val = null; if (x is Boisson) { val = (Boisson)x; } if (val != null) { return (val.Type == recher.Type) && (val.Taille == recher.Taille); } else { return false; } }); 
                if (exist is null)
                {
                    commande.Add(recher);
                    return (Boisson) commande.Last();
                }
                else
                {
                    int i = commande.IndexOf(exist);
                    commande[i].Quantite += 1;
                    commande[i].Prix += recher.Prix;
                    return (Boisson) commande[i];
                }

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Permet d'encaisser un paiement
        /// </summary>
        /// <param numero de la commmande ="numeroC"></param>
        /// <param numero du livreur="numeroL"></param>
        /// <param succès de livraison="reponse"></param>
        /// <returns>Message d'action</returns>
        public string Paiement(int numeroC, string numeroL, bool reponse)
        {
            if (historique.ContainsKey(numeroC))
            {
                
                Livreur charge = employe_l.Find(x => x.Numero == numeroL);
                charge.NbLivraisons += 1;
                if (charge != null)
                {
                    charge.Route = false;
                    if (reponse)
                    {
                        historique[numeroC].Etat = "fermée";
                        historique[numeroC].EtatSolde = "ok";
                    }
                    else
                    {
                        historique[numeroC].Etat = "fermée";
                        historique[numeroC].EtatSolde = "perdu";
                    }
                    return "Paiement effectué";
                }
                else
                {
                    return "Le numéro du livreur n'est pas le bon";

                }
            }
            else
            {
                return "Cette commande n'existe pas";
            }
        }

        /// <summary>
        /// Calcul la moyenne des Commandes réalisés
        /// </summary>
        /// <returns>la valeur moyenne</returns>
        public float MoyenneCommande()
        {
            float somme = 0f;
            foreach(Commande livre in historique.Values)
            {
                if ((livre.Referente != null)&&(livre.Referente.Solde!=0))
                {
                    somme += livre.Referente.Solde;
                }
            }
            return somme /= historique.Count;
        }

        /// <summary>
        /// Partie Innovation: tri les pizzas par ordre de préférences
        /// </summary>
        /// <returns></returns>
        public string PizzaPref()
        {
            SortedList<string, int> favoris = new SortedList<string, int>();
            foreach (Commande element in historique.Values)
            {
                if (element.Referente.ListeProduits != null)
                {
                    foreach (Produit commande in element.Referente.ListeProduits)
                    {
                        if(commande is Pizza)
                        {
                            Pizza val = (Pizza)commande;
                            if (favoris.Keys.Contains(val.Type))
                            {
                                favoris[val.Type] += commande.Quantite;
                            }
                            else
                            {
                                favoris.Add(val.Type, commande.Quantite);
                            }
                        }
                    }
                }
            }
            string affiche = "";
            List<int> tri = favoris.Values.ToList();
            tri.Sort();
            tri.ForEach(x => affiche += favoris.Keys[favoris.IndexOfValue(x)] + " x" + x + "\n");
            return affiche;
        }
        #endregion

        #region Gestion Commis

        /// <summary>
        /// Ajoute un commis à la pizzeria
        /// </summary>
        /// <param le commis ajouté="a"></param>
        /// <returns>le résultat de l'ajout</returns>
        public string AjouterCommis(Commis a)
        {
           // employe_c.Add(a);
            NouveauCommisApproved?.Invoke(this, a);
            return "Nouveau Commis enregistré";
        }

        /// <summary>
        /// Supprime un commis de la pizzeria
        /// </summary>
        /// <param Commis supprimer="select"></param>
        /// <returns>résultat de la suppression</returns>
        public string SupprimeCommis(Commis select)
        {
            SuppressionCommis?.Invoke(this, select);
            return "Commis Supprimer";
        }

        /// <summary>
        /// Recherche un commis à partir des caractéristiques du commis
        /// </summary>
        /// <param caractéristiques du commis="nom"></param>
        /// <returns>Commis recherché</returns>
        public Commis RechercheCommis(string nom)
        {
            foreach (Commis element in employe_c)
            {
                if (element.ToString() == nom)
                {
                    return element;
                }
            }
            return null;
        }
        #endregion

        #region Gestion Livreur

        /// <summary>
        /// Ajoute un Livreur
        /// </summary>
        /// <param livreur ajouté="a"></param>
        /// <returns>résultat de l'ajout</returns>
        public string AjouterLivreur(Livreur a)
        {
            NouveauLivreurApproved?.Invoke(this, a);
            return "Nouveau Livreur enregistré";
        }

        /// <summary>
        /// Supprime Livreur de la pizzeria
        /// </summary>
        /// <param Livreur selectionné="select"></param>
        /// <returns>résultat de la suppression</returns>
        public string SupprimeLivreur(Livreur select)
        {
            SuppressionLivreur?.Invoke(this, select);
            return "Livreur Supprimer";
        }

        /// <summary>
        /// Recherche le livreur à parit de ses caractéristiques
        /// </summary>
        /// <param les caractéristiques du livreur="nom"></param>
        /// <returns>le livreur recherché</returns>
        public Livreur RechercheLivreur(string nom)
        {
            foreach (Livreur element in employe_l)
            {
                if (element.ToString() == nom)
                {
                    return element;
                }
            }
            return null;
        }
        #endregion

        #region Gestion Fournisseur

        /// <summary>
        /// Fonction pour acheter un produit
        /// </summary>
        /// <param item selectionne="choisi"></param>
        /// <param le nombre d'unité acheté="number"></param>
        /// <returns></returns>
        public string Achete(string choisi, int number)
        {
            Fournisseur select = null;
            foreach (Fournisseur element in partenaire)
            {
                if (choisi == element.ToString())
                {
                    select = element;
                }
            }
            select.Vendu.ForEach(x => { if (x.GetType() == select.Vendu[0].GetType()) { x.Quantite -= number; select.Achats += x.Prix*number; } });
            stock.ForEach(x => { if (x.GetType() == select.Vendu[0].GetType()) x.Quantite += number; });
            return "achats confirmé";
        }
        #endregion

    }
}
