using Models.Interfaces;
using Models.Models;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using ViewModels.Commands;

namespace ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields
        private int _numberOfHits;
        private DelegateCommand<object> _getStatsButtonCommand;
        private DelegateCommand<object> _closeAppCommand;
        private BackgroundWorker _progressBarWorker;
        private double _progressPercent;
        private bool _inProgress;
        private string urlAftonbladet = @"http://www.aftonbladet.se";
        private string urlExpressen = @"http://www.expressen.se";
        private string urlDn = @"http://www.dn.se";
        #endregion

        #region Propertys
        private IWebCalculator Calculator { get; }
        private IWebCollector Collector { get; }
        private string SelectedKeyWord { get; set; }
        public bool InProgress
        {
            get { return _inProgress; }
            private set { _inProgress = value; RaisePropertyChange("InProgress"); }
        }
        public string SelectedUrl { get; set; }

        public double ProgressPercent
        {
            get { return _progressPercent; }
            set
            {
                _progressPercent = value;
                RaisePropertyChange("ProgressPercent");
            }
        }

        public int NumberOfHits
        {
            get { return _numberOfHits; }
            private set
            {
                _numberOfHits = value;
                RaisePropertyChange("NumberOfHits");
            }
        }
        #endregion

        #region DelegateCommand Propertys
        public DelegateCommand<object> RadioButtonKeywordCommand
        {
            get
            {
                return new DelegateCommand<object>((p) =>
                {
                    SelectedKeyWord = (string)p;
                });
            }
        }

        public DelegateCommand<object> RadioButtonUrlCommand
        {
            get
            {
                return new DelegateCommand<object>((p) =>
                {
                    switch ((string)p)
                    {
                        case "Aftonbladet":
                            {
                                SelectedUrl = urlAftonbladet;
                                break;
                            }
                        case "Expressen":
                            {
                                SelectedUrl = urlExpressen;
                                break;
                            }
                        case "Dn":
                            {
                                SelectedUrl = urlDn;
                                break;
                            }
                        default:
                            {
                                SelectedUrl = (string)p;
                                break;
                            }
                    }
                    RaisePropertyChange("SelectedUrl");
                });
            }
        }

        public DelegateCommand<object> GetStatButtonCommand
        {
            get
            {
                if (_getStatsButtonCommand == null)
                {
                    _getStatsButtonCommand = new DelegateCommand<object>(
                        whatToExecute => updateView(),
                        canExecuteTrueOrFalse => InProgress ? false : true
                        );
                }
                return _getStatsButtonCommand;
            }
        }

        public DelegateCommand<object> CloseAppCommand
        {
            get
            {
                if (_closeAppCommand == null)
                {
                    _closeAppCommand = new DelegateCommand<object>(this.CloseWindow);
                }
                return _closeAppCommand;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Contructor
        public MainViewModel()
        {
            Calculator = new WebCalculator();
            Collector = new WebCollector();
            SelectedKeyWord = "Trump";
            SelectedUrl = urlAftonbladet;
            SetUpProgressWorker();
        }
        #endregion

        #region Methods
        private void SetUpProgressWorker()
        {
            _progressBarWorker = new BackgroundWorker();
            _progressBarWorker.WorkerReportsProgress = true;
            _progressBarWorker.DoWork += _progressWorker_DoWork;
            _progressBarWorker.ProgressChanged += _progressWorker_ProgressChanged;
            _progressBarWorker.RunWorkerCompleted += _progressWorker_RunWorkerCompleted;
        }

        private void updateView()
        {
            InProgress = true;
            _progressBarWorker.RunWorkerAsync();
            getHTMLCode();
            calculateNumberOfHits();
        }

        private void calculateNumberOfHits()
        {
            NumberOfHits = Calculator.CalculateNumberOfHits(Collector, SelectedKeyWord);
        }

        private void getHTMLCode()
        {
            Collector.GetHTMLFromUrl(SelectedUrl);
        }

        private void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseWindow(object obj)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Breaking News", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Window win = obj as Window;
                win.Close();
            }

        }
        #endregion

        #region EventHandlers ProgressBar
        private void _progressWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < 100; i++)
            {
                Thread.Sleep(10);
                _progressBarWorker.ReportProgress(i);
            }
        }

        private void _progressWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressPercent = e.ProgressPercentage;
        }

        private void _progressWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressPercent = 100.0;
            InProgress = false;
            GetStatButtonCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
