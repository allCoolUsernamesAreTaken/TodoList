   M S B A M L   `   `   `  ���� E  ATodoListV1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TodoListV1
TodoListV1      TodoListV1.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:TodoListV1       � Title$  
MainWindow��$	��350��$	��525��.��� ���	#FF2F5F46.+��� -    ��Name$ txtBxIntitule��$
��Left=�$��23��$��	10,10,0,0q� �� TextWrapping$
 Wrap=�$��Intitule��$	��Top=�$	��120���� -   $ 
txtBxDuree��$
��Left=�$��23��$��	10,38,0,0q�$
 Wrap=�$��Durée (en heures)��$	��Top=�$	��120���� -    ��Name$ 
btnAjouter��$��Ajouter��$
��Left=�$	��Top=�$��75��$��	10,66,0,0q��� -    ��Name$ lstVwListeTaches��$
��Left=�$	��209��$��
10,102,0,0q�$	��Top=�$	��497�� J� RenderTransformOrigin$ 0.517,0.702&�$��Disabled=�����  �� Path$ ListeDetaches� �� View �� .P��� -   clmnIntitule��$��	Intitulé�� �� Width$	 100��	 �� DisplayMemberBinding	 �� $ Intitule��� -   	clmnDuree��$��Durée��$	 200��	 �� $ Duree��� -   
clmnStatut��$��Statut��$	 300��	 �� $ Statut�