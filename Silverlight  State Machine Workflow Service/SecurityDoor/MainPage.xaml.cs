namespace SecurityDoor
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using SecurityDoor.SecurityWeb;

    /// <summary>
    ///   The main page.
    /// </summary>
    public partial class MainPage : UserControl, INotifyPropertyChanged
    {
        #region Constants and Fields

        /// <summary>
        ///   The proxy.
        /// </summary>
        private readonly SecurityDoorServiceClient proxy = new SecurityDoorServiceClient();

        /// <summary>
        ///   The timer.
        /// </summary>
        private readonly DispatcherTimer timer = new DispatcherTimer();

        private bool alert;

        /// <summary>
        ///   The card key.
        /// </summary>
        private Guid cardKey;

        /// <summary>
        ///   The locked flag.
        /// </summary>
        private bool locked;

        /// <summary>
        ///   The door open flag.
        /// </summary>
        private bool open;

        /// <summary>
        ///   The retry count.
        /// </summary>
        private int retryCount;

        /// <summary>
        ///   The room number.
        /// </summary>
        private int roomNumber;

        /// <summary>
        ///   The timeout.
        /// </summary>
        private int timeout = 10;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.DataContext = this;
            this.locked = true;
            this.InitializeComponent();
            var r = new Random();
            this.RoomNumber = r.Next(100);
            this.CardKey = Guid.NewGuid();

            this.timer.Tick += this.OnOpenTimeout;
            this.timer.Interval = TimeSpan.FromSeconds(this.Timeout);

            this.proxy.AuthorizeKeyCompleted += this.OnAuthorizeKeyCompleted;
            this.proxy.NotifyDoorStatusCompleted += OnNotifyDoorStatusCompleted;
            this.proxy.DoorResetCompleted += this.OnDoorResetCompleted;

            this.Messages = new ObservableCollection<string>();
        }

        #endregion

        #region Events

        /// <summary>
        ///   The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public bool Alert
        {
            get
            {
                return this.alert;
            }
            set
            {
                this.alert = value;
                this.NotifyChanged("Alert");
            }
        }

        /// <summary>
        ///   Gets a value indicating whether CanOpen.
        /// </summary>
        public bool CanOpen
        {
            get
            {
                return this.DoorClosed && this.Unlocked;
            }
        }

        /// <summary>
        ///   Gets or sets CardKey.
        /// </summary>
        public Guid CardKey
        {
            get
            {
                return this.cardKey;
            }

            set
            {
                this.cardKey = value;
                this.NotifyChanged("CardKey");
            }
        }

        /// <summary>
        ///   Gets a value indicating whether DoorClosed.
        /// </summary>
        public bool DoorClosed
        {
            get
            {
                return !this.open;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether Open.
        /// </summary>
        public bool DoorOpened
        {
            get
            {
                return this.open;
            }

            set
            {
                this.open = value;
                if (this.open)
                {
                    this.doorImage.Source = new BitmapImage(new Uri("Images/OpenDoor.png", UriKind.Relative));
                    this.Open.Position = TimeSpan.FromSeconds(0);
                    this.Open.Play();
                }
                else
                {
                    this.doorImage.Source = new BitmapImage(new Uri("Images/ClosedDoor.png", UriKind.Relative));
                    this.Close.Position = TimeSpan.FromSeconds(0);
                    this.Close.Play();
                }

                this.NotifyChanged("DoorOpened");
                this.NotifyChanged("DoorClosed");
                this.NotifyChanged("CanOpen");
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether Locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                this.locked = value;

                if (this.Locked)
                {
                    this.LED.Fill = new SolidColorBrush(Colors.Red);
                }

                this.NotifyChanged("Locked");
                this.NotifyChanged("Unlocked");
                this.NotifyChanged("CanOpen");
            }
        }

        /// <summary>
        ///   Gets or sets Messages.
        /// </summary>
        public ObservableCollection<string> Messages { get; set; }

        /// <summary>
        ///   Gets or sets RoomNumber.
        /// </summary>
        public int RoomNumber
        {
            get
            {
                return this.roomNumber;
            }

            set
            {
                this.roomNumber = value;
                this.NotifyChanged("RoomNumber");
            }
        }

        /// <summary>
        ///   Gets or sets Timeout.
        /// </summary>
        public int Timeout
        {
            get
            {
                return this.timeout;
            }

            set
            {
                this.timeout = value;
                this.NotifyChanged("Timeout");
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether Unlocked.
        /// </summary>
        public bool Unlocked
        {
            get
            {
                return !this.Locked;
            }

            set
            {
                this.Locked = !value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   The notify changed.
        /// </summary>
        /// <param name = "property">
        ///   The property.
        /// </param>
        public void NotifyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   The on notify door status completed.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private static void OnNotifyDoorStatusCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        /// <summary>
        ///   The add message.
        /// </summary>
        /// <param name = "msg">
        ///   The msg.
        /// </param>
        private void AddMessage(string msg)
        {
            this.Messages.Insert(0, msg);
            //this.Messages.Add(msg);
            this.NotifyChanged("Messages");
        }

        /// <summary>
        ///   The authorize unlock.
        /// </summary>
        /// <param name = "retry">
        ///   The retry.
        /// </param>
        private void AuthorizeUnlock(bool retry = false)
        {
            this.AddMessage("Authorize Unlock");
            this.LED.Fill = new SolidColorBrush(Colors.Yellow);
            if (!retry)
            {
                this.retryCount = 0;
            }
            else
            {
                this.retryCount++;
            }

            this.proxy.AuthorizeKeyAsync(this.RoomNumber, this.CardKey, TimeSpan.FromSeconds(this.Timeout), 3);
        }

        /// <summary>
        ///   The button clear_ click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonClearClick(object sender, RoutedEventArgs e)
        {
            this.Messages.Clear();
            this.NotifyChanged("Messages");
        }

        /// <summary>
        ///   The button close click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            this.DoorOpened = false;
            this.Locked = true;
            this.AddMessage("Notify Door Closed");
            this.proxy.NotifyDoorStatusAsync(this.RoomNumber, this.DoorOpened, this.Locked);
        }

        /// <summary>
        ///   The button empty_ click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonEmptyClick(object sender, RoutedEventArgs e)
        {
            this.CardKey = Guid.Empty;
        }

        /// <summary>
        ///   The button new key click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonNewKeyClick(object sender, RoutedEventArgs e)
        {
            this.CardKey = Guid.NewGuid();
        }

        /// <summary>
        ///   The button open_ click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonOpenClick(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
            this.DoorOpened = true;
            this.AddMessage("Notify Door Open");
            this.proxy.NotifyDoorStatusAsync(this.RoomNumber, this.DoorOpened, this.Locked);
        }

        /// <summary>
        ///   The button unlock_ click.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void ButtonUnlockClick(object sender, RoutedEventArgs e)
        {
            this.AuthorizeUnlock();
        }

        /// <summary>
        ///   The on open door completed.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "args">
        ///   The event args.
        /// </param>
        private void OnAuthorizeKeyCompleted(object sender, AuthorizeKeyCompletedEventArgs args)
        {
            if (args.Error != null && this.retryCount > 2)
            {
                MessageBox.Show(args.Error.Message);
                return;
            }

            try
            {
                var unlocked = args.Result.Authorized;
                this.Alert = args.Result.Alert;

                this.TryToUnlock(unlocked);
            }
            catch (Exception ex)
            {
                if (this.retryCount > 2)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                else
                {
                    this.AddMessage("Error - retry");
                    Thread.Sleep(1000);
                    this.AuthorizeUnlock(true);
                }
            }
        }

        private void OnDoorResetCompleted(object sender, DoorResetCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.Message);
            }

            try
            {
                var unlocked = args.Result.Authorized;
                this.Alert = args.Result.Alert;
                this.TryToUnlock(unlocked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        ///   The on open timeout.
        /// </summary>
        /// <param name = "sender">
        ///   The sender.
        /// </param>
        /// <param name = "e">
        ///   The event args.
        /// </param>
        private void OnOpenTimeout(object sender, EventArgs e)
        {
            this.AddMessage("Open Timeout");

            this.timer.Stop();
            this.Locked = true;
        }

        /// <summary>
        ///   The show authorized.
        /// </summary>
        private void ShowAuthorized()
        {
            this.Unlock.Position = TimeSpan.FromSeconds(0);
            this.Unlock.Play();
            this.LED.Fill = new SolidColorBrush(Colors.Green);
        }

        /// <summary>
        ///   The show unauthorized.
        /// </summary>
        private void ShowUnauthorized()
        {
            this.LED.Fill = new SolidColorBrush(Colors.Red);
            this.Buzz.Position = TimeSpan.FromSeconds(0);
            this.Buzz.Play();
        }

        private void TryToUnlock(bool unlocked)
        {
            if (unlocked)
            {
                this.timer.Start();
                this.ShowAuthorized();
            }
            else
            {
                if (this.Alert)
                {
                    this.AddMessage("Alert - Security Notified");
                }

                this.ShowUnauthorized();
            }
            this.Unlocked = unlocked;
        }

        private void ButtonResetClick(object sender, RoutedEventArgs e)
        {
            this.AddMessage("Door Reset");
            this.proxy.DoorResetAsync(this.RoomNumber, this.CardKey, TimeSpan.FromSeconds(this.Timeout), 3);
        }

        #endregion
    }
}