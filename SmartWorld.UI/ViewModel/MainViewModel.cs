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

        private Timer Timer { get; set; }

        private World World { get; set; }


        public MainViewModel()
        {
            var config = ConfigManager.Current;
            Width = (int)config.WorldWidth;
            Height = (int)config.WorldHeight;

            World = new World();

            StartCommand = new RelayCommand(Start);
            Timer = new Timer(1000 / 24);
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            World.Tick();

            var agents = World.Agents.Select(a => new ElementViewModel
            {
                PositionX = (int)a.Position.X,
                PositionY = (int)a.Position.Y,
                Radius = (int)a.Radius,
                Color = Brushes.Red,
            });

            var foodElements = World.FoodElements.Select(f => new ElementViewModel
            {
                PositionX = (int)f.Position.X,
                PositionY = (int)f.Position.Y,
                Radius = (int)f.Radius,
                Color = Brushes.Green,
            });

            var allElements = agents.Union(foodElements);

            Elements = new ObservableCollection<ElementViewModel>(allElements);
        }

        private void Start()
        {
            Timer.Start();
        }
    }
}
