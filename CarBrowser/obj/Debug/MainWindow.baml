   M S B A M L   `   `   `  ����E  ACarBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CarBrowser
CarBrowser  5   	       CarBrowser.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CarBrowser     5   	   W SHelixToolkit.Wpf, Version=2.5.1.0, Culture=neutral, PublicKeyToken=52aa3500039caf0d(helixhttp://helix-toolkit.org/wpf  5   	   -   5          �Name$  
mainWindow��5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      � -   5         �Name$ 	mainPanel��5
       � ColumnDefinitions 5   
   �� 5      $
��617* �6   �� 5      $
��177* �6   .+�5   
   �� -   5         ��Name$ 	txtSearch��5      $
��Left=�6%   $��31��6@   $��
5.2,10,0,0q�6L    �� TextWrapping$ NoWrap=�6`   $	��Top=�6w   $	��169��6�   $��1��6�   6�   �� 5   
   -   5         ��Name$ listRecentUpload��5      $
��Left=�6-   $	��364��6H   $��
5.2,46,0,0q�6U   $	��Top=�6i   $	��170��6�   $��1��6�   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��20��6A   $��20��6L   $��0,0,10,0q�6X   ��6    �� *ThumbnailPath+6X   �� 5      ��6$   �� *
FileName+5      5      ��5      �� 5       �� 
TargetType" ��6   .�5      -   5        5      5      �� 5    
   -   5        	 ��Name$	 
btnUpModel��5       $��Model��6%   $
��Left=�65   $��
516,10,0,0q�6P   $	��Top=�6d   $��44��6|   $��31��6�   
 � ToolTip$"
 Upload Model from your Drive��6�   5!      �� 5"   
   -   5        $	 btnUpTexture��5"      $��Texture��6'   $
��Left=�69   $��
565,10,0,0q�6T   $	��Top=�6h   $��44��6�   $��31��6�   ���	#FFDDDDDD$$
 Upload Texture from your Drive��6�   6�   �� 5#   
   $��1q�6   
���   �$
��Left=�5%      $	��Top=�5&      $	��599��5'      $	��364��5(      $��	10,46,0,0q�6   ._�5)      �� $��0.2,0.2,-0.4,-1.6q�6   .+�5*      &   HelixToolkit.Wpf.HelixViewport3D  -   5          Name$ 
viewPort3d��5*   (      ZoomExtentsWhenLoaded . $��2��6Y   $	��598��6j   $	��360��6v    �� Left$ -1��6�      Children. 5+        HelixToolkit.Wpf.SunLight  5,      5-      5.      �� 5/   
   $
��Left=�6   $	��100��6/   $��311,-23,0,0q�6<   $	��Top=�6Q   $��28��6i    J� RenderTransformOrigin$ 0.5,0.5&�6t   z�50      V� 51      .~�52      �� �� 53      �� 54       �� Angle$ 90Q�6&   T� 55      56      58      5:      5;      