   M S B A M L   `   `   `  ���� ?  ;DCOM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DCOMDCOM      DCOM.View.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:DCOM       � WindowStyle$
  None=� � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=� � Title$ MainView��$	��550��$	��800����Z� .���  � ColumnDefinitions �� $	��150 ��� .+��� -    J� Opacity$	 0.8Q� �� CornerRadius$ 	10 0 0 10��
���   �._��� $��0,50,0,0q�
������ 
�������$��0q�$��16(�����  �� 
TargetType" ��.���  �� Property � ��	 �� Value	 �� 
 �� 
TargetType"
 ��.R�� -   }�. #��z��#��z��.+��� $��	Container��#��z�� �� Orientation$ 
Horizontal=�
������ .+��� $��LeftSideRectangle��
�������$��Stretch=�$��5��$y�Hidden=��� $��Center=�$��15,0,0,0q�F� $
��path��
���������@� ellipse�� @� RadiusX$ 0Q� @� RadiusY ��  �� RelativeSource ��  �� Mode$
 Self=� �� Path$ RadiusX� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ LeftSideRectangle�� � y�$	 Visible=��� $ LeftSideRectangle�� � ��	 �	#FF2DFFD9�� $ 	Container�� � ��	 �	#336EF0EAP�  �@��
IsSelected . . ��� $ LeftSideRectangle�� � y�$	 Visible=��� $ 	Container�� � ��	 �	#26999797��  � ��$	 50����  � ��$		 148��.@��� -   $��串口�� �� 
IsSelected . �� -   $��设置���� -   $��关于���� -   $��1��$
 0.88Q�
���   �$ 	0 10 10 0��._��  � RowDefinitions �� $��30 ��� .+��� -   $��30��$��30��$��Right=�$��Center=�   CloseButtonTemplate#���    � -     �Name$ 	pageFrame��$��1��$��5q�  � NavigationUIVisibility$ Hidden=�  � Source$ MainPages/MainPage.xaml?�