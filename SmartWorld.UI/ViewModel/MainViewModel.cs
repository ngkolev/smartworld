using Common;
using SmartWorld.Core;
using SmartWorld.Core.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace SmartWorld.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public int Width { get; set; }

        public int Height { get; set; }

        #region Elements
        private ObservableCollection<ElementViewModel> _elements;
        public virtual ObservableCollection<ElementViewModel> Elements
        {
            get { return _elements; }
            set
            {
                if (value != _elements)
                {
                    _elements = value;
                    OnPropertyChanged("Elements");
                }
            }
        }
        #endregion

        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand RestartCommand { get; private set; }

        private Timer Timer { get; set; }

        private World World { get; set; }

        #region IsWorking
        private bool _isWorking;
        public virtual bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                if (value != _isWorking)
                {
                    _isWorking = value;
                    OnPropertyChanged("IsWorking");

                    StartCommand.RaiseCanExecuteChanged();
                    StopCommand.RaiseCanExecuteChanged();
                    RestartCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        public MainViewModel()
        {
            var config = ConfigManager.Current;
            Width = config.WorldWidth.Rounded();
            Height = config.WorldHeight.Rounded();

            StartCommand = new RelayCommand(Start, () => !IsWorking);
            StopCommand = new RelayCommand(Stop, () => IsWorking);
            RestartCommand = new RelayCommand(Restart, () => IsWorking);

            Timer = new Timer(1000 / 24);
            Timer.Elapsed += Timer_Elapsed;

            World = new World();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoWork();
        }

        private void DoWork()
        {
            lock (this)
            {
                World.Tick();

                var agents = World.Agents.Select(a => new ElementViewModel
                {
                    PositionX = a.Position.X.Rounded(),
                    PositionY = a.Position.Y.Rounded(),
                    Radius = a.Radius.Rounded(),
                    Color = Brushes.Red,
                });

                var foodElements = World.FoodElements.Select(f => new ElementViewModel
                {
                    PositionX = f.Position.X.Rounded(),
                    PositionY =f.Position.Y.Rounded(),
                    Radius = f.Radius.Rounded(),
                    Color = Brushes.Green,
                });

                var allElements = agents.Concat(foodElements);

                Elements = new ObservableCollection<ElementViewModel>(allElements);
            }
        }

        private void Start()
        {
            IsWorking = true;
            Timer.Start();
        }

        private void Stop()
        {
            IsWorking = false;
            Timer.Stop();
        }

        private void Restart()
        {
            World = new World();
        }
    }
}
