using System;
using MultiChainLib;
using System.Threading;
using System.Threading.Tasks;
using Gtk;

namespace VotingMultiChain
{
    public partial class VotingWindow : Gtk.Window
    {
        private MultiChainClient client;

        public VotingWindow(MultiChainClient importClient) :
                base(Gtk.WindowType.Toplevel)
        {
            client = importClient;
            this.Build();
			Pango.FontDescription fd = Pango.FontDescription.FromString
                                                    ("Arial bold 15");
			this.votingTitle.ModifyFont(fd);
        }

        //Calls createAddressAsync, if async cannot connect to chain throws 
        //exception shows error, closes window and opens blockchain info again.
        protected void OnGetAddressButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    await CreateAddressAsync(BlockchainPermissions.Send |
                                             BlockchainPermissions.Receive |
                                             BlockchainPermissions.Issue);
                });
                task.Wait();
            }
			catch (Exception ex)
			{
				Console.WriteLine("******************");
				Console.WriteLine(ex);

				MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent,
													 MessageType.Error, ButtonsType.Close,
				"Connection to chain lost please reconnect.");
				md.Run();
				md.Destroy();

				MainWindow win = new MainWindow();
				win.Show();
                this.Destroy();
			}
        }

        //Function to create a new address and display it in the GUI just to show 
        //we are still connected to blockchain
        private async Task CreateAddressAsync(BlockchainPermissions permissions)
        {
			// Create a new address
			Console.WriteLine("Create New address");
			var newAddress = await client.GetNewAddressAsync();
			newAddress.AssertOk();
			Console.WriteLine("New issue address: " + newAddress.Result);
            newAddressEntry.Text = newAddress.Result;
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
			Application.Quit();
			args.RetVal = true;
        }
    }
}
