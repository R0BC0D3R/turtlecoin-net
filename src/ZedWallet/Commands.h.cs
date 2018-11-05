// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


public class Command
{
		public Command(string commandName, string description)
		{
			this.commandName = commandName;
			this.description = description;
		}

		public readonly string commandName;
		public readonly string description;
}

public class AdvancedCommand : Command
{
		public AdvancedCommand(string commandName, string description, bool viewWalletSupport, bool advanced) : base(commandName, description)
		{
			this.viewWalletSupport = viewWalletSupport;
			this.advanced = advanced;
		}

		public readonly bool viewWalletSupport;
		public readonly bool advanced;
}