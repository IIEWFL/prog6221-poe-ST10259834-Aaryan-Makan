   M S B A M L   `   `   `  ����   chatbot    chatbot.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$%  Grace - Cybersecurity Assistant��$	��600��$	��850�����#1E1E1E � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ 	CanResize=�.��� $��10q� � ColumnDefinitions �� $��2* ��� $��* � � RowDefinitions �� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$
��Auto=�$��	0,0,10,10q�.���� -    ��Name$ ChatHistoryTextBlock��$��Consolas)�$��14(�$
��Wrap=�
�������$��5q�$��CharacterEllipsis=��� -    ��Name$ UserInputTextBox��$��1��$��0��$��14(�$��0,0,10,5q�$��30��$��5q����#2D2D30
����������#3C3C3C�� -   $��2��$��0��$
��Send��$
��Bold"�$��35��$��0,5,10,0q����#007ACC
����������#3C3C3C � Cursor$
 Hand��� $��0��$��1��$��3�� �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Tasks��
�������$
��Bold"�$��0,0,0,5q��� -    ��Name$ TaskListBox��$��1�����#2D2D30
����������#3C3C3C$	��200��$��0,0,0,10q��� -   $ TaskTitleTextBox��$��2��$��
Task Title��$��14(�$��0,0,0,5q����#2D2D30
����������#3C3C3C�� -   $ TaskDescriptionTextBox��$��3��$��Task Description��$��14(�$��0,0,0,5q����#2D2D30
����������#3C3C3C(  "System.Windows.Controls.DatePicker  -   	  Name$	 ReminderDatePicker��$��4��$��0,0,0,5q����#2D2D30
����������#3C3C3C
 � ToolTip$3
 -Select an optional reminder date for the task�� � 	Resources :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value �� 
TargetType �� Path �� StringFormat �� RelativeSource �� AncestorType �� UpdateSourceTrigger �� Triggers �� Binding �� Value��%�  '        �� "  .���  � �� �#2D2D30��  � ��
 �������  � �� �#3C3C3C��  � ��$ 5q���  � �� �� "  .R��� $ PART_TextBox��#��z��#��z��#��z��#��z������ $ SelectedDate�$ 
MM/dd/yyyy�� �� "  $ PropertyChanged=�.������ " ��.���  � ��
 ����� ��  �� $ SelectedDate� �� "   Z� .a���  � ��$ Select Reminder Date����  � ��
 �����.���� $��5�� �� Orientation$ 
Horizontal=�$��0,0,0,5q�.+��� -    ��Name$ AddTaskButton��$��Add Task��$
��Bold"�$��30��$��0,0,5,0q����#007ACC
��������� -	   $ CompleteTaskButton��$��Complete��$
��Bold"�$��30��$��0,0,5,0q����#007ACC
��������� -
   $ DeleteTaskButton��$��Delete��$
��Bold"�$��30�����#007ACC
��������� $��6��$ 
Horizontal=�.+��� -   $ StartQuizButton��$��
Start Quiz��$
��Bold"�$��30��$��0,0,5,0q����#007ACC
��������� -   $ ShowLogButton��$��Show Log��$
��Bold"�$��30�����#007ACC
�������