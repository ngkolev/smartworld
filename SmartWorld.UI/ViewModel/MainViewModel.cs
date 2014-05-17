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

namespace SmartWorld.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public int Width { get; set; }

        public int Height { get; set; }

        #region Agents
        private ObservableCollection<AgentViewModel> _agents;
        public virtual ObservableCollection<AgentViewModel> Agents
        {
            get { return _agents; }
            set
            {
                if (value != _agents)
                {
                    _agents = value;
                    OnPropertyChanged("Agents");
                }
            }
        }
        #endregion

        #region FoodElement
        private ObservableCollection<FoodElementViewModel> _foodElement;
        public virtual ObservableCollection<FoodElementViewModel> FoodElement
        {
            get { return _foodElement; }
            set
            {
                if (value != _foodElement)
                {
                    _foodElement = value;
                    OnPropertyChanged("FoodElement");
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

            // NOTE: Commented because of the test. Remove this comment when it is done
            //World = new World();

            StartCommand = new RelayCommand(Start);
            Timer = new Timer(1000 / 24);
            Timer.Elapsed += Timer_Elapsed;

            // NOTE: Just for the test. Remove this when it is done
            Agents = new ObservableCollection<AgentViewModel>() 
            {
                new AgentViewModel 
                { 
                    PositionX = 10, 
                    PositionY = 20, 
                    Radius = 20 
                } 
            };

            FoodElement = new ObservableCollection<FoodElementViewModel>() 
            { 
                new FoodElementViewModel {
                    PositionX = 10,
                    PositionY = 20,
                    Radius = 10 
                } 
            };
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            World.Tick();

            var agents = World.Agents.Select(a => new AgentViewModel
            {
                PositionX = (int)a.Position.X,
                PositionY = (int)a.Position.Y,
                Radius = (int)a.Radius,
            });

            var foodElements = World.FoodElements.Select(f => new FoodElementViewModel
            {
                PositionX = (int)f.Position.X,
                PositionY = (int)f.Position.Y,
                Radius = (int)f.Radius,
            });

            Agents = new ObservableCollection<AgentViewModel>(agents);
            FoodElement = new ObservableCollection<FoodElementViewModel>(foodElements);
        }

        private void Start()
        {
            Timer.Start();
        }
    }
}
