   M S B A M L   `   `   `  ����H  DFaceID.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FaceID.Client.CommonFaceID.Client.Common  5          FaceID.Client.Views.Main   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59Z VPrism.Unity.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      D @ModernWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM	 IModernWpf.Controls, Version=0.9.3.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019  	 5      <d2http://schemas.microsoft.com/expression/blend/2008  	 5      Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006  	 5      /core"clr-namespace:FaceID.Client.Common  	 5      _
 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors 
 5	      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6   .+�5   
   ' 	 !ModernWpf.Controls.NavigationView  -   5          Name$ NavView��5         IsBackButtonVisible$ Visible=�5         
IsPaneOpen$
 True��5         PaneDisplayMode$	 Top=�6      IsBackEnabled 5      �� *IsBackEnabled+   SelectedItem 5      �� *
Selected+* 
 $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      + 
 %Microsoft.Xaml.Behaviors.EventTrigger  5      	   	EventName$	 ItemInvoked��6!   * 
 $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 5      -  'Prism.Interactivity.InvokeCommandAction     Command 60   �� *ItemInvokedCommand+5         	MenuItems 5      + 	 %ModernWpf.Controls.NavigationViewItem  5         Icon-  'ModernWpf.Controls.IconElementConverter$ Camera 6(     5      $ People 6(      FooterMenuItems 5!        5"      $ Contact 6(   
 � Tag$ Account��67     5#      $
 Help 6(   .��5%      	  ModernWpf.Controls.Frame	  -   5         	 Name$ 	HomeFrame��5'      $��5q�5&      $��1��5(      "
   FaceID.Client.Common.Regions 
  ContentRegion!  Prism.Regions.RegionManager   
RegionName# Z 5)        � NavigationUIVisibility$ Hidden=�5*       	  ContentTransitions 5,      4  .ModernWpf.Media.Animation.TransitionCollection  9  3ModernWpf.Media.Animation.NavigationThemeTransition  5-      &   DefaultNavigationTransitionInfo. 5.      =  7ModernWpf.Media.Animation.SlideNavigationTransitionInfo     Effect$ 
FromBottom=�6?   5/      50      52      53      �� 54   
   $��1��65   ��6   �� *
GetDevices+��55      �� 56      .R�57      �  � Orientation$ 
Horizontal=�6    58      5:      5;      5<      