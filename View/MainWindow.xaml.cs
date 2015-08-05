namespace View
{
    using System.Windows.Controls.Ribbon;
    using System.Windows.Input;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        private readonly MainViewModel mainViewModel; 

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
       
        //---------------------------------------------------------------------
        #region [Constructor]
        //---------------------------------------------------------------------
	
        public MainWindow()
        {
            InitializeComponent();
            // this.ReadXml();
            //_ribbon.IsMinimized = true;
            mainViewModel = new MainViewModel();
            base.DataContext = mainViewModel;
            this._treeViewControl.DataContext = base.DataContext;
            this._orderGrid.DataContext = mainViewModel.CurrentOrder;
            this.CreateBindings();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Create standard bindings.
        /// </summary>
        private void CreateBindings()
        {
            var binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += (s, e) => {mainViewModel.NewCommand(s,e);};
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteNew(s,e); };
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += (s,e)=> { mainViewModel.OpenCommand(s, e); };
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteOpen(s,e); };
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += (s, e) => { mainViewModel.SaveCommand(s, e); };
            binding.CanExecute += (s,e) => { mainViewModel.CanExecuteSave(s,e); };

            binding = new CommandBinding(ApplicationCommands.SaveAs);
            binding.Executed += (s, e) => { mainViewModel.SaveAsCommand(s, e); };
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteSaveAs(s, e); };

            binding = new CommandBinding(ApplicationCommands.Find);
            binding.Executed += (s, e) => { mainViewModel.FindCommand(s, e); };
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteFind(s, e); };

            binding = new CommandBinding(ApplicationCommands.Delete);
            binding.Executed += (s, e) => { mainViewModel.DeleteCommand(s, e); };
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteDelete(s, e); };

            binding = new CommandBinding(ApplicationCommands.Help);
            binding.Executed += (s, e) => { mainViewModel.HelpCommand(s, e); };
            binding.CanExecute += (s, e) => { mainViewModel.CanExecuteHelp(s, e); };
            
            this.CommandBindings.Add(binding);
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

    
       
    }
}
