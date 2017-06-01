# README #

Application for a voting system using Multichain. Only connects to a blockchain and 
gets a new address for now.

### Setup ###

Requirements: Two linux systems. (Can be two virtual systems)

On your first linux system:

	1. Download and install multichain to allow command line functionality
			cd /tmp
			wget http://www.multichain.com/download/multichain-1.0-beta-1.tar.gz
			tar -xvzf multichain-1.0-beta-1.tar.gz
			cd multichain-1.0-beta-1
			mv multichaind multichain-cli multichain-util /usr/local/bin 

	2. Create a blockchain through command line
			multichain-util create "blockChainName"

	3. Change default parameters of blockchain parameter file.
			sudo gedit ~/.multichain/"blockChainName"/params.dat

			Change the following:
				anyone-can-connect = true 
				anyone-can-send = true

	4. Change default parameters of blockchain conf file.
			sudo gedit ~/.multichain/"blockChainName"/multichain.conf 
			Write down rpcuser/rpcpassword or change them to something easy to remember
			add this line to conf file, which allows any ip to connect to chain 
			(only for development)
				rpcallowip=0.0.0.0/0
				
			Your conf file should have three lines, rpcuser, rpcpassword and rpcallowip.
			
	5. Initialize the blockchain. 
			multichaind "blockChainName" -daemon
	
	6. Now that your blockchain is up you can leave this system idle.
	
On the second linux system:

	1. Install monodevelop preview via flatpak http://www.monodevelop.com/download/linux/ which is required for C# and GTK# on linux.

	2. Clone this repository to your local directory.
	
	3 Either compile and run using the Monodevelop IDE, or mono command line (mcs).

	4. Enter blockchain details into the "Connect to blockchain window".
			These details can be seen on your other systems param and conf file,
			and the output when you initially connected to the chain.
			For example given this
				multichaind blockChainName@192.168.218.131:4391
			IP: 192.168.218.131
			Port: 4390 (RPCPort is always -1 than generic port)
			RPCUsername/RPCPassword (What was in your .conf file)
			ChainName: blockChainName
	
		If the program hangs for ~20seconds when you connect to client
		you have entered the blockchain details incorrectly.

	5. Once you have connected to the blockchain you can simply log in with
		anything (validation not implemented het) and then create a new 
		address from the chain showing that you are connected.

Subsequent setups:

	1. Simply start the blockchain on the first system with 
			multichaind "blockChainName" -daemon
	
	2. Then on the second system run project using MonoDevelop/mono CLI


### References ###

Project currently consists of two projects.

The first MultiChainLib is a library that was created by PbjCloud to allow C# applications to connect with MultiChain:

	Modification in MultiChainClient.cs, added IssueMoreAsync Method.
	Original found here https://github.com/PbjCloud/MultiChainLib.
	
The second VotingMultiChain was created by us with the use of Monodevelops GUI designer.