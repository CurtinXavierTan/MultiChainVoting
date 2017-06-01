using System;
using Gtk;
using MultiChainLib;
using System.Threading;
using System.Threading.Tasks;

public partial class MainWindow : Gtk.Window
{

    //Allow all functions to make use of the MultiChainClient client
    private MultiChainClient client;

    private Boolean clientConnected = false;



    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {

        Build();
        Pango.FontDescription fd = Pango.FontDescription.FromString("Arial bold 15");
        this.labelTitle.ModifyFont(fd);
        this.portEntry.WidthChars = 7;
   
	}

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnClientConnectClicked(object sender, EventArgs e)
    {
        
       
		// Call task to connect to client blockchain
		try
		{
            var task = Task.Run(async () =>
           {
               await connectToChain();
           });

			task.Wait();  

		}
		catch (Exception ex)
		{
			Console.WriteLine("******************");
			Console.WriteLine(ex);

			MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                 MessageType.Error, ButtonsType.Close,
            "Could not connect to chain, please ensure you have entered the correct details.");
			md.Run();
			md.Destroy();
		}

        //Open login screen and close this if client connected successfully
		if (clientConnected)
		{
			VotingMultiChain.LoginWindow win = new VotingMultiChain.LoginWindow(client);
			win.Show();
			this.Destroy();
		}
    }


	internal async Task connectToChain()
	{
        //blockchain data user entry
    
        String ipAddress = ipEntry.Text;
        int portNum = Convert.ToInt32(portEntry.Text);
        String rpcUserName = rpcUserEntry.Text;
        String rpcPass = rpcPasswordentry.Text;
        String chainName = chainNameEntry.Text;
        client = new MultiChainClient(ipAddress, portNum, false, rpcUserName, rpcPass, chainName);


        //Fast client data entry here for development
        //client = new MultiChainClient("192.168.218.131", 4390, false, "test", "123", "test");

        //Print out chain name to console to be sure we are connected
        Console.WriteLine("Connect to chain");
        var info = await client.GetInfoAsync();
        info.AssertOk();
        Console.WriteLine("ChainName: {0}", info.Result.ChainName);
        Console.WriteLine();
        clientConnected = true;	  

	}


}
