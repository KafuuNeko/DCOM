   M S B A M L   `   `   `  ���� ?  ;DCOM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DCOM.View.MainPagesDCOM.View.MainPages  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity "    DCOM.View.MainPages.MainPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:DCOM.View.MainPages     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity       N� Title$  MainPage�� � 	Resources  �� 
TargetType   for_scrollbar �� BasedOn��%   '��       7   �� " ��8   .���  � RowDefinitions �� $��2* ��� $��1* ��� $
��auto ��� $
��auto �.+��  �� $
��auto ���  � ColumnDefinitions �� �� .+���  �� Orientation$ 
Horizontal=�$��0��$��0��$��Center=�.+��� ���	#99FFFFFF$��Center=�$��Center=�$��接收数据���� $��0�����	#FF050923 �� 
IsEditable .  
������� �� 
IsReadOnly .  $��5 0 0 0q�  ComboBoxDefaultStyle#���  	 �� SelectedIndex	 �� *ReceiveDisplayType+.@��� $��十六进制���� $��文本显示���� $��0��$��1�����	#99FFFFFF$��Center=�$��Center=�$��发送数据���� -   $��1��$��0��  TextBoxDefaultStyle#���  
 �� 
CaretBrush

 ����� �� AcceptsReturn . 
�������
������  �� TextWrapping$
 Wrap=�$��5q�$��10(�" �� VerticalScrollBarVisibility$ Visible=����� *ReceiveData+�� -   $��1��$��1��#���  

 ����� . 
�������
������ $
 Wrap=�$��5q�$ Visible=����� *SendDataLog+�� -   $��1��#���  

 ����� . 
�������
������ $
 Wrap=�$��5q�$ Visible=����� *	LogText+� $��2�� �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��0��
������ $��30��$��5q�  DefaultButtonTemplate#��[ 
����������� *OpenOrCloseButtonContent+���� *OpenOrCloseCommand+�� $��1��
������ $��30��$��5q�#��[ $��清空数据��
����������� *ClearOutputCommand+�� $��2��
������ $��30��$��5q�#��[ $��接收数据转文件��
����������� *ReceiveDataSaveFileCommand+�� $��3��
������ $��30��$��5q�#��[ $��发送文件��
����������� *SendFileCommand+�� $��4��$ 
Horizontal=�.+��� 
�������$��5q�$��Center=�$��接收���� $��0��
�������$��5q�$��Center=����� *NumberBytesReceived+�� $��5��$ 
Horizontal=�.+��� 
�������$��5q�$��Center=�$��发送���� $��0��
�������$��5q�$��Center=����� *NumberBytesSend+� $��3�� �� $��1* ��� $��1* �.+��� -   #���  

 ����� . 
�������
������ $
 Wrap=�$	��150��$��5q�$ Visible=����� *SendDataText+� $��2�� �� �� .+��� $��0�����	#FF050923 .  
�������$��30�� . #���  	 �� *SendDataType+.@��� $��文本发送���� $��十六进制���� $��1��
������ 
�������$��30��$��3q�#��[ $��发送������ *SendDataCommand+