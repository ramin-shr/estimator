using System;

namespace QuoterPlan
{
	public abstract class Command
	{
		public abstract void Undo();

		public abstract void Redo();

		public abstract void RenameLayer(string oldName, string newName);

		protected Command()
		{
		}
	}
}
