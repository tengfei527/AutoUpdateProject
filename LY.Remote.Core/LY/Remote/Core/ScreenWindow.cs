namespace LY.Remote.Core
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public class ScreenWindow : Window, IComponentConnector
    {
        private bool _contentLoaded;
        internal System.Windows.Controls.Button btnMax;
        internal System.Windows.Controls.Button btnMin;
        private ClientControl ClientControlPC;
        private bool IsWindowCloseing;
        private bool IsWindowMax;
        private double LastWindowHeight;
        private double LastWindowLeft;
        private double LastWindowTop;
        private double LastWindowWidth;
        internal System.Windows.Controls.Menu menuP;
        internal System.Windows.Controls.Image screenView;
        internal ScrollViewer scrollV;
        internal Grid stackPal;
        internal System.Windows.Controls.Label status;
        internal System.Windows.Controls.ToolBar toolBar;

        public ScreenWindow()
        {
            this.ClientControlPC = null;
            this.IsWindowCloseing = false;
            this.LastWindowWidth = 0.0;
            this.LastWindowHeight = 0.0;
            this.IsWindowMax = false;
            this.LastWindowLeft = 0.0;
            this.LastWindowTop = 0.0;
            this.InitializeComponent();
        }

        public ScreenWindow(ClientControl clientControlPC) : this()
        {
            this.ClientControlPC = clientControlPC;
            this.ClientControlPC.ScreenControl = this.screenView;
            this.ClientControlPC.AddEvent(new LoginoutRespondEventHandler(this.ClientControlPC_UserLogoutRespond));
            this.ClientControlPC.AddEvent(new SetImageEventHandler(this.ClientControlPC_ShowScreenView));
            this.ClientControlPC.AddEvent(new ControlRespondEventHandler(this.ClientControlPC_ControlRespond));
            if (Screen.PrimaryScreen.WorkingArea.Height < 0x300)
            {
                base.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num;
        Label_002C:
            num = Convert.ToInt32(((System.Windows.Controls.Button) e.Source).Tag);
            bool flag = num != 0;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    this.ClientControlPC.CloseConnect();
                    num2 = 8;
                    goto Label_0002;

                case 1:
                    if (flag)
                    {
                        break;
                    }
                    num2 = 6;
                    goto Label_0002;

                case 2:
                    if (flag)
                    {
                        flag = num != 1;
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    goto Label_0002;

                case 3:
                case 7:
                case 8:
                    break;

                case 4:
                    this.IsWindowMax = true;
                    this.LastWindowWidth = base.Width;
                    this.LastWindowHeight = base.Height;
                    this.LastWindowLeft = base.Left;
                    this.LastWindowTop = base.Top;
                    base.Width = Screen.PrimaryScreen.Bounds.Width;
                    base.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.btnMax.Visibility = Visibility.Collapsed;
                    this.btnMin.Visibility = Visibility.Visible;
                    base.Top = 0.0;
                    base.Left = 0.0;
                    base.WindowStyle = WindowStyle.None;
                    this.ClientControlPC.BorderWidth = 0;
                    this.ClientControlPC.CaptionHeight = 0;
                    num2 = 3;
                    goto Label_0002;

                case 5:
                    if (flag)
                    {
                        flag = num != 2;
                        int expressionStack_74_0 = 1;
                        if (expressionStack_74_0 == 0)
                        {
                        }
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 6:
                    this.IsWindowMax = false;
                    base.Width = this.LastWindowWidth;
                    base.Height = this.LastWindowHeight;
                    base.Left = this.LastWindowLeft;
                    base.Top = this.LastWindowTop;
                    this.btnMax.Visibility = Visibility.Visible;
                    this.btnMin.Visibility = Visibility.Collapsed;
                    base.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.ClientControlPC.BorderWidth = Convert.ToInt32((double) ((base.ActualWidth - this.stackPal.ActualWidth) / 2.0));
                    this.ClientControlPC.CaptionHeight = Convert.ToInt32(SystemParameters.CaptionHeight);
                    num2 = 7;
                    goto Label_0002;

                default:
                    goto Label_002C;
            }
        }

        private void ClientControlPC_ControlRespond(object sender, ControlRespondEventArgs e)
        {
            // This item is obfuscated and can not be translated.
        }

        private void ClientControlPC_ShowScreenView(MemoryStream ms)
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            this.screenView.Dispatcher.Invoke(delegate {
                BitmapImage image2;
                int expressionStack_2D_0;
            Label_0027:
                expressionStack_2D_0 = 1;
                if (expressionStack_2D_0 == 0)
                {
                }
                bool flag = !this.IsWindowMax;
                int num2 = 2;
            Label_0010:
                switch (num2)
                {
                    case 0:
                    case 1:
                        this.status.Margin = new Thickness(((this.ActualWidth / 2.0) - (this.status.ActualWidth / 2.0)) - 140.0, 0.0, 0.0, 0.0);
                        return;

                    case 2:
                        if (flag)
                        {
                            image2 = new BitmapImage();
                            image2.BeginInit();
                            image2.StreamSource = new MemoryStream(ms.ToArray());
                            image2.EndInit();
                            this.screenView.Source = image2;
                            ms.Close();
                            ms.Dispose();
                            num2 = 0;
                        }
                        else
                        {
                            num2 = 3;
                        }
                        goto Label_0010;

                    case 3:
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        int toW = Convert.ToInt32((double) (this.scrollV.ActualWidth - 20.0));
                        MemoryStream stream = ScreenCapture.MakeMaximum(ms, image.Width, image.Height, toW);
                        image2 = new BitmapImage();
                        image2.BeginInit();
                        image2.StreamSource = new MemoryStream(stream.ToArray());
                        image2.EndInit();
                        this.screenView.Source = image2;
                        stream.Close();
                        stream.Dispose();
                        num2 = 1;
                        goto Label_0010;
                    }
                }
                goto Label_0027;
            });
        }

        private void ClientControlPC_UserLogoutRespond(object sender, LoginRespondEventArgs e)
        {
            // This item is obfuscated and can not be translated.
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            int expressionStack_27_0;
            int num2 = 14;
        Label_0021:
            expressionStack_27_0 = 1;
            if (expressionStack_27_0 == 0)
            {
            }
            bool flag = !this._contentLoaded;
            int num = 2;
        Label_000B:
            switch (num)
            {
                case 0:
                case 3:
                    return;

                case 1:
                    num = 3;
                    goto Label_000B;

                case 2:
                    if (flag)
                    {
                        this._contentLoaded = true;
                        Uri resourceLocator = new Uri(SMouseEventArgs.b("웨꟪듬쇮ꏰ雲飴飶跸黺폼볾渀焂怄㰆樈搊怌缎縐紒瀔礖洘㐚渜簞匠䘢䀤䤦帨䈪䌬䬮帰䐲᬴伶堸嘺儼", num2), UriKind.Relative);
                        System.Windows.Application.LoadComponent(this, resourceLocator);
                        num = 0;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000B;
            }
            goto Label_0021;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // This item is obfuscated and can not be translated.
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            // This item is obfuscated and can not be translated.
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            // This item is obfuscated and can not be translated.
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            int expressionStack_23_0;
            int num2 = 14;
        Label_001D:
            expressionStack_23_0 = 1;
            if (expressionStack_23_0 == 0)
            {
            }
            System.Windows.Controls.ToolBar templatedParent = sender as System.Windows.Controls.ToolBar;
            FrameworkElement element = templatedParent.Template.FindName(SMouseEventArgs.b("ꛨ鷪裬鷮韰鿲髴胶뻸觺铼鯾", num2), templatedParent) as FrameworkElement;
            bool flag = element == null;
            int num = 1;
        Label_000B:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    if (flag)
                    {
                        break;
                    }
                    num = 2;
                    goto Label_000B;

                case 2:
                    num = 0;
                    goto Label_000B;

                default:
                    goto Label_001D;
            }
        }

        private void tspClose_Click(object sender, EventArgs e)
        {
            this.ClientControlPC.CloseConnect();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.IsWindowCloseing = true;
            this.ClientControlPC.CloseConnect();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int expressionStack_6_0 = 1;
            if (expressionStack_6_0 == 0)
            {
            }
            this.ClientControlPC.BorderWidth = Convert.ToInt32((double) ((base.ActualWidth - this.stackPal.ActualWidth) / 2.0));
            this.ClientControlPC.CaptionHeight = Convert.ToInt32(SystemParameters.CaptionHeight);
        }
    }
}

