   M S B A M L   `   `   `  ���� ?  ;DCOM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DCOM.View.MainPagesDCOM.View.MainPages  %    DCOM.View.MainPages.SettingPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:DCOM.View.MainPages       N� Title$  SettingPage�� � 	Resources  �� 
TargetType   for_scrollbar �� BasedOn��%   '��       7   �� " ��8   .���� .����  �� Orientation$ Vertical=�.+��� $��串口属性��
�������$��20(�$
��Bold"�$��5 25 0 0q��� $ Vertical=�$��5 0 0 0q�.+��� $��10q�$%��启用请求发送 (RTS) 信号�� J� Opacity$	 0.8Q�$��15(�
������� d� 	IsChecked �� *	RtsEnable+�� $��串口��
�������$��15(�$��10q��� $ 
Horizontal=�.+���  �� 
IsEditable . $��30�� �� 
IsReadOnly .  $��16(�$��Center=�$
��Left=�$	��100��$��5 0 0 0q�$	 0.5Q����� *ComConfig.ComName+	 �� Text	 �� *	ComName+�� $��刷新��$��50��$��10 0 0 0q�  DefaultButtonTemplate#��[ 
����������� *RefreshSerialPortListCommand+�� $��	波特率��
�������$��15(�$��10q���  . $��30�� .  $��16(�$��Center=�$
��Left=�$	��100��$��5 0 0 0q�$	 0.5Q����� *ComConfig.BaudRate+	 �� *
BaudRate+�� $��	停止位��
�������$��15(�$��10q��� $��16(�$��Center=�$
��Left=�$��30��$	��100��$��5 0 0 0q�$	 0.5Q����� *ComConfig.StopBit+	 �� *	StopBit+�� $��校验模式��
�������$��15(�$��10q��� $��16(�$��Center=�$
��Left=�$��30��$	��100��$��5 0 0 0q�$	 0.5Q����� *ComConfig.CheckMode+	 �� *Parity+�� $��	数据位��
�������$��15(�$��10q��� $��16(�$��Center=�$
��Left=�$��30��$	��100��$��5 0 0 0q�$	 0.5Q����� *ComConfig.DataBits+	 �� *
DataBits+�� $��字符编码��
�������$��20(�$
��Bold"�$��5 25 0 0q��� $ Vertical=�$��5 0 0 0q�.+��� $��接收数据解码编码��
�������$��15(�$��10q��� $��16(�$��Center=�$
��Left=�$��30��$	��100��$��5 0 0 0q�$	 0.5Q����� *EncodingName+	 �� *ReceiveDataEncoding+�� $��发送数据字符编码��
�������$��15(�$��10q��� $��16(�$��Center=�$
��Left=�$��30��$	��100��$��5 0 0 0q�$	 0.5Q����� *EncodingName+	 �� *SendDataEncoding+�� $��显示限制��
�������$��20(�$
��Bold"�$��5 25 0 0q��� $ Vertical=�$��5 0 0 0q�.+��� $��最大显示字节数��
�������$��15(�$��10q��� $	 0.5Q�$��Center=�$
��Left=�$��5 0 0 0q�$��30��$��16(�$	��100������ *MaxShowByteCount+