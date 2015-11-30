using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace ServiceConsumer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string Request
        {
            get { return _request; }
            set
            {
                if (_request != value)
                {
                    _request = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        private string _request;

        public string Result
        {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        private string _result;

        public ICommand SendRequestCommand
        {
            get { return _sendRequestCommand ?? (_sendRequestCommand = new RelayCommand(SendRequest, CanSendRequest)); }
        }
        private RelayCommand _sendRequestCommand;

        private bool CanSendRequest()
        {
            return true;
        }

        private void SendRequest()
        {
            try
            {
                this.Result = ServiceClient.SendRequest(this.Request, "localhost", 15000);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
        }
    }
}