   M S B A M L   `   `   `  ����C  ?shipTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:shipTestshipTest  5   	       shipTest.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:shipTest     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5	   	     � Title$  
MainWindow��5   	   $��442.715��6   $��650.488��6-    � 
ResizeMode$ CanMinimize=�6=   .��5
      �� -   5         ��Name$ myCanvas��5
      
���   �.+�5   
   �� -   5         ��Name$
 ship��5      $
��Left=�6   $��62��69   $	��Top=�6E   $��61��6]   $��Ressources/shipB.png��6h    �� Left$	 292��6�   
 �� Top$	 317��6�    K� ZIndex$ 1��6�   �� 5   
   -   5         ��Name$ scoreLb��5      $�� ��6!   $	 114��6,   $ 21��6>   $	��102��6N   $��36��6Z   $��	Yu Gothic)�6f   ���	#FFF5F4F4
���   �$��16(�6�   �� 5   
   -   5        $ bgImg��5      "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ background.gif��6   $	��100��6Q   $	��100��6^   	 J� RenderTransformOrigin$	 0.428,-0.28&�6j   $
��Fill=�6�   �� 5   
   -   5        $ menuBg��5      $	��100��6    $
 1039��6-   $	��135��6@   $��Ressources/mainMenuBg.jpg��6L   $��UniformToFill=�6o   $ 21��6�   �� 5   
   -   5        $ scoreImg��5      $	��100��6"   $
 1074��6/   $	 126��6B   $	��100��6S   $��Ressources/scoreImg.png��6_   �� 5   
   -   5        $ pauseImg��5      $	��100��6"   $
 1074��6/   $	 231��6B   $	��100��6S   $��Ressources/pauseImg.png��6_   �� 5   
   -   5        $ lifeImg��5      $	��100��6!   $	 279��6.   $	��100��6?   $��Ressources/lifeImg.png��6K   $
 1074��6k   �� 5   
   -	   5        
 ��Name$
 start��5      $�� ��6    $ 70��6+   $	 105��6<   $	��105��6M   $��40��6Y   |�. $��1��6�   4  .System.Windows.Automation.AutomationProperties   	AccessKey$ s��6�    J� IsHitTestVisible .  J� 	AllowDrop . ��5      �� 5       �� ImageSource$ Ressources/startMenu.png��6    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -
   5        $
 scores��5      $�� ��6!   $ 70��6,   $	 171��6=   $	��105��6N   $��40��6Z   |�. $��2��6w   ��5      �� 5      $ Ressources/scoreImg.png��6   5      �� 5   
   -   5        $
 credit��5      $�� ��6!   $ 70��6,   $	 230��6=   $	��105��6N   $��40��6Z   |�. $��3��6w   ��5       �� 5!      $ Ressources/creditsPic.png��6   5#      �� 5$   
   -   5        $

 exit��5$      $�� ��6   $ 70��6*   $	 292��6;   $	��105��6L   $��40��6X   |�. $��4��6u   ��5%      �� 5&      $ Ressources/exitMenu.png��6   5(      �� 5)   
   -   5        $ gameOverImg��5)      $	��100��6%   $	 186��62   $	��100��6C   $ ��Ressources/gameOverImg.png��6O   $	 951��6s   5,      5-      