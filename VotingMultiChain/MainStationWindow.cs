using System;
using Gtk;
using MultiChainLib;
using System.Threading;
using System.Threading.Tasks;

namespace VotingMultiChain
{
    public partial class MainStationWindow : Gtk.Window
    {
        private MultiChainClient client;

        public MainStationWindow(MultiChainClient importClient) :
                base(Gtk.WindowType.Toplevel)
        {
            client = importClient;
            this.Build();
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
			Application.Quit();
			args.RetVal = true;
        }
    }
}
