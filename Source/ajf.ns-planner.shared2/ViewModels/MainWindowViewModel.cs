using System;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.ViewModels
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly INsContext _nsContext;
        private ICreateEmailsCommand _createEmailsCommand;
        private ICreateResultFileCommand _createResultFileCommand;
        private ISendEmailsCommand _sendEmailsCommand;

        public MainWindowViewModel(ICreateResultFileCommand createResultFileCommand,
            ICreateEmailsCommand createEmailsCommand, ISendEmailsCommand sendEmailsCommand,
            ILogItemListViewModel logItemListViewModelListViewModel, IChangeConfigCommand changeConfigCommand,
            INsContext nsContext, IMakeBackupCommand makeBackupCommand)
        {
            _nsContext = nsContext;
            Title = "Flet NS ønsker, skab emails, send dem";

            LogItemListViewModel = logItemListViewModelListViewModel;
            ChangeConfigCommand = changeConfigCommand;
            CreateResultFileCommand = createResultFileCommand;
            CreateEmailsCommand = createEmailsCommand;
            SendEmailsCommand = sendEmailsCommand;
            MakeBackupCommand = makeBackupCommand;
        }

        public ILogItemListViewModel LogItemListViewModel { get; set; }
        public IChangeConfigCommand ChangeConfigCommand { get; set; }
        public IMakeBackupCommand MakeBackupCommand { get; set; }
        public string Title { get; private set; }

        public string ChangeConfigCommandText => "Skift folder. Aktuelt: " + Environment.NewLine +
                                                 _nsContext.Directory;

        public string MakeBackupCommandText => "Lav backup af " + _nsContext.Directory;

        public string ConfigFileLabel
        {
            get { return _nsContext.Directory; }
        }

        public ICreateResultFileCommand CreateResultFileCommand
        {
            get { return _createResultFileCommand; }
            set
            {
                value.MainWindowViewModel = this;
                _createResultFileCommand = value;
            }
        }

        public ICreateEmailsCommand CreateEmailsCommand
        {
            get { return _createEmailsCommand; }
            set
            {
                value.MainWindowViewModel = this;
                _createEmailsCommand = value;
            }
        }

        public ISendEmailsCommand SendEmailsCommand
        {
            get { return _sendEmailsCommand; }
            set
            {
                value.MainWindowViewModel = this;
                _sendEmailsCommand = value;
            }
        }
    }
}