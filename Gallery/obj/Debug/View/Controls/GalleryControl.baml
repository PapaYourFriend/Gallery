   M S B A M L   `   `   `  ����B  >Gallery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Gallery.View.ControlsGallery.View.Controls  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aeHclr-namespace:Microsoft.Xaml.Behaviors;assembly=Microsoft.Xaml.BehaviorsMicrosoft.Xaml.Behaviors 5      *    $Gallery.View.Controls.GalleryControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Gallery.View.Controls     5      WieHclr-namespace:Microsoft.Xaml.Behaviors;assembly=Microsoft.Xaml.Behaviors     5      $��Arial)�5	   7   $��Medium"�6J   
������ .��5
      �   � RowDefinitions  5   
   �� 5      $��50 �6   6*   �� 5      $��* �6   6)   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��0��6f   .+�5      �� $��Center=�6   $��16(�63   ���#757575
Цена6\   �� 5         ImageButtonSize#��[  6   $'��!/View/Windows/Pics/down-arrow.png��5      $��0,0,40,0q�5      $��Center=�5      $��20��5      $��20��5       � Cursor$
 Hand��5      y�5      �� *PriceFilter+ �� UpdateSourceTrigger$ PropertyChanged=�   BooleanToVisibilityConverter �� 	Converter# [  J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�5   '    �� Command 5   #   ��  �� Path$ 	UpCommand�5   '   5      �� 5      #��[  6   $%��/View/Windows/Pics/up-arrow.png��5      $��0,0,40,0q�5!      $��Center=�5"      $��20��5#      $��20��5$      $
 Hand��5%      y�5       �� *PriceFilter+$ PropertyChanged=� $  BooleanToVisibilityBackConverter# [  5&      d� 5'      $ 	LeftClicke�5(   '    5'   #   �� $ DownCommand�5(   '   5*      �� 5+      -   5        $��typeOf��5+      $��Center=�6%   	 �� 
IsEditable	 . $	��180��6�   $��25��6�   $��14(�6�   
 �� 
IsReadOnly
 . $��Stretch=�6�   $
 Hand��6�   ��6@   �� *Styles+ �� Text 6q   �� *Text+ �� IsDropDownOpen 6�   �� *DataContext.DropOpenStyle+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >�$ PropertyChanged=� J� Effect 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-         
BlurRadius$ 10Q�6'      ShadowDepth$ 3Q�67      Color$ #E0E0E0��6G   6Y   ��5/      �� 50      .R�51      �� $��Center=�6#   $
 Hand��6X   $��Stretch=�6f   ��6>   �� *Style+ d� 	IsChecked 6�   �� *	IsChecked+52      54      �� 55       
 Clicki#��[ 56      
������ $��30,0,0,0q�59      ��55      �� *SearchCommand+��58      Z� .��5:      ��  �� CornerRadius$ 15��6   $	��100��6+   $��30��67   ���#F5F5F5��6X   Z�  5;        5<      $ 10Q�6+   $ 3Q�6;   $ #E0E0E0��6K   6]   ._�5>      �� $��Center=�6'   $��Center=�6D   $��
Поиск��6_   6q   5?      5@      5B      �� 5D   
   -   5         ��Name$ pictures��5D      $��0,10,0,0q�6$    %� 
ScrollUnit$ Pixel=�66   $��Disabled=�6[   $��Hidden=�5E      $��Center=�5F      
������ $	��Top=�5H      $	��Top=�5I      $��Center=�5J      $��1��5L      ��5K      Z�  �� SelectedItem 5M      �� *SelectedItem+ �� Mode$ TwoWay=���5N      �� *
Pictures+$ PropertyChanged=���5O      �� 5P       �� 
TargetType" ��6   .�5Q      ��  �� Property � �� �� Value 5R      �� 5S       �� 
TargetType" ��6.   .R�5T   "   �� $��Bd��5X      #��z��5T   )    �� Padding# z��5U      #��z��5V      #��z��5W       J� SnapsToDevicePixels . ._�5Z   &   �� #��z��67   #��z��5[         �� ContentStringFormat! �� ContentStringFormat#! z  5\       #��z��5]       #��z��5^       # z 5_       5`   #   " �� Triggers" 5a   "   ]� 5b   &   # ]� 
Conditions# 5c   *   �� 5d   .   $ �� Property$ �@��IsSelectionActive% �� Value% .  6^   �� 5e   .   $ �@��
IsSelected% . 6W   ./�5g   *   �� & �� 
TargetName$& Bd��6M    � ��
 ���� 5h      �� 5i   *   $& Bd��6O    � �� 5j   .   ��5k   2   Transparent��6O   5m   +   5n   '   ]� 5o   &   # 5p   *   �� 5q   .   $ �@��IsSelectionActive% . 6^   �� 5r   .   $ �@��
IsSelected% . 6W   ./�5t   *   �� $& Bd��6M    � ��
 ���� 6]   �� 5u   *   $& Bd��6O    � �� 5v   .   ��5w   2   Transparent��6O   5y   +   5z   '   P� 5{   &   ' P� Property' �@J�IsMouseOver( P� Value( . . �5|   *   �� $& Bd��6M    � ��
 ���� 5}      �� 5~   *   $& Bd��6O    � �� 5   .   ��5�   2   Transparent��6O   5�   +   ) N� EnterActions) 5�   *   �� 5�   .   .��5�   2   �� .��5�   6   W� * g� Duration$* 	0:0:0.250D�6F   	+ W� To$	+ 0.8X�6[   , �� TargetProperty$, Opacity�6d   5�   3   5�   /   - N� ExitActions- 5�   *   �� 5�   .   .��5�   2   �� .��5�   6   W� $* 	0:0:0.150D�6F   $+ 1X�6[   $, Opacity�6b   5�   3   5�   /   5�   '   P� 5�   &   ' � {�( .  . �5�   *   �� $& Bd��6S    � �� 5�   .   C� 5�   2   . C� ResourceKey#. Z��6B   5�   +   5�   '   5�      5�      5�      ��5�      �� 5�      .R�5�      �� $ 15q�6   ���#FFFFFF$��15q�6?   $��Center=�6K    5�        5�      $ 15Q�6/   $ 10Q�6?   $ #E0E0E0��6P   6b   ._�5�      �� $ Vertical=�6%   $��Center=�6<   $��Center=�6Y   $
 Hand��6t   .+�5�      �� $
��Fill=�6@   $
 Hand��6g   ��6$   �� *
DataPath+��6O   �� *SizeX+6g   � 5�      $��0,10,0,0q�6#   / � ColumnDefinitions/ 5�   "   �� 5�   &   $��* �67   6C   �� 5�   &   $��40 �67   6D   .+�5�   "   �� $ Vertical=�6-   $
��Left=�6D   $	��Top=�6_   $��0��6w   .+�5�   &   �� ���#F4511E$��15(�6E   ��6S   �� $ PictureName�6w   �� 5�   &   $ 
Horizontal=�61   $��Bottom=�6J   $	��0,5q�6e   .+�5�   *   �� ���#455A64$��13(�6I   $��By��6W   $��0,0,5,0q�6a   6t   �� 5�   *   ���#455A64$��13(�6I   $��0,0,5,0q�6x   ��6W   �� $ 
ArtistName�6�   �� 5�   *   ���#455A64$��13(�6I   ��6W   �� $ ArtistSurname�6}   5�   '   �� 5�   &   ���#9E9E9E$��Italic$�6E   $��11(�6X   ��6f   �� $ 	StyleName�6�   5�   #   �� 5�   "   #��[  6(   $ ��/View/Windows/Pics/buy.png��6Q   $��1��6�   $��25��6�   $��25��6�   $
 Hand��6�   y�6u   �� *	IsOrdered+# [  5�   &   d� 5�   *   $ 	LeftClicke�5�   '    5�   7   �� *DataContext.BuyCommand+0 �� ElementName$0 pictures��1 �� CommandParameter1 6x   �� 5�   '   5�   #   5�      5�      5�      5�      ��5�      �� 5�      .R�5�      � 5�      5�      5�      5�      