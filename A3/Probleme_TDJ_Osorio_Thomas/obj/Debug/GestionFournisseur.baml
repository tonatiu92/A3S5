   M S B A M L   `   `   `  ����U  QProbleme_TDJ_Osorio_Thomas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Probleme_TDJ_Osorio_ThomasProbleme_TDJ_Osorio_Thomas  5   	   3    -Probleme_TDJ_Osorio_Thomas.GestionFournisseur   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Probleme_TDJ_Osorio_Thomas     5   	     � Title$  GestionFournisseur��5   	   $	��450��6$   $	��800��61   .��5	      � .+�5
   
   �� -   5         ��Name$ 	ListFourn��5
      $
��Left=�6'   $	��313��6B   $��	43,66,0,0q�6O   $	��Top=�6b   $	��267��6z   ��6�   �� *fournisseur+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� View 5      �� 5      .P�5      �� $	��Nom��6%    �� Width$ 90��62    �� DisplayMemberBinding 6=   �� *Nom+$ TwoWay=��� 5      $��Vendu��6%   $ 90��64    6?   �� *TypeProduit+$ TwoWay=��� 5      $��A payer��6%   $ 90��66    6A   �� *Achats+$ TwoWay=�5      5      �� 5   
   $��Gestion Fournisseurs��6   $
��Left=�6/   $��56��6J   $��	43,10,0,0q�6V   $	��Top=�6i   $	��332��6�   $��Tempus Sans ITC)�6�   $��36(�6�   �� 5   
   -   5        $	��Nom��5      $
��Left=�6   $��21��68   $��
330,71,0,0q�6D    �� TextWrapping$
 Wrap=�6X   $	��Nom��6l   $	��Top=�6w   $��70��6�   6�   �� 5   
   -   5        $��Produit��5      $
��Left=�6!   $��21��6<   $��
405,71,0,0q�6H   $
 Wrap=�6\   $��Produit��6p   $	��Top=�6   $��70��6�   6�   �� 5   
   -   5        $��Trier Achats��5      $
��Left=�6(   $��20��6C   $��330,108,0,0q�6O   $	��Top=�6d   $��70��6|   6�   �� 5   
   -   5        $��Trier Stock��5      $
��Left=�6'   $��20��6B   $��375,211,0,0q�6N   $	��Top=�6c   $��70��6{   6�   �� 5   
   -   5        $��
Rechercher��5      $
��Left=�6&   $��20��6A   $��405,108,0,0q�6M   $	��Top=�6b   $��70��6z   6�   �� 5   
   -   5        $
��Left=�5      $��21��6-   $��330,151,0,0q�69   $
 Wrap=�6N   $��Quantite��6b   $	��Top=�6r   $��70��6�   6�   �� 5   
   -   5        $��Acheter��5      $
��Left=�6#   $��20��6>   $��405,152,0,0q�6J   $	��Top=�6_   $��70��6w   6�   �� 5   
   -	   5        $ Stock��5      $
��Left=�6"   $	��143��6=   $��375,236,0,0q�6J   $	��Top=�6_   $	��221��6w   ��6�   �� *stock+$ TwoWay=�$ PropertyChanged=� 5      �� 5      .P�5      �� $��Stock��6%   $ 90��64    6?   �� *Nom+$ TwoWay=��� 5      $��Quantite��6%   $ 90��67    6B   �� *
Quantite+$ TwoWay=�5       5"      5$      5%      