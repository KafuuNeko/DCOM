   M S B A M L   `   `   `  ����?  ;DCOM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DCOMDCOM  5   	       DCOM.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:DCOM     5   	     � WindowStyle$
  None=�5       � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � Title$ MainView��5	   	   $	��550��6   $	��800��6'   ��5   -   Z� .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      .+�5   
   �� -   5         J� Opacity$	 0.8Q�5       �� CornerRadius$ 	10 0 0 10��6   
���   �._�5      �� $��0,50,0,0q�6   
������ 
�������$��0q�6T   $��16(�6h   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � ��	 �� Value	 5      �� 5   "   
 �� 
TargetType"
 ��62   .R�5   &   � -   5        }�. #��z��5   n   #��z��6�   .+�5   *   �� $��	Container��6�   #��z��65    �� Orientation$ 
Horizontal=�6W   
������ .+�5   .   �� $��LeftSideRectangle��6   
�������$��Stretch=�6E   $��5��6a   $y�Hidden=�6k   6   �� 5   .   $��Center=�6?   $��15,0,0,0q�6Z   5   +   F� 5   *   $
��path��6<   
���������5    .   @� 5!   2   ellipse�� @� RadiusX$ 0Q�6S    @� RadiusY 6_   ��  �� RelativeSource ��  �� Mode$
 Self=� �� Path$ RadiusX�5#   +   5$   '    �� Triggers 5&   &   P� 5'   *    P� Property �@J�IsMouseOver P� Value . . �5(   .   ��  �� 
TargetName$ LeftSideRectangle��65    � y�$	 Visible=�6j   �� 5)   .   $ LeftSideRectangle��65    � ��	 �	#FF2DFFD96d   �� 5*   .   $ 	Container��65    � ��	 �	#336EF0EA6b   5+   +   P� 5-   *    �@��
IsSelected . . �5.   .   �� $ LeftSideRectangle��65    � y�$	 Visible=�6j   �� 5/   .   $ 	Container��65    � ��	 �	#269997976b   50   +   53   #   55      �� 57       � ��$	 50��63   �� 58       � ��$		 148��62   59      .@�5<      �� -   5        $��串口��5<       �� 
IsSelected . 6=   �� 5=      -   5        $��设置��5=      6+   �� 5>      -   5        $��关于��5>      6+   5@      5A      �� 5D   
   -   5        $��1��5D      $
 0.88Q�6!   
���   �$ 	0 10 10 0��6C   ._�5F      �  � RowDefinitions 5G      �� 5H      $��30 �6$   �� 5I      .+�5L      �� -   5        $��30��5L      $��30��6%   $��Right=�60   $��Center=�6L      CloseButtonTemplate#���   6g   6�    � 5N      -   5          �Name$ 	pageFrame��5P      $��1��5N      $��5q�5O        � NavigationUIVisibility$ Hidden=�5S        � Source$ MainPages/MainPage.xaml?�5T      6;   5U      5W      5Y      5Z      