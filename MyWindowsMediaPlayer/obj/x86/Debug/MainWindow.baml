   M S B A M L   `   `   `  ����  MyWindowsMediaPlayer%    MyWindowsMediaPlayer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��MyWindow��5   ?     � Title$  MyWindowsMediaPlayer��6	   $	��500��6&   $	��550��63    � 
ResizeMode$ CanResizeWithGrip=�6�   $	��133��5   0   $	��535��6@   .��5      � 
���   �.+�5   
   r� -   5        $��MyMediaPlayer��5   '   $	��363��5      $
��Left=�6$   $��0,20,0,0q�5      $	��Top=�5	      $	��535��6-    r� LoadedBehavior$ Manual=�5
       r� UnloadedBehavior$
 Stop=�6-    r� Stretch$
 Fill=�5      � 5   
   -   5        $��MenuGrid��5       J� Opacity$	 100Q�6   .+�5      o� -   5        $
��Menu��5      $��20��6   $	��Top=�6+   .@�5      m� $��Media��6   .@�5      m� -   5        $��Open file...��5      65   m� 5      -   5        $��Library��5      60   m� 5      -   5        $
��Exit��5      6-   5      m� 5      $��Playback��6   .@�5      m� -   5        $��PlayMenuItem��5   F   $
��Play��6   6F   m� 5      -	   5        $
��Stop��5      6-   m� 5      -
   5        $��Full Screen��5      64   5      m� 5      $��Playlist��6   .@�5      m� -   5        $
��Read��5      6-   m� 5      -   5        $	��Add��5      6,   m� 5      -   5        $��Remove��5      6/   m� 5      -   5        $
��Save��5      6-   m� 5      -   5        $��Up track��5      61   m� 5      -   5        $��
Down track��5      63   5       5!      5"      � 5#   
   -   5        $��
ButtonGrid��5#      
�������$	 100Q�64   $��Bottom=�5$      $��75��6*   $	��535��66   .+�5%      �� -   5        $��Volume��5%   P   $��20��6   $
��Left=�6!   $��
205,23,0,0q�6<   $	��Top=�6z   $	��191��6�    � Maximum$	 100Q�6�   �� 5&      -   5        $��PositionSlider��5&   N   $��20��6   $
��Left=�6!   $��67,4,0,0q�6<   $	��Top=�6�   $	��448��6�   �� 5'      -   5        $��MuteCheckBox��5'   a   $
��Mute��6   $��16��6&   $
��Left=�62   $��
419,24,0,0q�6M   $	��Top=�6u   6�   �� 5(      -   5        $��HideAll��5(   a   $
��Hide��6   $��16��6&   $
��Left=�62   $��
419,50,0,0q�6M   $	��Top=�6p   6�   �� 5)      -   5        $��	TimeMedia��5)   N   $��00:00:00��6   $��20��6'   $
��Left=�63   $	��Top=�6_   $��36��6w   $��10(�6�   $��0q�6�   $��12,4,0,0q�6�   �� 5+      -   5        $
��Play��5+   N   $��50��6   $
��Left=�6!   $��4,22,0,0q�6<   $	��Top=�6s   $��50��6�   .��5,      �� $��Ressource/play.jpg��6   5-      �� 5/      -   5        $
��Stop��5/   P   $��30��6   $
��Left=�6!   $��
132,32,0,0q�6<   $	��Top=�6o   $��32��6�   .��50      �� $��Ressource/stop.png��6   51      �� 52      -   5        $��
SpeedRight��52   O   $��30��6   $
��Left=�6!   $��	61,32,0,0q�6<   $	��Top=�6z   $��30��6�   6�   �� 54      -   5        $��
NextButton��54   P   $��30��6   $
��Left=�6!   $��
170,32,0,0q�6<   $	��Top=�6u   $��29��6�   .��55      �� $��Ressource/suivant.png��6   56      �� 58      -   5        $��
PrevButton��58   O   $��30��6   $
��Left=�6!   $��	97,32,0,0q�6<   $	��Top=�6t   $��29��6�   6�   �� 5:      -   5        $��FullScreenButton��5:   P   $��23��6   $
��Left=�6!   $��
491,43,0,0q�6<   $	��Top=�6h   $��29��6�   .��5;      �� $��Ressource/fullScreen.png��6   5<      �� 5=      -   5        $��CurrentPlay��5=   O   $��28��6   $
��Left=�6    $��
205,40,0,0q�6;   $	��Top=�6b   $	��191��6z   �� 5>      $!��Ressource/avance_rapide.png��6   $��63,34,446,15q�69   �� 5?      $��Ressource/precedent.png��6   $��99,34,411,15q�65   5@      R� 5A   
   -   5        $
��tree��5A      $
��Left=�6   $	 100Q�6:   $	��363��6H   $��
379,20,0,0q�6U   $	��Top=�6i   $	��155��6�    J� 	AllowDrop . .@�5B      Q� -   5        $��Playlist��5B   @   $��Playlist��6    Q� 
IsExpanded .  6P   5C      �� 5D   
   -    5        $��Debug��5D   L   $��22��6   $
��Left=�6   $��	12,37,0,0q�69   $y�Hidden=�6Y   $	��Top=�6m   $	��353��6�   5E      5F      