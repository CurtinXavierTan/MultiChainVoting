using System;
using Gtk;
using MultiChainLib;
using System.Threading;
using System.Threading.Tasks;

namespace VotingMultiChain
{
    public partial class LoginWindow : Gtk.Window
    {
        private MultiChainClient client;

        public LoginWindow(MultiChainClient importClient) :
                base(Gtk.WindowType.Toplevel)
        {
            client = importClient;
            this.Build();
			Pango.FontDescription fd = Pango.FontDescription.FromString("Arial bold 15");
			this.titleLabel.ModifyFont(fd);
            this.passwordEntry.WidthChars = 20;
            this.userEntry.WidthChars = 20;
        }

        //If login is valid go to either voting or main station screen
        protected void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if(loginIsValid(userEntry.Text, passwordEntry.Text))
            {
                string userType = "voter";
                //string userType = "admin";

                if(userType.Equals("voter"))
                {
					VotingMultiChain.VotingWindow win = new VotingMultiChain.VotingWindow(client);
					win.Show();
					this.Destroy();
                }
                else if(userType.Equals("admin"))
                {
					VotingMultiChain.MainStationWindow win = new VotingMultiChain.MainStationWindow(client);
					win.Show();
					this.Destroy();
                }
                
            }
        }

        //Only contains true until connection to database can be made
        private Boolean loginIsValid(String userName, String password)
        {

            return true;
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
			Application.Quit();
			args.RetVal = true;
        }
    }
}
