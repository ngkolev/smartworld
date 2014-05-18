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
                    StartFastGenerationCommand.RaiseCanExecuteChanged();
                    StopFastGenerationCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region IsFastGenerating
        private bool _isFastGenerating;
        public virtual bool IsFastGenerating
        {
            get { return _isFastGenerating; }
            set
            {
                if (value != _isFastGenerating)
                {
                    _isFastGenerating = value;
                    OnPropertyChanged("IsFastGenerating");

                    StartFastGenerationCommand.RaiseCanExecuteChanged();
                    StopFastGenerationCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region WorldAge
        private int _worldAge;
        public virtual int WorldAge
        {
            get { return _worldAge; }
            set
            {
                if (value != _worldAge)
                {
                    _worldAge = value;
                    OnPropertyChanged("WorldAge");
                }
            }
        }
        #endregion

        #region BestAgentFitness
        private double _bestAgentFitness;
        public virtual double BestAgentFitness
        {
            get { return _bestAgentFitness; }
            set
            {
                if (value != _bestAgentFitness)
                {
                    _bestAgentFitness = value;
                    OnPropertyChanged("BestAgentFitness");
                }
            }
        }
        #endregion

        #region NumberOfAgentsThatHaveExisted
        private int _numberOfAgentsThatHaveExisted;
        public virtual int NumberOfAgentsThatHaveExisted
        {
            get { return _numberOfAgentsThatHaveExisted; }
            set
            {
                if (value != _numberOfAgentsThatHaveExisted)
                {
                    _numberOfAgentsThatHaveExisted = value;
                    OnPropertyChanged("NumberOfAgentsThatHaveExisted");
                }
            }
        }
        #endregion

        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand RestartCommand { get; private set; }
        public RelayCommand StartFastGenerationCommand { get; private set; }
        public RelayCommand StopFastGenerationCommand { get; private set; }


        private Timer Timer { get; set; }

        private Task FastGenerationTask { get; set; }

        private World World { get; set; }


        public MainViewModel()
        {
            // Init map
            var config = ConfigManager.Current;
            Width = config.WorldWidth.Rounded();
            Height = config.WorldHeight.Rounded();
            World = new World();

            // Init commands
            StartCommand = new RelayCommand(Start, () => !IsWorking);
            StopCommand = new RelayCommand(Stop, () => IsWorking);
            RestartCommand = new RelayCommand(Restart, () => IsWorking);
            StartFastGenerationCommand = new RelayCommand(StartFastGeneration, () => IsWorking && !IsFastGenerating);
            StopFastGenerationCommand = new RelayCommand(StopFastGeneration, () => IsWorking && IsFastGenerating);

            // Init timer
            Timer = new Timer(1000 / 24);
            Timer.Elapsed += Timer_Elapsed;
        }


        private static readonly object syncObjectWorldTimer = new object();
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (syncObjectWorldTimer)
            {
                World.Tick();

                var agents = World.Agents.Select(a => new ElementViewModel
                {
                    PositionX = (a.Position.X - a.Radius).Rounded(),
                    PositionY = (a.Position.Y - a.Radius).Rounded(),
                    Diameter = (a.Radius * 2).Rounded(),
                    Color = Brushes.Red,
                    IsAgent = true,
                    AgeString = "a: {0}".Formatted(a.Age),
                    HealthString = "h: {0}".Formatted(a.Health),
                });

                var foodElements = World.FoodElements.Select(f => new ElementViewModel
                {
                    PositionX = (f.Position.X - f.Radius).Rounded(),
                    PositionY = (f.Position.Y - f.Radius).Rounded(),
                    Diameter = (f.Radius * 2).Rounded(),
                    Color = Brushes.Green,
                });

                var allElements = agents.Concat(foodElements);

                Elements = new ObservableCollection<ElementViewModel>(allElements);

                NumberOfAgentsThatHaveExisted = World.NumberOfAgentsThatHaveExisted;
                WorldAge = World.NumberOfTicks;
                BestAgentFitness = World.BestAgentFitness;
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
            if (FastGenerationTask!=null)
            {
                IsFastGenerating = false;
                FastGenerationTask.Wait();
            }
        }

        private void Restart()
        {
            World = new World();
        }

        private void StartFastGeneration()
        {
            IsFastGenerating = true;

            Timer.Stop();

            FastGenerationTask = Task.Factory.StartNew(() =>
            {
                lock (syncObjectWorldTimer)
                {
                    while (IsFastGenerating)
                    {
                        World.Tick();
                    }
                }
            });
        }

        private void StopFastGeneration()
        {
            IsFastGenerating = false;
            FastGenerationTask.Wait();
            FastGenerationTask = null;
            Timer.Start();
        }
    }
}
